using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveAcceleration = 5.0f;
    [SerializeField] float jumpForce = 5.0f;

    private Rigidbody rb;

    private Vector2 moveInput;
    private bool isJumped = false;
    private Vector3 move;

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Movement()
    {
        move = new Vector3(moveInput.x, 0, moveInput.y);
        rb.MovePosition(rb.position + move * moveAcceleration * Time.deltaTime);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumped = true;
        Debug.Log("Jumped");
    }

    void OnJump(InputValue value)
    {
        if(!isJumped && value.isPressed)
        {
            Jump();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isJumped = false;
            Debug.Log("Back on ground!");
        }
    }


    void Awake() 
    {
        rb = GetComponent<Rigidbody>();    
    }

    void Update()
    {
        Movement();
    }

   
}
