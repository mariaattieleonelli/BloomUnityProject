using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopListing : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;

    ItemData itemData;

    public void Display(ItemData itemData)
    {
        this.itemData = itemData;
        itemIcon.sprite = itemData.itemIcon;
        nameText.text = itemData.name;
        costText.text = "$" + itemData.cost;
    }
}
