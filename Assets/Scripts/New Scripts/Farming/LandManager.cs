using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandManager : MonoBehaviour
{
    public static LandManager instance { get; private set; }

    //Guarda la información
    public static Tuple<List<LandSaveState>, List<CropSaveState>> farmData = null;

    //El estado de guardado de los espacios de tierra y cultivos
    List<LandSaveState> landData = new List<LandSaveState>();
    List<CropSaveState> cropData = new List<CropSaveState>();

    List<Land> landPlots = new List<Land>();

    private void Awake()
    {
        //si hay otra instancia, destruir la extra
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void OnEnable()
    {
        RegisterLandPlots();
        //Con esta corrutina esperamos a que primero cargue todo antes de querer darle la farm data
        StartCoroutine(LoadFarmData());
    }

    IEnumerator LoadFarmData()
    {
        yield return new WaitForEndOfFrame();
        //Si farm data tiene información, la cargamos
        if (farmData != null)
        {
            //Carga la información guardada
            ImportLandData(farmData.Item1); //El item 1 de la tupla, que trae la información de la tierra
            ImportCropData(farmData.Item2); //El item 2 de la tupla, que trae la información de los cultivos
        }
    }

    //Función con la que se guarda la información
    public void SaveFarmData()
    {
        farmData = new Tuple<List<LandSaveState>, List<CropSaveState>>(landData, cropData);
    }

    //Toma todos los espacios "plantables"
    void RegisterLandPlots()
    {
        foreach(Transform landTransform in transform)
        {
            Land land = landTransform.GetComponent<Land>();
            landPlots.Add(land);

            //Crea su correspondiente LandSaveState
            landData.Add(new LandSaveState());

            //Asignamos un id al pedazo de tierra
            land.id = landPlots.Count - 1;
        }
    }

    //Actualizamos la land data con cada cambio en el land state
    public void OnLandStateChange(int id, Land.LandStatus landStatus, Timestamp lastWatered)
    {
        landData[id] = new LandSaveState(landStatus, lastWatered);
    }

    //Registra el cultivo en la lista
    public void RegisterCrop(int landId, SeedData seedToGrow, CropBehaviour.CropState cropState, int growth, int plantHealth)
    {
        cropData.Add(new CropSaveState(landId, seedToGrow.name, cropState, growth, plantHealth));
    }

    //Elimina el cultivo de la lista
    public void DeregisterCrop(int landId)
    {
        //Encuentra su indice en la lista y lo elimina
        cropData.RemoveAll(x => x.landPlotId == landId);
    }

    //Actualizamos la crop data con cada cambio en el land state
    public void OnCropStateChange(int landId, CropBehaviour.CropState cropState, int growth, int plantHealth)
    {
        //Encuentra su indice en la lista
        int cropIndex = cropData.FindIndex(x => x.landPlotId == landId);

        //Esto lo metí en un if porque cuando ya nace la planta, el crop se destruye y se pierde el index
        if(cropIndex >= 0)
        {
            string seedToGrow = cropData[cropIndex].seedToGrow;
            cropData[cropIndex] = new CropSaveState(landId, seedToGrow, cropState, growth, plantHealth);
        }
    }

    //Cargamos la información de farmData a landData
    public void ImportLandData(List<LandSaveState> landDatasetToLoad)
    {
        for(int i = 0; i < landDatasetToLoad.Count; i++)
        {
            LandSaveState landDataToLoad = landDatasetToLoad[i];
            landPlots[i].LoadLandData(landDataToLoad.landStatus, landDataToLoad.lastWatered);
        }

        //Asignamos los valores de land data
        landData = landDatasetToLoad;
    }

    //Cargamos la información de farmData hacia cropData
    public void ImportCropData(List<CropSaveState> cropDatasetToLoad)
    {
        cropData = cropDatasetToLoad;

        foreach (CropSaveState cropSave in cropDatasetToLoad)
        {
            //Accesamos a la tierra
            Land landToPlant = landPlots[cropSave.landPlotId];
            //Hacemos aparecer al cultivo
            CropBehaviour cropToPlant = landToPlant.SpawnCrop();
            //Lo cargamos en la data
            SeedData seedToGrow = (SeedData) InventoryManager2.instance.itemIndex.GetItemFromString(cropSave.seedToGrow);
            cropToPlant.LoadCrop(cropSave.landPlotId, seedToGrow, cropSave.cropState, cropSave.growth, cropSave.plantHealth);
        }
    }
}


