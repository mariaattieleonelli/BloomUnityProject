using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopListingManager : MonoBehaviour
{
    //El prefab de la lista de objetos de la tienda
    public GameObject shopListing;
    //El transform de la cuadrícula que guarda los elementos de la lista
    public Transform listingGrid;

    //Componentes de la pantalla de confirmación
    public GameObject confirmationPanel;
    public TextMeshProUGUI confirmationPrompt;
    public TextMeshProUGUI costCalculationText;
    public Button purchaseButton;

    //Apoyo para llevar registro de lo que el jugador quiere comprar
    ItemData itemToBuy;

    //Actualiza y muestra la tienda
    public void RenderShop(List<ItemData> shopItems)
    {
        //Resetea 
        if(listingGrid.childCount > 0)
        {
            foreach(Transform child in listingGrid)
            {
                Destroy(child.gameObject);
            }
        }

        //Crea un nuevo slot de tienda por cada item de la lista
        foreach(ItemData shopItem in shopItems)
        {
            GameObject listingGameObject = Instantiate(shopListing, listingGrid);

            //Le asignamos al slot que se instanció qué objeto va a mostrar
            listingGameObject.GetComponent<ShopListing>().Display(shopItem);
        }
    }

    public void OpenConfirmationScreen(ItemData item)
    {
        itemToBuy = item;
        RenderConfirmationScreen();
    }

    public void RenderConfirmationScreen()
    {
        confirmationPanel.SetActive(true);

        confirmationPrompt.text = "¿Comprar " + itemToBuy.name + "?";

        int playerMoneyLeft = PlayerStats.money - itemToBuy.cost;
        
        //Revisamos que el dinero del jugador sea suficiente
        if(playerMoneyLeft < 0)
        {
            costCalculationText.text = "No tienes dinero suficiente";
            //No permite interactuar con el botón de aceptar
            purchaseButton.interactable = false;
            return;
        }
        //Si sí tiene dinero suficiente
        purchaseButton.interactable = true;
        costCalculationText.text = "$" + PlayerStats.money + ">" + playerMoneyLeft;
    }

    //Se compra el item y se cierra la confirmación
    public void ConfirmPurchase()
    {
        Shop.Purchase(itemToBuy);
        confirmationPanel.SetActive(false);
    }

    //Cancela la compra y cierra la pantalla de confirmación
    public void CancelPurchase()
    {
        confirmationPanel.SetActive(false);
    }
}
