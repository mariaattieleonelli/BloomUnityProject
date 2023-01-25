using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    private Rigidbody characterRB;
    private Vector3 movement;
    private float verticalInput;
    private float horizontalInput;

    private void Start()
    {
        
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
        }
    }
}
