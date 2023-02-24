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
}
