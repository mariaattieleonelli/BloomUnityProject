using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Tool")]

public class ToolsData : ItemData
{
    public ToolType toolType;

    public enum ToolType
    {
        shovel, waterCan
    }
}
