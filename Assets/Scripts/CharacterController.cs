using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    private Rigidbody characterRB;
    public Animator playerAnimator;
    private Vector3 movement;
    private float verticalInput;
    private float horizontalInput;

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
    }
}
