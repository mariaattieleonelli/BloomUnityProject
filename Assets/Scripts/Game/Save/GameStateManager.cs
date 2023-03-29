using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance { get; private set; }

    //Se establece la instancia
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

    //Función para irte a dormir (no se implementó aún)
    public void Sleep()
    {
        Debug.Log("Duermo");
    }

    public GameSaveState ExportSaveState()
    {
        //Recupera la info de los cultivos
        List<LandSaveState> landData = LandManager.farmData.Item1;
        List<CropSaveState> cropData = LandManager.farmData.Item2;

        //Recupera info del inventario


        //Recupera estado del time stamp
        Timestamp timestamp = TimeManager.instance.GetTimeStamp();

        return new GameSaveState(landData, cropData, timestamp, PlayerStats.money, PlayerStats.playerEnergy, PlayerStats.water);

    }

    public void SaveGame()
    {
        SaveManager.Save(ExportSaveState());
    }

    public void LoadSave()
    {
        //Recuperamos la info guardada
        GameSaveState save = SaveManager.Load();

        //Hora
        TimeManager.instance.LoadTime(save.timeStamp);

        //Cultivos
        LandManager.farmData = new System.Tuple<List<LandSaveState>, List<CropSaveState>>(save.landData, save.cropData);

        if (LandManager.farmData != null)
        {
            //Carga la información guardada
            LandManager.instance.ImportLandData(LandManager.farmData.Item1); //El item 1 de la tupla, que trae la información de la tierra
            LandManager.instance.ImportCropData(LandManager.farmData.Item2); //El item 2 de la tupla, que trae la información de los cultivos
        }

        //Player stats
        PlayerStats.LoadStats(save.money, save.playerEnergy, save.water);
    }
}
