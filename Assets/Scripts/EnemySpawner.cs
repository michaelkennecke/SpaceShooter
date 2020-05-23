using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] ObjectPool enemyPool;
    [SerializeField] float spawnTime = 3f;
    private float spawnTimer;

    void Start()
    {
        //this.SpawnEnemy();
        this.spawnTimer = spawnTime;
    }

    void Update()
    {
        //Invoke("SpawnEnemy", spawnTime);
        if (this.spawnTimer > 0)
        {
            this.spawnTimer -= Time.deltaTime;
        }
        if (this.spawnTimer <= 0)
        {
            this.SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = this.enemyPool.GetPooledGameObject();
        if (enemy == null) return;
        enemy.transform.position = this.transform.position;
        enemy.SetActive(true);
        this.spawnTimer = this.spawnTime;
    }
}
