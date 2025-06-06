using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    public GameController Game;

    public float MaxHealth = 100f;

    public float CurrentHealth;

    public event Action<HealthComponent> OnDeath;

    public event Action<float> OnDamageTaken;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        this.OnDamageTaken?.Invoke(damage);
        if (CurrentHealth <= 0f)
        {
            this.OnDeath?.Invoke(this);
            Game.GameLost();
        }
    }
}
