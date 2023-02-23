using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    ItemData itemIcon;
    public Image itemDisplayImage;

    public void Display(ItemData itemToDisplay)
    {
        if (itemToDisplay != null)
        {
            itemDisplayImage.sprite = itemToDisplay.itemIcon;
            this.itemIcon = itemToDisplay;

            itemDisplayImage.gameObject.SetActive(true);

            return;
        }
        
        itemDisplayImage.gameObject.SetActive(false);
        
    }
}
