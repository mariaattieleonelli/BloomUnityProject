using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public static CharacterController instance;

    public float speed;
    private Rigidbody characterRB;
    public Animator playerAnimator;
    private Vector3 movement;
    private float verticalInput;
    private float horizontalInput;

    public int playerEnergy = 0;
    public Image imgEnergy;

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
    }

    public void IncreaseEnergy(int value)
    {
        playerEnergy += value;
        imgEnergy.fillAmount = playerEnergy / 100;
    }
}
