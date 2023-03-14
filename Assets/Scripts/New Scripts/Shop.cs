using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<ItemData> shopItems;

    public static void Purchase(ItemData item)
    {
        //El costo va a ser el del item definido en su scriptable object
        int cost = item.cost;

        //Si el jugador tiene más dinero del que cuesta el item, gastamos
        if(PlayerStats.money > cost)
        {
            PlayerStats.SpendMoney(cost);
            //Crea un ItemSlotData al item comprado
            ItemSlotData purchasedItem = new ItemSlotData(item);

            //Mandamos el objeto comprado al inventario
        }
    }
}
