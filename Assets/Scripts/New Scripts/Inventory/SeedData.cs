using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Seed")]
public class SeedData : ItemData
{
    public int daysToGrow;
    public ItemData objectToGrow;
    //Game object que ser� el reto�o de la planta
    public GameObject sprout;
}
