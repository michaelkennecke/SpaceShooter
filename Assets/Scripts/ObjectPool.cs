using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int poolSize = 0;
    [SerializeField] bool willExpand = true;
    private List<GameObject> gameObjectPool;

    void Start()
    {
        gameObjectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject gObject = Instantiate(prefab) as GameObject;
            //Set the parent of the projectile to be the ProjectilePool
            gObject.transform.parent = this.gameObject.transform;
            gObject.SetActive(false);
            gameObjectPool.Add(gObject);
        }
    }

    // Do not pool out the object, because this would be costly as well
    // instead leave the object in the list and just return it.
    public GameObject GetPooledGameObject()
    {
        for (int i = 0; i < gameObjectPool.Count; i++)
        {
            if (!gameObjectPool[i].activeInHierarchy)
            {
                return gameObjectPool[i];
            }
        }
        if (willExpand)
        {
            GameObject gObject = Instantiate(prefab) as GameObject;
            //Set the parent of the projectile to be the ProjectilePool
            gObject.transform.parent = this.gameObject.transform;
            gameObjectPool.Add(gObject);
            return gObject;
        }
        return null;
    }
}
