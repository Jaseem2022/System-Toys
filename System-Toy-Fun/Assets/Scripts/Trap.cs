using NUnit.Framework;
using UnityEngine;

public class Trap : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float trapTime = 4f;

    private Color activeColor = Color.red;
    private bool isPlayerTrapped = false;
    float trapTimer = 0f;
    private float trapHeight = 1f;

    private Renderer rend;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            isPlayerTrapped = true;

            Vector3 trapCenter = transform.position;
            
            Vector3 oldPlayerPosition = other.transform.position;
            Vector3 newPlayerPosition = new Vector3(trapCenter.x,
                                                   trapCenter.y + (trapHeight/2f) ,
                                                   trapCenter.z);
            other.transform.position = Vector3.Lerp(oldPlayerPosition,newPlayerPosition,0.95f);


            if (rend != null)
            {
                rend.material.color = activeColor;
                Debug.LogWarning("You are Trapped!");
            }
        }
    }

    private void CheckPlayerTrapped()
    {
        if (isPlayerTrapped)
        {
            trapTimer += Time.deltaTime;
            if (trapTimer >= trapTime)
            {
                rb.constraints = RigidbodyConstraints.None;
                isPlayerTrapped = false;
                Destroy(gameObject);
            }

        }
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    
    void Update()
    {
        CheckPlayerTrapped();
    }

}
