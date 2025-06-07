////using System;
////using System.Collections;
////using UnityEngine;

////public class EnemySpawner : MonoBehaviour
////{
////    public GameController Game;
////    public int SpawnAmountOnStart;

////    public Spider SpiderPrefab1;
////    public Spider SpiderPrefab2;
////    public Spider SpiderPrefab3;

////    public Base Base;
////    public HealthComponent[] HPC;

////    [SerializeField] private float _spawnRadius = 10f;

////    private ObjectPool<Spider> _pool1;
////    private ObjectPool<Spider> _pool2;
////    private ObjectPool<Spider> _pool3;

////    public int EnemiesKilled;
////    public int TotalEnemies;

////    public event Action<int> OnEnemyKilled;

////    private void Awake()
////    {
////        //if (SpiderPrefab1 != null) _pool1 = new ObjectPool<Spider>(SpiderPrefab1, 10, transform);
////        //if (SpiderPrefab2 != null) _pool2 = new ObjectPool<Spider>(SpiderPrefab2, 10, transform);
////        //if (SpiderPrefab3 != null) _pool3 = new ObjectPool<Spider>(SpiderPrefab3, 10, transform);
////    }

////    public void SpawnEnemy(int level)
////    {
////        Spider spider = null;

////        switch (level)
////        {
////            case 1:
////                spider = _pool1?.Get();
////                break;
////            case 2:
////                spider = _pool2?.Get();
////                break;
////            case 3:
////                spider = _pool3?.Get();
////                break;
////            default:
////                Debug.LogWarning("Invalid spider level requested.");
////                return;
////        }

////        if (spider != null)
////        {
////            spider.InitializeSpider(level, Base, HPC);

////            //Vector2 rand = UnityEngine.Random.insideUnitCircle * _spawnRadius;
////            //spider.transform.position = new Vector3(rand.x, 1f, rand.y);

////            spider.OnDeath -= HandleSpiderDeath;
////            spider.OnDeath += HandleSpiderDeath;
////        }
////    }

////    private void HandleSpiderDeath(Spider spider)
////    {
////        StartCoroutine(OnDeath(spider));
////    }

////    private IEnumerator OnDeath(Spider spider)
////    {
////        Transform model = spider.transform.GetChild(0);
////        if (model != null) model.gameObject.SetActive(false);

////        yield return new WaitForSeconds(0.5f);

////        if (model != null) model.gameObject.SetActive(true);

////        switch (spider.materialType)
////        {
////            case MaterialType.Wood:
////                _pool1?.Return(spider);
////                OnEnemyKilled?.Invoke(50);
////                break;
////            case MaterialType.Metal:
////                _pool2?.Return(spider);
////                OnEnemyKilled?.Invoke(250);
////                break;
////            default:
////                _pool3?.Return(spider);
////                OnEnemyKilled?.Invoke(1000);
////                break;
////        }

////        EnemiesKilled++;

////        if (EnemiesKilled >= TotalEnemies)
////        {
////            if (Base != null && Base.Health_Current > 0f)
////                Game.GameWon();
////            else
////                Game.GameLost();
////        }
////    }

////    public void DespawnAllSpiders()
////    {
////        _pool1?.ReturnAll();
////        _pool2?.ReturnAll();
////        _pool3?.ReturnAll();
////    }
////}
//using System;
//using System.Collections;
//using System.Numerics;
//using UnityEngine;
//using UnityEngine.AI;
//using UnityEngine.UIElements;

//public class EnemySpawner : MonoBehaviour
//{
//    public GameController Game;
//    public int SpawnAmountOnStart; // Number of enemies to spawn when the game starts (not used here yet)

//    /*  public Transform Player;*/
//    public float SpawnDelay = 1f;
//    public Spider SpiderPrefab1;
//    public Spider SpiderPrefab2;
//    public Spider SpiderPrefab3;

//    public Base Base;
//    public HealthComponent[] HPC;

//    public int EnemiesKilled;
//    public int TotalEnemies;


//    [SerializeField]
//    private float _spawnRadius; // How far from the spawner enemies can appear (randomly)

//    private ObjectPool<Spider> _pool1;
//    private ObjectPool<Spider> _pool2;
//    private ObjectPool<Spider> _pool3;

//    public event Action<int> OnEnemyKilled;


//    //private NavMeshTriangulation Triangulation;

//    private void Awake()
//    {
//        if (SpiderPrefab1 != null) _pool1 = new ObjectPool<Spider>(SpiderPrefab1, 10, transform);
//        if (SpiderPrefab2 != null) _pool2 = new ObjectPool<Spider>(SpiderPrefab2, 10, transform);
//        if (SpiderPrefab3 != null) _pool3 = new ObjectPool<Spider>(SpiderPrefab3, 10, transform);


//    }

//    public void SpawnEnemy(int level)
//    {
//        Spider spider = null;
//        switch (level)
//        {
//            case 1: spider = _pool1?.Get(); break;
//            case 2: spider = _pool2?.Get(); break;
//            case 3: spider = _pool3?.Get(); break;
//            default:
//                Debug.LogWarning("Invalid spider level requested.");
//                return;
//        }

//        if (spider != null)
//        {
//            UnityEngine.Vector3 spawnPoint = transform.position;



//            if (NavMesh.SamplePosition(spawnPoint, out NavMeshHit hit, 10f, NavMesh.AllAreas))
//            {
//                spawnPoint = hit.position;
//                spider.transform.position = spawnPoint;
//                spider.gameObject.SetActive(true);

//            }
//            else
//            {
//                Debug.LogWarning("No NavMesh found near spawn position. Enemy not spawned.");
//            }
//        }
//        //UnityEngine.Vector3 spawnPoint = transform.position;

//        ////int VertexIndex = UnityEngine.Random.Range(0, Triangulation.vertices.Length);

//        ////NavMeshHit Hit;
//        //if (NavMesh.SamplePosition(spawnPoint, out NavMeshHit hit, 2f, NavMesh.AllAreas))
//        //    //if (NavMesh.SamplePosition(Triangulation.vertices[VertexIndex], out Hit, 2f, -1))
//        //    {
//        //    //spider.Agent.Warp(Hit.position);
//        //    //spider.Agent.enabled = true;
//        //     spider.transform.position = hit.position; //// Snap to NavMesh
//        //}

//        // else
//        //{
//        //Debug.LogWarning("Failed to find valid NavMesh position. Spider will spawn at fallback position.");
//        //    spider.transform.position = spawnPoint; //// Fallback: use exact transform position
//        //}

//        //spider.InitializeSpider(level, Base, HPC);
//        spider.OnDeath -= HandleSpiderDeath;
//        spider.OnDeath += HandleSpiderDeath;

//        //// Spawn enemy at a random position within the spawn radius(on XZ plane, Y is height)
//        //UnityEngine.Vector2 randomPos = UnityEngine.Random.insideUnitCircle * _spawnRadius;

//        //spider.transform.localPosition = new UnityEngine.Vector3(randomPos.x, 1f, randomPos.y);
//        //// Reset enemy state (custom function inside Enemy script)
//        spider.ResetSpider();
//    }



//    private void HandleSpiderDeath(Spider spider)
//    {
//        StartCoroutine(OnDeath(spider));
//    }

//    private IEnumerator OnDeath(Spider spider)
//    {
//        Transform model = spider.transform.GetChild(0);
//        if (model != null) model.gameObject.SetActive(false);

//        yield return new WaitForSeconds(0.5f);

//        if (model != null) model.gameObject.SetActive(true);

//        switch (spider.materialType)
//        {
//            case MaterialType.Wood:
//                _pool1?.Return(spider);
//                OnEnemyKilled?.Invoke(50);
//                break;
//            case MaterialType.Metal:
//                _pool2?.Return(spider);
//                OnEnemyKilled?.Invoke(250);
//                break;
//            default:
//                _pool3?.Return(spider);
//                OnEnemyKilled?.Invoke(1000);
//                break;
//        }

//        EnemiesKilled++;

//        if (EnemiesKilled >= TotalEnemies)
//        {
//            if (Base != null && Base.Health_Current > 0f)
//                Game.GameWon();
//            else
//                Game.GameLost();
//        }
//    }

//    public void DespawnAllSpiders()
//    {
//        _pool1?.ReturnAll();
//        _pool2?.ReturnAll();
//        _pool3?.ReturnAll();
//    }
//}