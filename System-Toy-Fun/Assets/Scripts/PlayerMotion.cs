using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMotion : MonoBehaviour
{
    [SerializeField] public float moveAcceleration = 5f;
    [SerializeField] float jumpForce = 2.5f;

    Rigidbody rb;
    Vector2 moveInput;
    public Vector3 move;

    private bool isJumped = false;

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isJumped && value.isPressed)
        {
            Jump();
        }
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isJumped = false;
            Debug.Log("Back on ground!");
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Movement();
    }
}
