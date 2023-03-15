using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractableObject
{
    public override void PickupItem()
    {
        ////Muestra el yes/no antes de ejecutar la acci�n de dormir
        UIManager.instance.TriggerYesNoPrompt("Quieres terminar el dia?", GameStateManager.instance.Sleep);
    }
}
