using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopListing : MonoBehaviour, IPointerClickHandler
{
    public Image itemIcon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;

    ItemData itemData;

    public void Display(ItemData itemData)
    {
        this.itemData = itemData;
        itemIcon.sprite = itemData.itemIcon;
        nameText.text = itemData.itemName;
        costText.text = "$" + itemData.cost;
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        //Dispara sonido de botón de UI
        AudioManager.instance.ButtonClick();

        //Función que mueve del inventario a la mano
        UIManager.instance.shopListingManager.OpenConfirmationScreen(itemData);
    }

}
