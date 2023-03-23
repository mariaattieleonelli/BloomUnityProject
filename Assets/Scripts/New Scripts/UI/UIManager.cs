using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

//Link para objetos clickeados tras botones
//https://
//answers.unity.com/questions/1220418/object-behind-button-is-clicked-too.html*/

public class UIManager : MonoBehaviour, ITimeTracker
{
    public static UIManager instance { get; private set; }

    //Son la UI de los slots en el inventario
    public InventorySlot[] toolSlots;
    public InventorySlot[] itemSlots;

    public Image toolEquipSlot;

    public Sprite daySprite;
    public Sprite nightSprite;
    public Sprite winterSprite;
    public Sprite fallSprite;
    public Sprite summerSprite;
    public Sprite springSprite;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dateText;
    public Image dayTimeImage;
    public Image seasonImage;

    public GameObject inventorypanel;
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public YesNoPrompt yesNoPrompt;

    public TextMeshProUGUI moneyText;

    public Image energyBar;
    public TextMeshProUGUI energyPercentage;

    //Es el slot donde se equipa la herramienta en la UI del inventario
    public HandInventorySlot toolHandSlot;

    //Es el slot donde se equipa el item en la UI del inventario
    public HandInventorySlot itemHandSlot;

    public ShopListingManager shopListingManager;

    //Se establece la instancia
    private void Awake()
    {
        //si hay otra instancia, destruir la extra
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        RenderInventory(); //Actualiza inventario y muestra
        AssignSlotIndexes(); //Asigna los index a la tierra
        UIManager.instance.RenderPlayerStats(); //Actualiza en la UI los stats del jugador

        //Añadimos al UIManager a la lista de objetos a los que se le notifican cambios en el paso del tiempo
        TimeManager.instance.RegisterTracker(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePausePanel();
        }
    }

    //Carga la pantalla de Main Menu
    public void MainMenu()
    {
        SceneManager.LoadScene(sceneName: "Main Menu");
    }

    //Itera sobre los slots de la UI y les asigna el índice adecuado a su referencia
    public void AssignSlotIndexes()
    {
        for(int i = 0; i < toolSlots.Length; i ++)
        {
            //La función AssignIndex viene del script de InventorySlot
            toolSlots[i].AssignIndex(i);
            itemSlots[i].AssignIndex(i);
        }
    }

    //Reflejar inventario en la pantalla de inventario
    public void RenderInventory()
    {
        //Tomamos los slots de herramientas del Inventory Manager
        ItemData[] inventoryToolSlots = InventoryManager2.instance.tools;

        //Tomamos los slots de items del Inventory Manager
        ItemData[] inventoryItemSlots = InventoryManager2.instance.items;

        //Mostrar la sección de herramientas
        RenderInventoryPanel(inventoryToolSlots, toolSlots);

        //Mostrar la sección de items
        RenderInventoryPanel(inventoryItemSlots, itemSlots);

        //Mostrar los elementos equipados (en mano)
        toolHandSlot.Display(InventoryManager2.instance.equipedTool);
        itemHandSlot.Display(InventoryManager2.instance.equipedItem);

        //Tomamos la herramienta equipada del inventory manager
        ItemData equipedTool = InventoryManager2.instance.equipedTool;

        //Revisa que haya un item para mostrar
        if (equipedTool != null)
        {
            toolEquipSlot.sprite = equipedTool.itemIcon;

            toolEquipSlot.gameObject.SetActive(true);

            return;
        }

        toolEquipSlot.gameObject.SetActive(false);
    }

    //Itera sobre un slot en una sección y se muestra en el UI
    void RenderInventoryPanel(ItemData[] slots, InventorySlot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            //Mostrar espacios de manera adecuada
            uiSlots[i].Display(slots[i]);
        }
    }

    #region Panel Activations

    //Las siguientes 4 funciones abren los paneles correspondientes
    public void ToggleInventoryPanel()
    {
        //Sonido de click
        AudioManager.instance.ButtonClick();

        //Si está escondido se muestra, y viceversa
        inventorypanel.SetActive(!inventorypanel.activeSelf);

        RenderInventory();
    }

    public void TogglePausePanel()
    {
        //Sonido de click
        AudioManager.instance.ButtonClick();

        pausePanel.SetActive(!pausePanel.activeSelf);
    }

    public void OpenOptionsPanel()
    {
        //Sonido de click
        AudioManager.instance.ButtonClick();

        optionsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void CloseOptionsPanel()
    {
        //Sonido de click
        AudioManager.instance.ButtonClick();

        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void TriggerYesNoPrompt(string message, System.Action onYes)
    {
        //Activa el panel
        yesNoPrompt.gameObject.SetActive(true);

        yesNoPrompt.CreatePrompt(message, onYes);
    }

    public void OpenShop(List<ItemData> shopItems)
    {
        //Muestra la ventana de la tienda
        shopListingManager.gameObject.SetActive(true);
        shopListingManager.RenderShop(shopItems);
    }
    #endregion

    //Nos traemos a esta función el timeStamp
    public void ClockUpdate(Timestamp timeStamp)
    {
        //Ajuste del tiempo para mostrarse en UI

        //Tomamos horas y minutos
        int hours = timeStamp.hour;
        int minutes = timeStamp.minute;


        string sufix = "am";
        dayTimeImage.sprite = daySprite;

        //Se coloca la imagen de noche en la UI de day time
        if (hours > 19)
        {
            hours -= 12;
            dayTimeImage.sprite = nightSprite;
        }

        //Convertimos a reloj de 12 horas
        if (hours > 12)
        {
            hours -= 12;
            sufix = "pm";
        }

        //Mostramos hora en la UI
        timeText.text = hours + ":" + minutes.ToString("00") + " " + sufix;

        //Cambio de imagen dependiendo de la temporada


        //Manejo de la fecha
        int day = timeStamp.day;
        string dayOfTheWeek = timeStamp.GetDayOfTheWeek().ToString();

        //Mostramos la fecha en la UI
        dateText.text = dayOfTheWeek + " " + day;
    }

    //Muestra stats del jugador
    public void RenderPlayerStats()
    {
        //Muestra el dinero del jugador
        moneyText.text = "$" + PlayerStats.money;

        energyPercentage.text = PlayerStats.playerEnergy.ToString();

        energyBar.fillAmount = PlayerStats.playerEnergy / 100;
    }
}
