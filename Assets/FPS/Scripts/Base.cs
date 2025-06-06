using System;
using UnityEngine;

public class Base : MonoBehaviour
{
    public GameController Game;

    public float Health_Start;

    public float Health_Current;

    public int BaseUpgradges;

    public int RepairmenAmount;

    public int HitmenAmount;

    public event Action OnHealthChanged;


    private void Awake()
    {
        Health_Current = Health_Start;
    }

 
    private void Start()
    {
        ResetBase();
    }

    public void ResetBase()
    {
        Health_Current = Health_Start;
        this.OnHealthChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        Health_Current = Mathf.Max(Health_Current - damage, 0f);
        this.OnHealthChanged?.Invoke();
        if (Health_Current == 0f)
        {
            Game.GameLost();
        }
    }

    public void BuyUpgrade()
    {
        BaseUpgradges++;
        Health_Current = Health_Start + (float)(BaseUpgradges * 100);
        this.OnHealthChanged?.Invoke();
    }

    public void Repair(float amount)
    {
        Health_Current = Mathf.Min(Health_Start + (float)(BaseUpgradges * 100), Health_Current + 50f);
        this.OnHealthChanged?.Invoke();
    }
}
