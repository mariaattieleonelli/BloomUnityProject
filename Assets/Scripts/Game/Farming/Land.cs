using UnityEngine;

public class Land : MonoBehaviour, ITimeTracker
{
    public Material soilMat;
    public Material plantableSoilMat;
    public Material wateredSoilMat;
    public GameObject water;
    public GameObject sparkles;

    private Animator playerAnimator;

    public GameObject soilSelector;

    public Renderer soilPatchRenderer;

    public int id;
    public LandStatus landStatus;

    [Header("Crops")]
    public GameObject cropPrefab;
    CropBehaviour cropPlanted = null;

    //Aquí atrapamos el tiempo en el que fue regada a tierra
    Timestamp timeWatered;

    void Start()
    {
        //Accedemos al animator del personaje
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();

        //Accesamos al componente renderer de los parches de tierra
        soilPatchRenderer = GetComponent<Renderer>();

        //El status por default de la tierra es el de soil
        SwitchLandStatus(LandStatus.SOIL);

        //Agregamos al listener de TimeManager
        TimeManager.instance.RegisterTracker(this);
    }

    //Función para cargar la información de land data
    public void LoadLandData(LandStatus statusToSwitch, Timestamp lastWatered)
    {
        landStatus = statusToSwitch;
        timeWatered = lastWatered;

        Material materialToSwitch = soilMat;

        //Decidimos a qué material se cambia
        switch (statusToSwitch)
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
                //Si la planta está en estado de haberse regado, se activa el efecto de agua cayendo
                water.SetActive(true);
                break;
        }

        //Aplicamos el material al renderer
        soilPatchRenderer.material = materialToSwitch;
    }

    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        water.SetActive(false);
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
                //Si la planta está en estado de haberse regado, se activa el efecto de agua cayendo
                water.SetActive(true);
                timeWatered = TimeManager.instance.GetTimeStamp();
                break;
        }

        //Aplicamos el material al renderer
        soilPatchRenderer.material = materialToSwitch;

        LandManager.instance.OnLandStateChange(id, landStatus, timeWatered);
    }

    //Al seleccionar el parche de tierra con el mouse
    private void OnMouseDown()
    {
        //Evitamos que se interactué con la tierra si está abierto el inventario
        if(UIManager.instance.uiOpened == false)
        {
            //Energía que le quedaría al jugador tras hacer la acción
            float playerEnergyAfterActing = PlayerStats.playerEnergy - 5;

            //Si el jugador tiene energía, se continua
            if (playerEnergyAfterActing >= 0)
            {
                playerAnimator.SetTrigger("tool");

                //Checamos la herramienta que se tiene en la mano
                ItemData toolSlot = InventoryManager2.instance.equipedTool;

                //Si no se tiene nada equipado, terminamos la función
                if (toolSlot == null)
                {
                    return;
                }

                //Intentamos castear la info del item en el slot equipado
                ToolsData equipementTool = toolSlot as ToolsData;

                if (equipementTool != null)
                {
                    ToolsData.ToolType toolType = equipementTool.toolType;

                    switch (toolType)
                    {
                        case ToolsData.ToolType.shovel:
                            if (landStatus != LandStatus.PLANTABLE) //Si el estado no es plantable (es decir, no se ha usado a pala)
                            {
                                SwitchLandStatus(LandStatus.PLANTABLE);
                                //Suena audio de pala
                                AudioManager.instance.ShovelSound();
                                //Gasta energía
                                PlayerStats.ConsumeEnergy();
                            }
                            break;
                        case ToolsData.ToolType.waterCan:
                            //Agua que le quedaría al jugador tras regar
                            float playerWaterAfterActing = PlayerStats.water - 10;
                            //Condición de regar
                            if (playerWaterAfterActing >= 0 && landStatus == LandStatus.PLANTABLE) //Si se tiene agua y el estatus es plantable (ya se usó la pala)
                            {
                                //Suena audio de agua
                                AudioManager.instance.WaterSound();
                                SwitchLandStatus(LandStatus.WATERED);
                                //Gasta energía
                                PlayerStats.ConsumeEnergy();
                                //Gasta agua
                                PlayerStats.UseWater();
                            }
                            else if (playerWaterAfterActing < 0)
                            {
                                UIManager.instance.PopUpWarning("¡Necesitas rellenar tu regadera de agua!");
                            }

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
                if (seedTool != null && landStatus != LandStatus.SOIL && cropPlanted == null)
                {
                    SpawnCrop();

                    //Plantamos la planta
                    cropPlanted.Plant(id, seedTool);
                    //Sonamos sonido de semilla
                    AudioManager.instance.SeedSound();
                }

                //Gasta energía
                PlayerStats.ConsumeEnergy();
            }
            //Sino, se da aviso de que no se tiene energía
            else if (playerEnergyAfterActing < 0)
            {
                UIManager.instance.PopUpWarning("¡No tienes más energía! Será mejor que comas algo...");
            }
        }
    }

    public CropBehaviour SpawnCrop()
    {
        //Instanciamos el el objeto prefab crop
        GameObject cropObject = Instantiate(cropPrefab, transform);
        //Movemos el prefab a la posición que queremos
        cropObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        //Accedemos al comportamiento de la planta que vamos a crear
        cropPlanted = cropObject.GetComponent<CropBehaviour>();

        return cropPlanted;
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

        //Si se plantó una planta y la tierra no está regada
        if(landStatus != LandStatus.WATERED && cropPlanted != null)
        {
            //Y si la semilla ya germinó
            if(cropPlanted.cropState != CropBehaviour.CropState.SEED)
            {
                //Empezamos el proceso de "muerte" de la planta
                cropPlanted.Wither();
            }
        }

        //Si existe un cultivo
        if(cropPlanted != null)
        {
            //Particulas que indican si ya está para cosecharse la planta
            if (cropPlanted.cropState == CropBehaviour.CropState.HARVESTABLE)
            {
                sparkles.SetActive(true);
            }
            else if(cropPlanted.cropState != CropBehaviour.CropState.HARVESTABLE && landStatus == LandStatus.SOIL)
            {
                sparkles.SetActive(false);
            }
        }

    }
}
