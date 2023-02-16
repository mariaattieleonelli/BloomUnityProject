using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite item;
    public GameObject cropPrefab;

    public ItemType itemType;

    public enum ItemType
    {
        CROP,
        SEED
    }
}
