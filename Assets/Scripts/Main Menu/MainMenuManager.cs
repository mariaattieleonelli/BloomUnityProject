using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    ////Establece un time stamp inicial en el día 1, 8 am
    //Timestamp resetedTimestamp = new Timestamp(0, Timestamp.Season.SPRING, 1, 8, 0);

    ////Resetea info de cultivos
    //List<LandSaveState> emptyLandData = null;
    //List<CropSaveState> emptyCropData = null;
    
    //public void ResetSaveStateValues()
    //{
    //    //Hora
    //    TimeManager.instance.LoadTime(resetedTimestamp);

    //    //Cultivos
    //    LandManager.farmData = new System.Tuple<List<LandSaveState>, List<CropSaveState>>(emptyLandData, emptyCropData);

    //    //Resetea la información 
    //    LandManager.instance.ImportLandData(LandManager.farmData.Item1); //El item 1 de la tupla, que trae la información de la tierra
    //    LandManager.instance.ImportCropData(LandManager.farmData.Item2); //El item 2 de la tupla, que trae la información de los cultivos

    //    //Resetea player stats
    //    PlayerStats.LoadStats(70, 100, 100);
    //}
}
