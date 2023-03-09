using System;
using System.Collections.Generic;
using UnityEngine;

public class LandManager : MonoBehaviour
{
    public static LandManager instance { get; private set; }

    //Guarda la información
    public static Tuple<List<LandSaveState>, List<CropSaveState>> farmData = null;

    //El estado de guardado de los espacios de tierra y cultivos
    public List<LandSaveState> landData = new List<LandSaveState>();
    List<CropSaveState> cropData = new List<CropSaveState>();

    public List<Land> landPlots = new List<Land>();

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

    private void Start()
    {
        RegisterLandPlots();

        //Si farm data tiene información, la cargamos
        if(farmData != null)
        {
            //Carga la información guardada
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

    //Cargamos la información de farmData a landData
    public void ImportLandData(List<LandSaveState> landDatasetToLoad)
    {
        for(int i = 0; i < landDatasetToLoad.Count; i++)
        {
            LandSaveState landDataToLoad = landDatasetToLoad[i];
            landPlots[i].LoadLandData(landDataToLoad.landStatus, landDataToLoad.lastWatered);
        }
    }
}


