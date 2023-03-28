using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    void Start()
    {
        //Si es una nueva partida, activamos el tutorial
        if (PlayerStats.isNewGame == false)
        {
            this.gameObject.SetActive(false);
        }   
    }
}
