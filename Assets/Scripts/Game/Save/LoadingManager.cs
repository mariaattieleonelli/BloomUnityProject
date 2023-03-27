using System.Collections;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    private void Awake()
    {
        //al inicio del juego carga lo que se tenga en el JSON de datos de guardado
        SaveManager.Load();
    }
}
