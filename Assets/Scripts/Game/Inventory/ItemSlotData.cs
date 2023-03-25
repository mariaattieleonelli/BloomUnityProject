using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotData
{
    public ItemData itemData;
    public int quantity;

    //Constructor de la clase
    public ItemSlotData(ItemData itemData, int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;
        ValidateQuantity();
    }

    //En primera instancia construye la clase con la cantidad de 1 item
    public ItemSlotData(ItemData itemData)
    {
        this.itemData = itemData;
        quantity = 1;
        ValidateQuantity();
    }

    //SISTEMA DE STACKING DE ITEMS

    //Shortcut para añadir 1 al stack
    public void AddQuantity()
    {
        AddQuantity(1);
    }

    //Añade una cantidad específica al stack
    public void AddQuantity(int amountToAdd)
    {
        quantity += amountToAdd;
    }

    //Quita del stack
    public void RemoveQuantity()
    {
        quantity--;
        ValidateQuantity();
    }

    //Valida si hay items 
    private void ValidateQuantity()
    {
        if(quantity <= 0 || itemData == null)
        {
            Empty();
        }
    }

    //Vacía el item slot
    public void Empty()
    {
        itemData = null;
        quantity = 0;
    }
}
