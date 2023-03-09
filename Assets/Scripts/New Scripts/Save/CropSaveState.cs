using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CropSaveState
{
    //Indica el pedazo de tierra en donde estaba plantada dicha planta
    public int landPlotId;

    public string seedToGrow;
    public CropBehaviour.CropState cropState;
    public int growth;
    public int plantHealth;

    public CropSaveState(int landPlotId, string seedToGrow, CropBehaviour.CropState cropState, int growth, int plantHealth)
    {
        this.landPlotId = landPlotId;
        this.seedToGrow = seedToGrow;
        this.cropState = cropState;
        this.growth = growth;
        this.plantHealth = plantHealth;
    }
}
