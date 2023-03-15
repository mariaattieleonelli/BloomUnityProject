using System.Collections;
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

    public void Sleep()
    {
        Debug.Log("Duermo");
    }
}
