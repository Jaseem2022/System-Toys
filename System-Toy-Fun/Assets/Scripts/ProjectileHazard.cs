using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileHazard : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] float projectileSpeed = 5f;

    //fix the logic tomorrow
    IEnumerator SpawnProjectiles()
    {
        while(true)
        {
            if (projectilePrefab != null && playerPrefab != null)
            {
                
                GameObject activeProjectile = Instantiate(projectilePrefab, transform.position, quaternion.identity);
                Vector3 towardsPlayer = (playerPrefab.transform.position - transform.position).normalized;
                activeProjectile.transform.Translate(towardsPlayer * projectileSpeed * Time.deltaTime);
            }


            yield return new WaitForSeconds(2f);
        }
    }

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(SpawnProjectiles());    
    }
}
