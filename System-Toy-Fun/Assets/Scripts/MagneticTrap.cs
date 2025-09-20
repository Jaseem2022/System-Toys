using System;
using Unity.VisualScripting;
using UnityEngine;

//check the player in range
//pull the player to trap(Vector3.MoveTowards)
//player could resist movement
//the trap gets active for a period of time
//then goes into cooldown

public class MagneticTrap : MonoBehaviour
{
    private Rigidbody playerRigidBody;

    [SerializeField] float magneticIntensity = 20f;
    [SerializeField] float magnetismDuration = 5f;
    [SerializeField] float magneticTimer = 0;
    private float defaultPlayerSpeed;
    private PlayerMotion playerMotion;


    void SetReferences(Collider other)
    {
        if (playerRigidBody == null)
        {
            playerRigidBody = other.GetComponent<Rigidbody>();

        }

        if (playerMotion == null)
        {
            playerMotion = FindFirstObjectByType<PlayerMotion>();
        }
    }

    void PullTowardsMagnet()
    {
        playerMotion.moveAcceleration = defaultPlayerSpeed * 0.25f;
        Vector3 towardsTrap = Vector3.MoveTowards(playerRigidBody.position,
                                                      transform.position,
                                                      magneticIntensity * Time.deltaTime);
        playerRigidBody.MovePosition(Vector3.Lerp(playerRigidBody.position,
                                                  towardsTrap,
                                                  magneticIntensity * Time.deltaTime));
    }

    //derefernce and revert changed parameters of other scripts back to normal values
    void RewindReferences()
    {
        if (playerMotion != null) { playerMotion.moveAcceleration = defaultPlayerSpeed; }

        playerRigidBody = null;
        playerMotion = null;
    }
    void TrackCooldDown()
    {
        if (magneticTimer <= 0)
        {
            magneticTimer = magnetismDuration;
        }
        magneticTimer = Math.Max(0, magneticTimer - Time.deltaTime);

    }
    void SetInitialReferences()
    {
        playerMotion = FindFirstObjectByType<PlayerMotion>();
        defaultPlayerSpeed = playerMotion.moveAcceleration;
        magneticTimer = magnetismDuration;
    }
    void OnTriggerEnter(Collider other)
    {
        //this helps to call OntriggerEnter continously 
        if (other.tag == "Player" && magneticTimer > 0f)
        {
            SetReferences(other);
            PullTowardsMagnet();
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RewindReferences();
        }
    }

    void Start()
    {
        SetInitialReferences();
    }


    void Update()
    {
        TrackCooldDown();
    }
}
