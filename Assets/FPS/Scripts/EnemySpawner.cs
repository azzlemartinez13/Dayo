using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{


    
    [Header("Game Logic")]
    public GameController Game;
    public Base Base;
    public HealthComponent[] HPC;
    public int TotalEnemies;
    public int EnemiesKilled;

    [Header("Spawning")]
    public float SpawnDelay = 1f;
    public float SpawnRadius = 10f;
    public Spider SpiderPrefab1;
    public Spider SpiderPrefab2;
    public Spider SpiderPrefab3;

    private ObjectPool<Spider> _pool1;
    private ObjectPool<Spider> _pool2;
    private ObjectPool<Spider> _pool3;

    public event Action<int> OnEnemyKilled;

    private void Awake()
    {
        if (SpiderPrefab1 != null) _pool1 = new ObjectPool<Spider>(SpiderPrefab1, 10, transform);
        if (SpiderPrefab2 != null) _pool2 = new ObjectPool<Spider>(SpiderPrefab2, 10, transform);
        if (SpiderPrefab3 != null) _pool3 = new ObjectPool<Spider>(SpiderPrefab3, 10, transform);
    }

    public void SpawnEnemy(int level)
    {
        Spider spider = null;
        switch (level)
        {
            case 1: spider = _pool1?.Get(); break;
            case 2: spider = _pool2?.Get(); break;
            case 3: spider = _pool3?.Get(); break;
            default:
                Debug.LogWarning("Invalid spider level requested.");
                return;
        }

        if (spider != null)
        {
            // Find a spawn point on the NavMesh within the spawn radius
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * SpawnRadius;
            randomOffset.y = 0f;
            Vector3 spawnPosition = transform.position + randomOffset;

            if (NavMesh.SamplePosition(spawnPosition, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            {
                spider.transform.position = hit.position;
                spider.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No valid NavMesh position near spawn point.");
                return;
            }

            // Reset spider and register death callback
            spider.ResetSpider();
            spider.InitializeSpider(this, level, Base, HPC); // ✅ Now it has all 4 arguments
            spider.OnDeath -= HandleSpiderDeath;
            spider.OnDeath += HandleSpiderDeath;
        }
    }

    private void HandleSpiderDeath(Spider spider)
    {
        StartCoroutine(HandleDeathRoutine(spider));
    }

    private IEnumerator HandleDeathRoutine(Spider spider)
    {
        Transform model = spider.transform.GetChild(0);
        if (model != null) model.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        if (model != null) model.gameObject.SetActive(true);

        switch (spider.materialType)
        {
            case MaterialType.Wood:
                _pool1?.Return(spider);
                OnEnemyKilled?.Invoke(50);
                break;
            case MaterialType.Metal:
                _pool2?.Return(spider);
                OnEnemyKilled?.Invoke(250);
                break;
            default:
                _pool3?.Return(spider);
                OnEnemyKilled?.Invoke(1000);
                break;
        }

        EnemiesKilled++;

        if (EnemiesKilled >= TotalEnemies)
        {
            if (Base != null && Base.Health_Current > 0f)
                Game.GameWon();
            else
                Game.GameLost();
        }
    }

    public void DespawnAllSpiders()
    {
        _pool1?.ReturnAll();
        _pool2?.ReturnAll();
        _pool3?.ReturnAll();
    }
}
