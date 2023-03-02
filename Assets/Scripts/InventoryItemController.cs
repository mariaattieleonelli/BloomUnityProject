using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    public void RemoveItem()
    {
        InventoryManager.instance.RemoveItem(item);
        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    //Pertence al primer intento
    //public void UseItem()
    //{
    //    switch(item.itemType)
    //    {
    //        case Item.ItemType.Crop:
    //            CharacterController.instance.IncreaseEnergy(item.value);
    //            break;
    //    }

    //    RemoveItem();
    //}
}
