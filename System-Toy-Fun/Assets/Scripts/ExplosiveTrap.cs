using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ExplosiveTrap : MonoBehaviour
{

    [SerializeField] TMP_Text stunText;
    [SerializeField] float stunDuration = 3f;
    
    private PlayerMotion playerMotion;

    private bool isStunned = false;
    private float stunTimer = 0f;
    private float defaultPlayerSpeed;


    void MakeInvisible()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (var rend in renderers)
        {
            rend.enabled = false; // turn off rendering
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isStunned)
        {
            if (playerMotion != null)
            {
                isStunned = true;
                stunTimer = stunDuration;
                MakeInvisible();

                // immediately slow player
                playerMotion.moveAcceleration = defaultPlayerSpeed * 0.25f;
            }
            
        }

    }

    void Start()
    {
        playerMotion = FindFirstObjectByType<PlayerMotion>();
        defaultPlayerSpeed = playerMotion.moveAcceleration;
    }

    void Update()
    {
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                // restore speed
                playerMotion.moveAcceleration = defaultPlayerSpeed;
                isStunned = false;
                Destroy(this.gameObject);
            }
        }
    }

}
