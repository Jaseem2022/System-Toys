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
    [SerializeField] float blinkCoolDown = 2f;
    [SerializeField] float rewindCoolDown = 5f;
    [SerializeField] GameObject playerClone;
    [SerializeField] float cloneTimeCoolDown = 5f;
    [SerializeField] float cloneTimeDestructionCoolDown = 2.5f;
     

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isJumped = false;
    private Vector3 move;
    private bool isBlinkPressed;
    private float dashTime = 0.2f;
    private float dashTimer = 0f;
    private bool canUseBlink = true;
    private bool canUseRewind = true;
    private bool canUseClone = true;
    //private bool canDestroyClone = false;
    private float blinkTimer = 0f;
    private float rewindTimer = 0f;
    private float cloneTimer = 0f;
    private float cloneDestructionTimer = 0f;
    private GameObject activeClone; // runtime instance

    
    

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
        if (value.isPressed && canUseRewind)
        {
            if (positionHistory.Count() > 0)
            {
                Vector3 past = positionHistory.Dequeue(); // one step back
                rb.MovePosition(Vector3.Lerp(transform.position, past, speed));

            }
            rewindTimer = rewindCoolDown;
            canUseRewind = false;
        }
    }

    void OnBlink(InputValue value)
    {
        isBlinkPressed = value.isPressed;
    }

    void OnClone(InputValue value)
    {
        if (value.isPressed && canUseClone)
        {
            activeClone = Instantiate(playerClone, transform.position, Quaternion.identity);

            cloneTimer = cloneTimeCoolDown;
            cloneDestructionTimer = cloneTimeDestructionCoolDown;
            canUseClone = false;


            //canDestroyClone = true;
        }

        
    }

    void Movement()
    {
        move = new Vector3(moveInput.x, 0, moveInput.y);

        if (isBlinkPressed && canUseBlink)
        {
            dashTimer = dashTime;
            blinkTimer = blinkCoolDown;
            canUseBlink = false;
        }

        if (dashTimer > 0)
        {
            rb.MovePosition(rb.position + move * moveAcceleration * blinkBoost * Time.deltaTime);
            dashTimer -= Time.deltaTime;
        }
        else
        {
            rb.MovePosition(rb.position + move * moveAcceleration * Time.deltaTime);
        }

        isBlinkPressed = false; //to prevent ghost input
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

    void TrackCoolDown()
    {
        if (activeClone != null && Input.GetKeyDown(KeyCode.C))
        {
            Destroy(activeClone);
        }

        if (blinkTimer > 0) { blinkTimer -= Time.deltaTime; }

        if (rewindTimer > 0) { rewindTimer -= Time.deltaTime; }

        if (cloneTimer > 0) { cloneTimer -= Time.deltaTime; }

        if(cloneDestructionTimer > 0) { cloneDestructionTimer -= Time.deltaTime; }

        if (blinkTimer <= 0)
        {
            blinkTimer = Mathf.Max(0, blinkTimer);
            canUseBlink = true;
        }

        if (rewindTimer <= 0)
        {
            rewindTimer = Mathf.Max(0, rewindTimer);
            canUseRewind = true;
        }

        //timer to destroy the clone
        if (cloneDestructionTimer <= 0)
        {
            cloneDestructionTimer = Mathf.Max(0, cloneDestructionTimer);

            if (playerClone != null)
            {
                Destroy(activeClone);
            }
        }

        if (cloneTimer <= 0)
        {
            cloneTimer = Mathf.Max(0, cloneTimer);
            canUseClone = true;
        }
    }

    void FixedUpdate()
    {
        TrackPosition();
    }

    void Update()
    {
        Movement();
        TrackCoolDown();

    }


}
