using UnityEngine;

public class Land : MonoBehaviour
{
    public Material soilMat;
    public Material plantableSoilMat;
    public Material wateredSoilMat;

    public GameObject soilSelector;

    public Renderer soilPatchRenderer;

    public LandStatus landStatus;

    void Start()
    {
        //Accesamos al componente renderer de los parches de tierra
        soilPatchRenderer = GetComponent<Renderer>();
        //El status por default de la tierra es el de soil
        SwitchLandStatus(LandStatus.SOIL);
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
                break;
        }

        //Aplicamos el material al renderer
        soilPatchRenderer.material = materialToSwitch;
    }

    //Al seleccionar el parche de tierra con el mouse
    private void OnMouseDown()
    {
        //Checamos la herramienta que se tiene en la mano
        ItemData tooSlot = InventoryManager2.instance.equipedTool;

        //Intentamos castear la info del item en el slot equipado
        ToolsData equipementTool = tooSlot as ToolsData;

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
}
