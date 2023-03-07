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

        if(Input.GetButtonDown("Fire2"))
        { 
            ItemInteract();
        }
    }

    private void ItemInteract()
    {
        //Si se tiene algo en la mano, se deja en el inventario
        if (InventoryManager2.instance.equipedItem != null)
        {
            InventoryManager2.instance.HandToInventory(InventorySlot.InventoryType.Item);
            return;
        }

        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out mouseHit, 150f))
        {

            if (mouseHit.transform.tag == "item" || mouseHit.transform.tag == "planta")
            {
                selectedInteractable = mouseHit.transform.GetComponent<InteractableObject>();
                
                selectedInteractable.PickupItem();
                //Si es planta, suena el efecto de cosechar
                if(mouseHit.transform.tag == "planta")
                {
                    AudioManager.instance.HarvestSound();
                }
            }

            if (selectedInteractable != null)
            {
                selectedInteractable = null;
            }
        }
    }


    //Pertenece al primer intento
    //public void IncreaseEnergy(int value)
    //{
    //    playerEnergy += value;
    //    imgEnergy.fillAmount = playerEnergy / 100;
    //}

}
