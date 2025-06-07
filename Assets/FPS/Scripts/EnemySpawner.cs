using System.Collections;
using UnityEngine;
using UnityEngine.AI;




public class EnemySpawner : MonoBehaviour
{


    [Header("Spawner Settings")]
    public Spider spiderPrefab1;              // Assign your Spider prefab here
    public int maxSpiders = 10;              // Max number of spiders in scene
    public float spawnInterval = 5f;         // Time between spawns

    [Header("Attack Settings")]
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private int attackDamage = 10;

    [Header("Spawn Area")]
    public Vector3 spawnAreaCenter;
    public Vector3 spawnAreaSize = new Vector3(10, 0, 10);

    private int currentSpiderCount = 0;

    public int TotalEnemies;
    public Base baseTarget;
    public int Level;
    public Transform house;
    private Transform player;


    public HealthComponent healthComponent;



    private void Start()
    {

        StartCoroutine(SpawnSpiderRoutine());
    }



    private IEnumerator SpawnSpiderRoutine()
    {
        while (true)
        {
            if (currentSpiderCount < maxSpiders)
            {
                SpawnEnemy(1);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SpawnEnemy(int level)
    {
        if (currentSpiderCount >= maxSpiders)
            return;
        Vector3 spawnPosition = GetRandomPointInArea();

        // Check if the position is on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPosition, out hit, 2f, NavMesh.AllAreas))
        {
            spawnPosition = hit.position;
        }
        else
        {
            Debug.LogWarning("Could not find a NavMesh position to spawn spider.");
            return;
        }

        Spider newSpider = Instantiate(spiderPrefab1, spawnPosition, Quaternion.identity);
        newSpider.GetComponent<NavAgentController>().follow = house;
        // Re-enable NavMeshAgent to force nav update
        NavMeshAgent agent = newSpider.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false;
            agent.enabled = true;
        }

    }

    private void OnSpiderDeath(Spider spider)
    {
        currentSpiderCount--;
        spider.OnDeath -= OnSpiderDeath;
    }

    private Vector3 GetRandomPointInArea()
    {
        Vector3 randomPos = new Vector3(
            spawnAreaCenter.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            spawnAreaCenter.y,
            spawnAreaCenter.z + Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
        return randomPos;
    }

    // Optional: Gizmos to visualize spawn area in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
    }
}

