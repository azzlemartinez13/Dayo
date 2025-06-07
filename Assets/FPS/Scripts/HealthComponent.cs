using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public GameController Game;
    public float MaxHealth;
    public float CurrentHealth;

    public event Action<HealthComponent> OnDeath;
    public event Action<float> OnDamageTaken;

    public MaterialType materialType;

    [Header("Hit Sounds")]
    public AudioSource woodHitSound;
    public AudioSource metalHitSound;
    public AudioSource characterHitSound;

    private void Awake()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        OnDamageTaken?.Invoke(damage);

        switch (materialType)
        {
            case MaterialType.Wood: woodHitSound?.Play(); break;
            case MaterialType.Metal: metalHitSound?.Play(); break;
            case MaterialType.Skin: characterHitSound?.Play(); break;
        }

        if (CurrentHealth <= 0f)
        {
            OnDeath?.Invoke(this);
            Game?.GameLost();
        }
    }
}
