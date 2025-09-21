using UnityEngine;
public class DiamondPickup : MonoBehaviour
{

    DiamondCountManager diamondCountManager;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            diamondCountManager.IncrementCount();  
            Destroy(this.gameObject);
        }  
    }
   
    void Start()
    {
        diamondCountManager = FindFirstObjectByType<DiamondCountManager>();
    }

}
