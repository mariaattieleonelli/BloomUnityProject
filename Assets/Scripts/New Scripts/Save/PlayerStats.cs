using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money { get; private set;}
    public static float playerEnergy { get; private set; } = 100;

    public static float water { get; private set; } = 100;

    public static void ConsumeEnergy()
    {
        playerEnergy -= 10;

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

    public static void LoadStats(int savedMoney, float savedEnergy, float savedWater)
    {
        money = savedMoney;
        playerEnergy = savedEnergy;
        water = savedWater;

        UIManager.instance.RenderPlayerStats();
    }
}
