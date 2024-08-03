using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private float initialDelay = 2;
    [SerializeField] private float delayBetweenEnemies = 5;

    private void Start()
    {
        StartEnemySpawn();
    }

    private void StartEnemySpawn()
    {
        StartCoroutine(EnemySpawnCoroutine());
    }
    
    private IEnumerator EnemySpawnCoroutine()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(delayBetweenEnemies);
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy;
        enemy = PoolManager.Instance.Spawn(Pools.Types.EnemyDefault)
            .GetComponent<Enemy>();

        enemy.transform.position = this.transform.position;
        enemy.transform.LookAt(transform.forward);
    }
}
