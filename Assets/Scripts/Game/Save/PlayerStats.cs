using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money { get; private set; } = 70;
    public static float playerEnergy { get; private set; } = 100;

    public static float water { get; private set; } = 100;

    public static float sfxVolume { get; private set; }
    public static float musicVolume { get; private set; }

    public static bool isNewGame { get; set; }

    #region Player Actions
    public static void ConsumeEnergy()
    {
        playerEnergy -= 5;

        //Actualiza en la UI los stats del jugador
        UIManager.instance.RenderPlayerStats();
    }

    public static void GainEnergy(int energyGain)
    {
        playerEnergy += energyGain;

        AudioManager.instance.EatingSound();

        //Actualiza en la UI los stats del jugador
        UIManager.instance.RenderPlayerStats();
    }

    public static void UseWater()
    {
        water -= 10;
    }

    public static void RefillWater()
    {
        water = 100;
        //Actualiza en la UI los stats del jugador
        UIManager.instance.RenderPlayerStats();
    }
    #endregion

    #region Money Actions
    public static void SpendMoney(int price)
    {
        //Si el precio del objeto es mayor que su dinero
        if(price > money)
        {
            Debug.Log("La cantidad es mayor a los ahorros");
            return;
        }
        //Sino, gasta el costo del objeto
        money -= price;

        //Actualiza en la UI los stats del jugador
        UIManager.instance.RenderPlayerStats();
    }

    public static void EarnMoney(int income)
    {
        //Se añade a los ahorros lo que se gane
        money += income;

        //Actualiza en la UI los stats del jugador
        UIManager.instance.RenderPlayerStats();
    }

    #endregion

    public static void LoadStats(int savedMoney, float savedEnergy, float savedWater)
    {
        money = savedMoney;
        playerEnergy = savedEnergy;
        water = savedWater;

        UIManager.instance.RenderPlayerStats();
    }
}
