using System.Collections.Generic;
using UnityEngine;

public class Shop : InteractableObject
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

            //Mandamos el objeto comprado al inventario
            InventoryManager2.instance.ShopToInventory(item);
        }
    }

    public override void PickupItem()
    {
        UIManager.instance.OpenShop(shopItems);
    }
}
