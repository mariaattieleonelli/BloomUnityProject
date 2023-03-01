using UnityEngine;

public class Land : MonoBehaviour, ITimeTracker
{
    public Material soilMat;
    public Material plantableSoilMat;
    public Material wateredSoilMat;

    public GameObject soilSelector;

    public Renderer soilPatchRenderer;

    public LandStatus landStatus;

    [Header("Crops")]
    public GameObject cropPrefab;
    CropBehaviour cropPlanted = null;

    //Aquí atrapamos el tiempo en el que fue regada a tierra
    Timestamp timeWatered;

    void Start()
    {
        //Accesamos al componente renderer de los parches de tierra
        soilPatchRenderer = GetComponent<Renderer>();

        //El status por default de la tierra es el de soil
        SwitchLandStatus(LandStatus.SOIL);

        //Agregamos al listener de TimeManager
        TimeManager.instance.RegisterTracker(this);
    }

    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        landStatus = statusToSwitch;
        Material materialToSwitch = soilMat;

        //Decidimos a qué material se cambia
        switch(statusToSwitch)
        {
            case LandStatus.SOIL:
                //Cambia al material de tierra normal
                materialToSwitch = soilMat;
                break;
            case LandStatus.PLANTABLE:
                //Cambia al material de tierra plantable
                materialToSwitch = plantableSoilMat;
                break;
            case LandStatus.WATERED:
                //Cambia al material de tierra regada
                materialToSwitch = wateredSoilMat;
                timeWatered = TimeManager.instance.GetTimeStamp();
                break;
        }

        //Aplicamos el material al renderer
        soilPatchRenderer.material = materialToSwitch;
    }

    //Al seleccionar el parche de tierra con el mouse
    private void OnMouseDown()
    {
        //Checamos la herramienta que se tiene en la mano
        ItemData toolSlot = InventoryManager2.instance.equipedTool;

        //Si no se tiene nada equipado, terminamos la función
        if(toolSlot == null)
        {
            return;
        }

        //Intentamos castear la info del item en el slot equipado
        ToolsData equipementTool = toolSlot as ToolsData;

        if(equipementTool != null)
        {
            ToolsData.ToolType toolType = equipementTool.toolType;

            switch (toolType)
            {
                case ToolsData.ToolType.shovel:
                    SwitchLandStatus(LandStatus.PLANTABLE);
                    break;
                case ToolsData.ToolType.waterCan:
                    SwitchLandStatus(LandStatus.WATERED);
                    break;
            }
            return;
        }

        //Intentamos castear la info de la semilla equipada en el slot de equipado
        SeedData seedTool = toolSlot as SeedData;

        //Las condiciones para que se pueda plantar una semilla son:
        //Se está usando una bolsa de semillas (objeto tipo SeedData)
        //El estado de la tierra debe estar listo para sembrar o regado
        //No hay otra planta sembrada
        if(seedTool != null && landStatus != LandStatus.SOIL && cropPlanted == null)
        {
            //Instanciamos el el objeto prefab crop
            GameObject cropObject = Instantiate(cropPrefab, transform);
            //Movemos el prefab a la posición que queremos
            cropObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z);

            //Accedemos al comportamiento de la planta que vamos a crear
            cropPlanted = cropObject.GetComponent<CropBehaviour>();
            //Plantamos la planta
            cropPlanted.Plant(seedTool);
        }
    }

    //Cuando se hace hover sobre la tierra se muestra un efecto que indica interactibilidad
    private void OnMouseOver()
    {
        soilSelector.SetActive(true);
    }

    private void OnMouseExit()
    {
        soilSelector.SetActive(false);
    }

    public enum LandStatus
    {
        SOIL, PLANTABLE, WATERED
    }

    public void ClockUpdate(Timestamp timestamp)
    {
        //Checamos si pasaron 24 horas para secar la tierra regada
        if(landStatus == LandStatus.WATERED)
        {
            //Horas desde que se regó
            int hoursPassed = Timestamp.CompareTimestamps(timeWatered, timestamp);

            //Crecimiento de la planta
            if(cropPlanted != null)
            {
                cropPlanted.Grow();
            }
            
            //Regresamos al estado de tierra seca si pasaron las 24 horas
            if(hoursPassed > 24)
            {
                SwitchLandStatus(LandStatus.PLANTABLE);
            }
        }
    }
}
