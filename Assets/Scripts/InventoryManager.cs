using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<Item> items = new List<Item>();

    public Transform inventoryContent;
    public GameObject inventoryItem;

    public void Awake()
    {
        instance = this;
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        foreach(Transform item in inventoryContent)
        {
            Destroy(item.gameObject);
        }
        foreach(var item in items)
        {
            GameObject obj = Instantiate(inventoryItem, inventoryContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemImage = obj.transform.Find("ItemImage").GetComponent<Image>();

            itemName.text = item.itemName;
            itemImage.sprite = item.item;
        }
    }
}
