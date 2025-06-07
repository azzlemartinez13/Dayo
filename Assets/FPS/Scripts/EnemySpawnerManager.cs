//using System.Collections;
//using UnityEngine;
//using UnityEngine.AI;

//public class SpiderSpawnerManager : MonoBehaviour
//{
//    [Header("Spawner Settings")]
//    public Spider spiderPrefab;
//    public int maxSpiders = 10;
//    public float spawnInterval = 5f;

//    [Header("Spawn Area")]
//    public Vector3 spawnAreaCenter;
//    public Vector3 spawnAreaSize = new Vector3(10, 0, 10);

//    [Header("Spider Target")]
//    public Transform house;
//    private Transform player;
//    public int level;

//    private int currentSpiderCount = 0;

//    private void Start()
//    {

       
//        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
//        if (playerObj != null)
//        {
//            player = playerObj.transform;
//        }
//        else
//        {
//            Debug.LogWarning("No object tagged 'Player' found.");
//        }
//        StartCoroutine(SpawnSpiderRoutine());
//    }

//    private IEnumerator SpawnSpiderRoutine()
//    {
//        while (true)
//        {
//            if (currentSpiderCount < maxSpiders)
//            {
//                SpawnEnemy(level);
//            }
//            yield return new WaitForSeconds(spawnInterval);
//        }
//    }

//    private void SpawnEnemy(int level)
//    {
//        Vector3 spawnPos = GetRandomPointInArea();

//        if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
//        {
//            spawnPos = hit.position;
//        }
//        else
//        {
//            Debug.LogWarning("No NavMesh found at spawn point.");
//            return;
//        }

//        Spider newSpider = Instantiate(spiderPrefab, spawnPos, Quaternion.identity);
//        NavAgentController controller = newSpider.GetComponent<NavAgentController>();
//        if (controller != null)
//        {
//            controller.follow = house;
//        }

//        NavMeshAgent agent = newSpider.GetComponent<NavMeshAgent>();
//        if (agent != null)
//        {
//            agent.enabled = false;
//            agent.enabled = true;
//        }

//        EnemySpawner spiderLogic = newSpider.GetComponent<EnemySpawner>();
//        if (spiderLogic != null)
//        {
//            spiderLogic.manager = this;
//        }

//        currentSpiderCount++;
//    }

//    public void OnSpiderDeath(EnemySpawner spider)
//    {
        
//        currentSpiderCount--;
//        Debug.Log("Spider died. Remaining: " + currentSpiderCount);
//    }

//    private Vector3 GetRandomPointInArea()
//    {
//        return new Vector3(
//            spawnAreaCenter.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
//            spawnAreaCenter.y,
//            spawnAreaCenter.z + Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
//        );
//    }

//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
//    }
//}
