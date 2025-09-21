using UnityEngine;

public class ExpandCollectible : MonoBehaviour
{
    private PlayerSize playerSize;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (playerSize.IsShrunk())
            {
                playerSize.Expand();
                playerSize.Expand();
                Destroy(this.gameObject);
            }

        }
    }

    void Start()
    {
        playerSize = FindFirstObjectByType<PlayerSize>();
    }
}
