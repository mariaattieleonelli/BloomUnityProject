using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    //Son la UI de los slots en el inventario
    public InventorySlot[] toolSlots;
    public InventorySlot[] itemSlots;

    public Image toolEquipSlot;

    public GameObject inventorypanel;

    //Es el slot donde se equipa la herramienta en la UI del inventario
    public HandInventorySlot toolHandSlot;

    //Es el slot donde se equipa el item en la UI del inventario
    public HandInventorySlot itemHandSlot;

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

    private void Start()
    {
        RenderInventory();
        AssignSlotIndexes();
    }

    //Itera sobre los slots de la UI y les asigna el índice adecuado a su referencia
    public void AssignSlotIndexes()
    {
        for(int i = 0; i < toolSlots.Length; i ++)
        {
            //La función AssignIndex viene del script de InventorySlot
            toolSlots[i].AssignIndex(i);
            itemSlots[i].AssignIndex(i);
        }
    }

    //Reflejar inventario en la pantalla de inventario
    public void RenderInventory()
    {
        //Tomamos los slots de herramientas del Inventory Manager
        ItemData[] inventoryToolSlots = InventoryManager2.instance.tools;

        //Tomamos los slots de items del Inventory Manager
        ItemData[] inventoryItemSlots = InventoryManager2.instance.items;

        //Mostrar la sección de herramientas
        RenderInventoryPanel(inventoryToolSlots, toolSlots);

        //Mostrar la sección de items
        RenderInventoryPanel(inventoryItemSlots, itemSlots);

        //Mostrar los elementos equipados (en mano)
        toolHandSlot.Display(InventoryManager2.instance.equipedTool);
        itemHandSlot.Display(InventoryManager2.instance.equipedItem);

        //Tomamos la herramienta equipada del inventory manager
        ItemData equipedTool = InventoryManager2.instance.equipedTool;

        //Revisa que haya un item para mostrar
        if (equipedTool != null)
        {
            toolEquipSlot.sprite = equipedTool.itemIcon;

            toolEquipSlot.gameObject.SetActive(true);

            return;
        }

        toolEquipSlot.gameObject.SetActive(false);
    }

    //Itera sobre un slot en una sección y se muestra en el UI
    void RenderInventoryPanel(ItemData[] slots, InventorySlot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            //Mostrar espacios de manera adecuada
            uiSlots[i].Display(slots[i]);
        }
    }

    public void ToggleInventoryPanel()
    {
        //Si está escondido se muestra, y viceversa
        inventorypanel.SetActive(!inventorypanel.activeSelf);

        RenderInventory();
    }
}
