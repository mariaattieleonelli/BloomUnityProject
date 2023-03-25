using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //Guarda la informaci�n del Game Object al que debe representar
    public ItemData item;

    public virtual void PickupItem()
    {
        //Se coloca en el item equipado el que se est� recogiendo
        InventoryManager2.instance.equipedItem = item;
        //Se muestra en la mano del personaje
        InventoryManager2.instance.RenderHandItem();
        //Adiciona la cantidad de income que da dicho item
        PlayerStats.EarnMoney(item.income);
        //Destruye el que se recogi�
        Destroy(gameObject);
    }
}
