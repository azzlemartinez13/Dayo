//using System;
//using UnityEngine;

//public class CoinManager : MonoBehaviour
//{
//    public int Coin_Amount;

//    [SerializeField]
//    private EnemySpawner _enemySpawner;

//    public event Action<int> OnCoinAmountChange;

//    private void OnEnable()
//    {
//        _enemySpawner.OnEnemyKilled += _enemySpawner_OnEnemyKilled;
//    }

//    private void OnDisable()
//    {
//        _enemySpawner.OnEnemyKilled -= _enemySpawner_OnEnemyKilled;
//    }

//    private void Start()
//    {
//        ResetAllCoins();
//    }

//    public void ResetAllCoins()
//    {
//        Coin_Amount = 0;
//        this.OnCoinAmountChange?.Invoke(Coin_Amount);
//    }

//    public void AddCoins(int amount)
//    {
//        Coin_Amount += amount;
//        this.OnCoinAmountChange?.Invoke(Coin_Amount);
//    }

//    public void SpendCoins(int amount)
//    {
//        Coin_Amount -= amount;
//        this.OnCoinAmountChange?.Invoke(Coin_Amount);
//    }

//    private void _enemySpawner_OnEnemyKilled(int obj)
//    {
//        AddCoins(obj);
//    }
//}
