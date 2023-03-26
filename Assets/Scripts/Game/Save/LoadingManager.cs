using System.Collections;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    public void Start()
    {
        //al inicio del juego carga lo que se tenga en el JSON de datos de guardado
        SaveManager.Load();

        //Muestra lo que se cargó
        UIManager.instance.RenderInventory();
        UIManager.instance.RenderPlayerStats();
    }

    //private void OnStart()
    //{
    //    WaitToLoad();

    //    //al inicio del juego carga lo que se tenga en el JSON de datos de guardado
    //    SaveManager.Load();

    //    //Muestra lo que se cargó
    //    UIManager.instance.RenderInventory();
    //    UIManager.instance.RenderPlayerStats();

    //}

    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(15);
    }
}
