using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] int poolSize = 0;
    [SerializeField] bool willExpand = true;
    private List<GameObject> projectilePool;

    void Start()
    {
        projectilePool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab) as GameObject;
            //Set the parent of the projectile to be the ProjectilePool
            projectile.transform.parent = this.gameObject.transform;
            projectile.SetActive(false);
            projectilePool.Add(projectile);
        }
    }

    // Do not pool out the object, because this would be costly as well
    // instead leave the object in the list and just return it.
    public GameObject GetPooledProjectile()
    {
        for (int i = 0; i < projectilePool.Count; i++)
        {
            if (!projectilePool[i].activeInHierarchy)
            {
                return projectilePool[i];
            }
        }
        if (willExpand)
        {
            GameObject projectile = Instantiate(projectilePrefab) as GameObject;
            //Set the parent of the projectile to be the ProjectilePool
            projectile.transform.parent = this.gameObject.transform;
            projectilePool.Add(projectile);
            return projectile;
        }
        return null;
    }
}
