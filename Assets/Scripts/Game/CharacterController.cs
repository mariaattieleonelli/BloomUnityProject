using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public static CharacterController instance;

    //Raycast del mouse
    private RaycastHit mouseHit;
    private Ray mouseRay;

    public float speed;
    private Rigidbody characterRB;
    public Animator playerAnimator;
    private Vector3 movement;
    private float verticalInput;
    private float horizontalInput;

    public int playerEnergy = 0;
    public Image imgEnergy;

    //Herramientas del jugador
    public GameObject playersWateringCan;
    public GameObject playersShovel;

    //Hace referencia al objeto que se quiere recoger
    InteractableObject selectedInteractable = null;

    private void Awake()
    {
        instance = this;
        imgEnergy.fillAmount = playerEnergy / 100;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        movement = new Vector3(-horizontalInput, 0, -verticalInput);
        movement.Normalize();

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if(movement != Vector3.zero)
        {
            transform.forward = movement;
            playerAnimator.SetBool("walk", true);
        }
        else
        {
            playerAnimator.SetBool("walk", false);
        }

        //PARA DEBUGGEAR SOLAMENTE
        //Adelantamos el tiempo
        if (Input.GetKey(KeyCode.F))
        {
            TimeManager.instance.Tick();
        }

        //Para consumir alimentos
        if (Input.GetKey(KeyCode.C))
        {
            ItemConsume();
        }

        //Item interaction
        if(Input.GetButtonDown("Fire2"))
        { 
            ItemInteract();
        }

        //Si se tiene herramienta equipada
        if(InventoryManager2.instance.equipedTool != null)
        {
            //Muestra herramientas en la mano del jugador
            if (InventoryManager2.instance.equipedTool.itemName.Equals("Pala"))
            {
                playersShovel.SetActive(true);
                playersWateringCan.SetActive(false);
            }
            else if (InventoryManager2.instance.equipedTool.itemName.Equals("Regadera"))
            {
                playersWateringCan.SetActive(true);
                playersShovel.SetActive(false);
            }
            else
            {
                playersWateringCan.SetActive(false);
                playersShovel.SetActive(false);
            }
        }
    }

    private void ItemConsume()
    {
        //Si se tiene algo en la mano,  y se da click en c, se consume
        if (InventoryManager2.instance.equipedItem != null)
        {
            //Se guarda el valor de energía que otorga
            int energyGain = InventoryManager2.instance.equipedItem.energyAmount;

            //Se elimina el item de la mano
            InventoryManager2.instance.equipedItem = null;

            //Actualiza el item en mano
            InventoryManager2.instance.RenderHandItem();

            PlayerStats.GainEnergy(energyGain);
        }
    }

    private void ItemInteract()
    {
        //Energía que le quedaría al jugador tras hacer la acción
        float playerEnergyAfterActing = PlayerStats.playerEnergy - 5;

        //Si se tiene algo en la mano, se deja en el inventario
        if (InventoryManager2.instance.equipedItem != null)
        {
            InventoryManager2.instance.HandToInventory(InventorySlot.InventoryType.Item);
            return;
        }

        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out mouseHit, 150f))
        {
            //Si el jugador tiene energía
            if(playerEnergyAfterActing >= 0)
            {
                if (mouseHit.transform.tag == "item" || mouseHit.transform.tag == "planta")
                {
                    //Animación de movimiento de mano
                    playerAnimator.SetTrigger("collectItem");

                    selectedInteractable = mouseHit.transform.GetComponent<InteractableObject>();

                    selectedInteractable.PickupItem();
                    //Gasta energía
                    PlayerStats.ConsumeEnergy();
                    //Si es planta, suena el efecto de cosechar
                    if (mouseHit.transform.tag == "planta")
                    {
                        AudioManager.instance.HarvestSound();
                    }
                }

                if (selectedInteractable != null)
                {
                    selectedInteractable = null;
                }

                if (mouseHit.transform.tag == "plantaMuerta")
                {
                    Destroy(mouseHit.transform.gameObject);
                    //Gasta energía
                    PlayerStats.ConsumeEnergy();

                    AudioManager.instance.ShovelSound();
                }
            }
            else if(playerEnergyAfterActing < 0)
            {
                UIManager.instance.PopUpWarning("¡No tienes más energía! Será mejor que comas algo...");
            }

            //Si seleccionamos el lago
            if(mouseHit.transform.tag == "waterSource")
            {
                //Rellenamos el agua
                PlayerStats.RefillWater();

                //Sonido de agua
                AudioManager.instance.WaterSound();
            }

            ////Si seleccionamos la "cama" (por ahora es la puerta de la casa)
            //if (mouseHit.transform.tag == "bed")
            //{
            //    //Mostramos el panel de dormir
            //    UIManager.instance.TriggerSleepPanel();
            //}
        }
    }
}
