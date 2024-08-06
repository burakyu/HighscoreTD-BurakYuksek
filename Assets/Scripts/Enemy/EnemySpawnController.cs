using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private float initialDelayBetweenEnemies = 5;

    private float _sessionEnemyDifficultyTimer;
    private float _spawnTimer;
    private bool _spawnStarted;

    private void Awake()
    {
        _spawnTimer = initialDelayBetweenEnemies;
    }

    private void OnEnable()
    {
        EventManager.FirstTowerPlaced.AddListener(StartEnemySpawn);
    }

    private void OnDisable()
    {
        EventManager.FirstTowerPlaced.RemoveListener(StartEnemySpawn);
    }

    private void Update()
    {
        if (!_spawnStarted) return;

        _sessionEnemyDifficultyTimer += Time.deltaTime;
        _spawnTimer += Time.deltaTime;

        float currentSpawnDelay = Mathf.Clamp(initialDelayBetweenEnemies / _sessionEnemyDifficultyTimer * 20, 1,
            initialDelayBetweenEnemies);
        
        if (_spawnTimer >= currentSpawnDelay)
        {
            _spawnTimer = 0;
            SpawnEnemy();
        }
    }

    private void StartEnemySpawn()
    {
        _spawnStarted = true;
    }

    private void SpawnEnemy()
    {
        Pools.Types enemyType = Pools.Types.EnemyTiny;
        int randomValueForEnemySelection = UnityEngine.Random.Range(0, 100);

        if (_sessionEnemyDifficultyTimer < 15)
        {
            if (randomValueForEnemySelection < 70)
                enemyType = Pools.Types.EnemyTiny;
            else if(randomValueForEnemySelection <= 100)
                enemyType = Pools.Types.EnemyDefault;
        }
        else if (_sessionEnemyDifficultyTimer < 60)
        {
            if (randomValueForEnemySelection < 25)
                enemyType = Pools.Types.EnemyTiny;
            else if(randomValueForEnemySelection < 85)
                enemyType = Pools.Types.EnemyDefault;
            else if(randomValueForEnemySelection <= 100)
                enemyType = Pools.Types.EnemyTroll;
        }
        else if (_sessionEnemyDifficultyTimer >= 60)
        { 
            if(randomValueForEnemySelection < 15)
                enemyType = Pools.Types.EnemyDefault;
            else if(randomValueForEnemySelection <= 100)
                enemyType = Pools.Types.EnemyTroll;
        }
        
        Enemy enemy = PoolManager.Instance.Spawn(enemyType).GetComponent<Enemy>();

        enemy.transform.position = this.transform.position;
        enemy.transform.LookAt(transform.forward);
    }
}
