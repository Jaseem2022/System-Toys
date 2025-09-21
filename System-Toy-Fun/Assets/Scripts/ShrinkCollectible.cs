using NUnit.Framework;
using UnityEngine;

public class ShrinkCollectible : MonoBehaviour
{

    private PlayerSize playerSize;
    private float shrinkFactor = 0.60f;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerSize.IsShrunk())
            {
                playerSize.Shrink(shrinkFactor);
                Destroy(this.gameObject);                
            }
        }
    }

    void Start()
    {
        playerSize = FindFirstObjectByType<PlayerSize>();
    }

}
