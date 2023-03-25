using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //Lugar en donde se guardará el JSON
    static readonly string FILEPATH = Application.persistentDataPath + "/Save.json";

    public static void Save(GameSaveState saveData)
    {
        string json = JsonUtility.ToJson(saveData);
        //Genera el json con la información y lo guarda en el path que le indicamos
        File.WriteAllText(FILEPATH, json);
    }

    public static GameSaveState Load()
    {
        GameSaveState loadedSave = null;
        if (File.Exists(FILEPATH))
        {
            string json = File.ReadAllText(FILEPATH);
            loadedSave = JsonUtility.FromJson<GameSaveState>(json);
        }

        return loadedSave;
    }
}
