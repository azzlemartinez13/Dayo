using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public GameController Game;
    public int MaxHealth;
    public int CurrentHealth;

    public event Action<HealthComponent> OnDeath;
    public event Action<float> OnDamageTaken;

    public MaterialType materialType;

    [Header("Hit Sounds")]
    public AudioSource woodHitSound;
    public AudioSource metalHitSound;
    public AudioSource characterHitSound;

    public HealthUI healthUI;

    //private void Awake()
    //{
    //    ResetHealth();
    //}

    //public void ResetHealth()
    //{
    //    CurrentHealth = MaxHealth;
    //}

    private void Start()
    {
        healthUI = FindObjectOfType<HealthUI>(); // Ensure this is assigned before use
                                                 //respawnPosition = transform.position;


        CurrentHealth = MaxHealth;
        healthUI.SetMaxHealth(MaxHealth); // Now this is safe to use
        healthUI.SetMaxHealth(CurrentHealth); // Now this is safe to use

        //buttonManager = FindObjectOfType<ButtonManager>();


    }

    public void RestoreHealth(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth); // Ensure health does not exceed max
        healthUI.SetHealth(CurrentHealth);
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

            if (healthUI != null)
            {
                healthUI.SetHealth(CurrentHealth); // Update UI when damage is taken
            }

        {
            OnDeath?.Invoke(this);
            Game?.GameLost();
        }
    }

    public int GetCurrentHealth()
    {
        return CurrentHealth;
    }
}
