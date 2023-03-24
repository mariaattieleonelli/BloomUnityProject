using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveState
{
    //Farm data
    public List<LandSaveState> landData;
    public List<CropSaveState> cropData;

    ////Inventario
    //public ItemSlotData[] toolSlots;
    //public ItemSlotData[] itemSlots;

    ////Objetos equipados
    //public ItemSlotData equippedToolSlot;
    //public ItemSlotData equippedItemSlot;

    //Tiempo
    public Timestamp timeStamp;

    //Player stats
    public int money;
    public float playerEnergy;
    public float water;

    public GameSaveState(List<LandSaveState> landData, List<CropSaveState> cropData, /*ItemSlotData[] toolSlots, ItemSlotData[] itemSlots, ItemSlotData equippedToolSlot, ItemSlotData equippedItemSlot,*/ Timestamp timeStamp, int money, float playerEnergy, float water)
    {
        this.landData = landData;
        this.cropData = cropData;
        this.money = money;
        this.playerEnergy = playerEnergy;
        this.water = water;
        //this.toolSlots = toolSlots;
        //this.itemSlots = itemSlots;
        //this.equippedToolSlot = equippedToolSlot;
        //this.equippedItemSlot = equippedItemSlot;
        this.timeStamp = timeStamp;
    }
}
