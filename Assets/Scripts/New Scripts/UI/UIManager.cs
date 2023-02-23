using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    public InventorySlot[] toolSlots;
    public InventorySlot[] itemSlots;
    public GameObject inventorypanel;

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
    }

    //Itera sobre un slot en una sección y se muestra en el UI
    void RenderInventoryPanel(ItemData[] slots, InventorySlot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            //Mostrar espacios de manera adecuada
            uiSlots[i].Display(slots[1]);
        }
    }

    public void ToggleInventoryPanel()
    {
        //Si está escondido se muestra, y viceversa
        inventorypanel.SetActive(!inventorypanel.activeSelf);

        RenderInventory();
    }
}
