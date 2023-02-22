using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Seed")]
public class SeedData : ItemData
{
    public int daysToGrow;
    public ItemData objectToGrow;
}
