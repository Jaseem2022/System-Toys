using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ability : MonoBehaviour
{   
    public enum PlayerAbilities{REWIND,BLINK,CLONE_SPAWN,CLONE_DESTROY};
   
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

    //class instance for cooldown
    private CoolDownManager coolDownManager;

    void OnRewind(InputValue value)
    {
        float speed = .95f;
        if (value.isPressed && coolDownManager.CanUse((int)PlayerAbilities.REWIND))
        {
            if (positionHistory.Count() > 0)
            {
                Vector3 past = positionHistory.Dequeue(); // one step back
                rb.MovePosition(Vector3.Lerp(transform.position, past, speed));

            }
            coolDownManager.TriggerCooldown((int)PlayerAbilities.REWIND);
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
        if (value.isPressed && coolDownManager.CanUse((int)PlayerAbilities.BLINK))
        {
            dashTimer = dashTime;
            coolDownManager.TriggerCooldown((int)PlayerAbilities.BLINK);
        }

    }

     void OnClone(InputValue value)
    {
        if (value.isPressed && coolDownManager.CanUse((int)PlayerAbilities.CLONE_SPAWN))
        {
            activeClone = Instantiate(playerClone, transform.position, Quaternion.identity);

            coolDownManager.TriggerCooldown((int)PlayerAbilities.CLONE_SPAWN);
            
            coolDownManager.TriggerCooldown((int)PlayerAbilities.CLONE_DESTROY);

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
        if (activeClone != null && coolDownManager.CanUse((int)PlayerAbilities.CLONE_DESTROY))
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
        coolDownManager.RegisterAbility((int)PlayerAbilities.REWIND, rewindCoolDown);
        coolDownManager.RegisterAbility((int)PlayerAbilities.BLINK, blinkCoolDown);
        coolDownManager.RegisterAbility((int)PlayerAbilities.CLONE_SPAWN, cloneCoolDown);
        coolDownManager.RegisterAbility((int)PlayerAbilities.CLONE_DESTROY, cloneTimeDestructionCoolDown);
    }

    void Update()
    {
        TrackPosition();
        TrackBlink();
        TrackClone(); 
    }

}
