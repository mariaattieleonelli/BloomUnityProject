using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money { get; private set;}

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

    public static void LoadStats()
    {
        money = money;
        UIManager.instance.RenderPlayerStats();
    }
}
