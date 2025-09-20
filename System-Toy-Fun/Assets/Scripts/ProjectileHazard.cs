using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileHazard : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] float projectileSpeed = 5f;

    GameObject activeProjectile;
    void TrackPlayer()
    {
        if (activeProjectile &&
            (activeProjectile.transform.position - playerPrefab.transform.position)
            .sqrMagnitude <= 0.01f
            )
        {
            Destroy(activeProjectile);
            activeProjectile = null;
        }
        else if(activeProjectile)
        {
            Vector3 towardsPlayer = Vector3.MoveTowards(activeProjectile.transform.position,
                                                            playerPrefab.transform.position,
                                                            projectileSpeed * Time.deltaTime);
            activeProjectile.transform.position = towardsPlayer;
        }
    }

    void Start()
    {
        activeProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {
        TrackPlayer();

    }


}
