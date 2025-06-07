//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//public class EnemySpawnController : MonoBehaviour
//{
//    public EnemySpawner Spawner;

//    public Base Base;

//    public Wave[] Waves;

//    public bool AreAllSpawned;

//    public int CurrentWave;

//    public List<Spider> SpawnedEnemyList;

//    [SerializeField]
//    private TextMeshProUGUI _waveTMP;

//    [SerializeField]
//    private GameObject _buyWeaponGO;

//    //public event EventHandler OnEnemyDie;

//    public event EventHandler<int> OnWaveComplete;

//    private void Start()
//    {
//        int num = 0;
//        Wave[] waves = Waves;
//        for (int i = 0; i < waves.Length; i++)
//        {
//            Wave wave = waves[i];
//            int num2 = 0;
//            for (int j = 0; j < wave.EnemyAmounts.Length; j++)
//            {
//                num2 += wave.EnemyAmounts[j];
//            }
//            num += num2;
//        }
//        _waveTMP.text = $"Wave: {CurrentWave + 1}/{Waves.Length}";
//        _buyWeaponGO.SetActive(value: true);
//        Spawner.TotalEnemies = num;
//    }

//    public void ResetSpawner()
//    {
//        AreAllSpawned = false;
//        if (SpawnedEnemyList == null)
//        {
//            SpawnedEnemyList = new List<Spider>();
//        }
//        StopAllCoroutines();
//        CurrentWave = 0;
//        _waveTMP.text = $"Wave: {CurrentWave + 1}/{Waves.Length}";
//        foreach (Spider spawnedEnemy in SpawnedEnemyList)
//        {
//            spawnedEnemy.gameObject.SetActive(value: false);
//        }
//        _buyWeaponGO.SetActive(value: false);
//        StartCoroutine(StartWave(Waves[CurrentWave]));
//    }

//    public IEnumerator StartWave(Wave wave)
//    {
//        int numberRounds = wave.EnemyPrefabs.Length;
//        float timeBetweenRound = wave.RoundInterval;
//        for (int currentRoundNr = 0; currentRoundNr < numberRounds; currentRoundNr++)
//        {
//            float timeBetweenSpawn = wave.SpawnIntervals[currentRoundNr];
//            int numberOfEnemiesToSpawn = wave.EnemyAmounts[currentRoundNr];
//            Spider spider = wave.EnemyPrefabs[currentRoundNr];
//            int enemyLvl = spider.Level;
//            for (int currentEnemySpawned = 0; currentEnemySpawned < numberOfEnemiesToSpawn; currentEnemySpawned++)
//            {
//                Spawner.SpawnEnemy(enemyLvl);
//                yield return new WaitForSeconds(timeBetweenSpawn);
//            }
//            yield return new WaitForSeconds(timeBetweenRound);
//        }
//        if (CurrentWave < Waves.Length - 1)
//        {
//            CurrentWave++;
//            _waveTMP.text = $"Wave: {CurrentWave + 1}/{Waves.Length}";
//            this.OnWaveComplete?.Invoke(this, CurrentWave);
//            StartCoroutine(StartWave(Waves[CurrentWave]));
//        }
//        else
//        {
//            AreAllSpawned = true;
//        }
//    }
//}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public EnemySpawner Spawner;
    public Base Base;

    public Wave[] Waves;

    public bool AreAllSpawned;
    public int CurrentWave;

    public List<Spider> SpawnedEnemyList;

    [SerializeField] private TextMeshProUGUI _waveTMP;
    [SerializeField] private GameObject _buyWeaponGO;

    public event EventHandler<int> OnWaveComplete;

    private void Start()
    {
        int totalEnemies = 0;
        foreach (var wave in Waves)
        {
            foreach (var amount in wave.EnemyAmounts)
                totalEnemies += amount;
        }

        _waveTMP.text = $"Wave: {CurrentWave + 1}/{Waves.Length}";
        _buyWeaponGO.SetActive(true);
        Spawner.TotalEnemies = totalEnemies;
    }

    public void ResetSpawner()
    {
        AreAllSpawned = false;
        if (SpawnedEnemyList == null)
            SpawnedEnemyList = new List<Spider>();

        StopAllCoroutines();
        CurrentWave = 0;
        _waveTMP.text = $"Wave: {CurrentWave + 1}/{Waves.Length}";

        foreach (Spider spider in SpawnedEnemyList)
        {
            if (spider != null)
                spider.gameObject.SetActive(false);
        }

        _buyWeaponGO.SetActive(false);
        StartCoroutine(StartWave(Waves[CurrentWave]));
    }

    public IEnumerator StartWave(Wave wave)
    {
        int rounds = wave.EnemyPrefabs.Length;
        float timeBetweenRounds = wave.RoundInterval;

        for (int round = 0; round < rounds; round++)
        {
            float spawnInterval = wave.SpawnIntervals[round];
            int enemyCount = wave.EnemyAmounts[round];
            int enemyLevel = wave.EnemyPrefabs[round].Level;

            for (int i = 0; i < enemyCount; i++)
            {
                Spawner.SpawnEnemy(enemyLevel);

                yield return new WaitForSeconds(spawnInterval);
            }

            yield return new WaitForSeconds(timeBetweenRounds);
        }

        if (CurrentWave < Waves.Length - 1)
        {
            CurrentWave++;
            _waveTMP.text = $"Wave: {CurrentWave + 1}/{Waves.Length}";
            OnWaveComplete?.Invoke(this, CurrentWave);
            StartCoroutine(StartWave(Waves[CurrentWave]));
        }
        else
        {
            AreAllSpawned = true;
        }
    }
}

