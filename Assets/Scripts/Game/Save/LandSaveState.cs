[System.Serializable]
public struct LandSaveState
{
    public Land.LandStatus landStatus;
    public Timestamp lastWatered;

    public LandSaveState(Land.LandStatus landStatus, Timestamp lastWatered)
    {
        this.landStatus = landStatus;
        this.lastWatered = lastWatered;
    }
}
