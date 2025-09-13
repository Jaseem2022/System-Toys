using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveAcceleration = 5.0f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float recordLimit = 5f;
    [SerializeField] float recordTimer = 0.0f;
    [SerializeField] float recordInterval = 0.2f;
    [SerializeField] float blinkBoost = 2.5f;
    

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isJumped = false;
    private Vector3 move;
    private bool isBlinkPressed;
    private float dashTime = 0.2f;
    private float dashTimer = 0f;


    private Queue<Vector3> positionHistory = new Queue<Vector3>();
    private Vector3 initialPosition;

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

    void OnRewind(InputValue value)
    {
        float speed = .95f;
        if (value.isPressed)
        {
            if (positionHistory.Count() > 0)
            {
                Vector3 past = positionHistory.Dequeue(); // one step back
                rb.MovePosition(Vector3.Lerp(transform.position, past, speed));

            }
        }
    }

    void OnBlink(InputValue value)
    {
        isBlinkPressed = value.isPressed;
    }

    void Movement()
    {
        move = new Vector3(moveInput.x, 0, moveInput.y);

        if (isBlinkPressed)
        {
            dashTimer = dashTime;
            isBlinkPressed = false;
        }

        if (dashTimer > 0)
        {
            rb.MovePosition(rb.position + move * moveAcceleration * blinkBoost * Time.deltaTime);
            //isBlinkPressed = false;
            dashTimer -= Time.deltaTime;
        }
        else
        {
            rb.MovePosition(rb.position + move * moveAcceleration * Time.deltaTime);
        }
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


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;

    }

    void FixedUpdate()
    {
        TrackPosition();
    }

    private void TrackPosition()
    {
        recordTimer += Time.deltaTime;
        if (recordTimer >= recordInterval)
        {
            positionHistory.Enqueue(transform.position);
            recordTimer = 0f;
        }

        if (positionHistory.Count > recordLimit / recordInterval)
        {
            positionHistory.Dequeue();
        }
    }

    void Update()
    {
        Movement();
    }


}
