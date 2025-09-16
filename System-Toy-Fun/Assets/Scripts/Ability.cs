using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ability : MonoBehaviour
{
   
    //rewind
    private Queue<Vector3> positionHistory = new Queue<Vector3>();
    [SerializeField] float rewindCoolDown = 5f;
    [SerializeField] float recordLimit = 5f;
    [SerializeField] float recordTimer = 0.0f;
    [SerializeField] float recordInterval = 0.2f;

    //blink
    [SerializeField] float blinkCoolDown = 3f;
    [SerializeField] float blinkBoost = 2.5f;
    float dashTimer = 0;
    float dashTime = 0.2f;
    PlayerMotion playerMotion;

    //clone
    [SerializeField] float cloneCoolDown = 5;
    [SerializeField] float cloneTimeDestructionCoolDown = 2.5f;
    private GameObject activeClone;
    [SerializeField] GameObject playerClone;



    //rigidbody
    Rigidbody rb;
    private CoolDownManager coolDownManager;

    void OnRewind(InputValue value)
    {
        float speed = .95f;
        if (value.isPressed && coolDownManager.CanUse("rewind"))
        {
            if (positionHistory.Count() > 0)
            {
                Vector3 past = positionHistory.Dequeue(); // one step back
                rb.MovePosition(Vector3.Lerp(transform.position, past, speed));

            }
            coolDownManager.TriggerCooldown("rewind");
        }
    }

    private void TrackPosition()
    {
        recordTimer += Time.deltaTime;

        //record position after every 0.2 sec
        if (recordTimer >= recordInterval)
        {
            positionHistory.Enqueue(transform.position);
            recordTimer = 0f;
        }

        //if more than 5 sec worth time is recorded pop from front
        if (positionHistory.Count > recordLimit / recordInterval)
        {
            positionHistory.Dequeue();
        }

    }

    void OnBlink(InputValue value)
    {
        if (value.isPressed && coolDownManager.CanUse("blink"))
        {
            dashTimer = dashTime;
            coolDownManager.TriggerCooldown("blink");
        }

    }

     void OnClone(InputValue value)
    {
        if (value.isPressed && coolDownManager.CanUse("clone_spawn"))
        {
            activeClone = Instantiate(playerClone, transform.position, Quaternion.identity);

            coolDownManager.TriggerCooldown("clone_spawn");
            
            coolDownManager.TriggerCooldown("clone_destruction");

        }

        
    }

    void TrackClone()
    {
        // Manual destroy with key
        if (activeClone != null && Input.GetKeyDown(KeyCode.C))
        {
            Destroy(activeClone);
            activeClone = null;
        }

        // Auto destroy when cooldown is done
        if (activeClone != null && coolDownManager.CanUse("clone_destruction"))
        {
            Destroy(activeClone);
            activeClone = null;
        }
    }

    void TrackBlink()
    {
        if (dashTimer > 0)
        {
            rb.MovePosition(rb.position + playerMotion.move * playerMotion.moveAcceleration * blinkBoost * Time.deltaTime);
            dashTimer -= Time.deltaTime;
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMotion = FindFirstObjectByType<PlayerMotion>();
        coolDownManager = FindFirstObjectByType<CoolDownManager>();
        coolDownManager.RegisterAbility("rewind", rewindCoolDown);
        coolDownManager.RegisterAbility("blink", blinkCoolDown);
        coolDownManager.RegisterAbility("clone_spawn", cloneCoolDown);
        coolDownManager.RegisterAbility("clone_destruction", cloneTimeDestructionCoolDown);
    }

    void Update()
    {
        TrackPosition();
        TrackBlink();
        TrackClone(); 
    }

}
