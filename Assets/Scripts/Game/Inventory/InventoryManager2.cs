using UnityEngine;

public class InventoryManager2 : MonoBehaviour
{
    public static InventoryManager2 instance { get; private set; }

    //Espacios de inventario para herramientas y herramienta en mano
    public ItemData[] tools = new ItemData[9];
    public ItemData equipedTool = null;

    //Espacios de inventario para items e item en mano
    public ItemData[] items = new ItemData[9];
    public ItemData equipedItem = null;

    public Transform playersHand;

    private void Awake()
    {
        //si hay otra instancia, destruir la extra
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    //La lista completa de items
    public ItemIndex itemIndex;

    //Movimiento de items del inventario al equipado
    public void InventoryToHand(int slotIndex, InventorySlot.InventoryType inventoryType)
    {
        if(inventoryType == InventorySlot.InventoryType.Item)
        {
            //Tomamos la información del item de InventoryManager2
            ItemData itemToEquip = items[slotIndex];

            //Cambiamos el item a la mano
            items[slotIndex] = equipedItem;

            //Cambiamos lo que estaba en la mano al inventario (la información que tomamos al inicio)
            equipedItem = itemToEquip;

            //Actualiza el item en mano
            RenderHandItem();
        }
        else
        {
            //Tomamos la información de la herramienta de InventoryManager2
            ItemData toolToEquip = tools[slotIndex];

            //Cambiamos la herramienta a la mano
            tools[slotIndex] = equipedTool;

            //Cambiamos lo que estaba en la mano al inventario (la información que tomamos al inicio)
            equipedTool = toolToEquip;
        }

        //Actualizamos los cambios a la UI mediante UIManager
        UIManager.instance.RenderInventory();
    }

    //Muestra el objeto que se tiene equipado en la mano del personaje
    public void RenderHandItem()
    {
        //Si se tiene algo en la mano, eliminamos el objeto
        if(playersHand.childCount > 0)
        {
            Destroy(playersHand.GetChild(0).gameObject);
        }

        //Checamos si se tiene algo equipado
        if(equipedItem != null)
        {
            Instantiate(equipedItem.gameModel, playersHand);
        }
    }

    //Movimiento de items del equipado al inventario
    public void HandToInventory(InventorySlot.InventoryType inventoryType)
    {
        if(inventoryType == InventorySlot.InventoryType.Item)
        {
            //Iteramos sobre los slots y encontramos el primero que esté vacío
            for(int i = 0; i < items.Length; i ++)
            {
                if(items[i] == null)
                {
                    //Mandamos el item en mano al inventario
                    items[i] = equipedItem;
                    //Quitamos el item de la mano
                    equipedItem = null;
                    break;
                }
            }

            //Actualiza el item en mano
            RenderHandItem();
        }
        else
        {
            //Iteramos sobre los slots y encontramos el primero que esté vacío
            for (int i = 0; i < tools.Length; i++)
            {
                if (tools[i] == null)
                {
                    //Mandamos el item en mano al inventario
                    tools[i] = equipedTool;
                    //Quitamos la herramienta de la mano
                    equipedTool = null;
                    break;
                }
            }
        }
        //Mostramos el inventario actualizado
        UIManager.instance.RenderInventory();
    }

    //Función que manda de la tienda al inventario del personaje el item
    public void ShopToInventory(ItemData itemSlotToMove)
    {
        //Iteramos sobre los slots y encontramos el primero que esté vacío
        for (int i = 0; i < tools.Length; i++)
        {
            if (tools[i] == null)
            {
                //Mandamos el item en mano al inventario
                tools[i] = itemSlotToMove;
                break;
            }
        }
        //Actualiza el item en mano
        RenderHandItem();

        //Mostramos el inventario actualizado
        UIManager.instance.RenderInventory();
    }
}
