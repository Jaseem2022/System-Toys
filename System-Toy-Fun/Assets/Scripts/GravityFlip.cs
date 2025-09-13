using UnityEngine;
using UnityEngine.InputSystem;

public class GravityFlip : MonoBehaviour
{

    private bool isGravityFlipped = false;
    void OnGravityFlip(InputValue value)
    {
        isGravityFlipped = !isGravityFlipped;
        Vector3 targetGravity = new Vector3(0, 1f, 0);
        Vector3 normalGravity = new Vector3(0, -9.81f, 0);

        if (isGravityFlipped)
        {
            Physics.gravity = Vector3.Lerp(Physics.gravity, targetGravity, 1f);
        }
        else
        {
            Physics.gravity = Vector3.Lerp(Physics.gravity, normalGravity, 1f);
        }

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
