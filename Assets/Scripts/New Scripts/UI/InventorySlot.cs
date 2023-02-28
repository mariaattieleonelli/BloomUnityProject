using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    ItemData itemIcon;
    ItemData itemName;

    public Image itemDisplayImage;
    public TextMeshProUGUI itemNameText;

    int slotIndex;

    //Determinamos a qu� secci�n del inventario pertenece el slot
    public InventoryType inventoryType;
    public enum InventoryType
    {
        Item, Tool
    }

    public void Display(ItemData itemToDisplay)
    {
        //Revisa que haya un item para mostrar
        if (itemToDisplay != null)
        {
            itemDisplayImage.sprite = itemToDisplay.itemIcon;
            itemNameText.text = itemToDisplay.itemName;
            this.itemIcon = itemToDisplay;

            itemDisplayImage.gameObject.SetActive(true);
            itemNameText.gameObject.SetActive(true);

            return;
        }
        
        itemDisplayImage.gameObject.SetActive(false);
        itemNameText.gameObject.SetActive(false);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        //Funci�n que mueve del inventario a la mano
        InventoryManager2.instance.InventoryToHand(slotIndex, inventoryType);
    }

    //Set the slot index
    public void AssignIndex(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }
}
