using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Seed")]
public class SeedData : ItemData
{
    public int daysToGrow;
    public ItemData objectToGrow;
    //Game object que será el retoño de la planta
    public GameObject sprout;
}
