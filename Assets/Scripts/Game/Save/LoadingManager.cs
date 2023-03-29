using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    private void Start()
    {
        //Si no es nueva partida, cargamos los datos
        if (PlayerStats.isNewGame == false)
        {
            GameStateManager.instance.LoadSave();
        }
        //Si es una nueva partida reseteamos los stats del jugador
        else if(PlayerStats.isNewGame == true)
        {
            PlayerStats.water = 100;
            PlayerStats.playerEnergy = 100;
            PlayerStats.money = 70;

            LandManager.farmData = null;
        }
    }
}
