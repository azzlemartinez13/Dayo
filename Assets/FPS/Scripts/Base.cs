using System;
using UnityEngine;

public class Base : MonoBehaviour
{
    public GameController Game;

    public float Health_Start = 100f;
    public float Health_Current;

    [SerializeField] private BaseUI baseUI;

    public event Action OnHealthChanged;

    private void Awake()
    {
        Health_Current = Health_Start;
    }

    private void Start()
    {
        if (baseUI == null)
        {
            baseUI = FindObjectOfType<BaseUI>();
        }

        if (baseUI != null)
        {
            OnHealthChanged += baseUI.UpdateHealthUI;
            baseUI.SetBase(this);
        }
        else
        {
            Debug.LogError("BaseUI not found in scene.");
        }

        ResetBase();
    }

    public void ResetBase()
    {
        Health_Current = Health_Start;
        OnHealthChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        Health_Current = Mathf.Max(Health_Current - damage, 0f);
        OnHealthChanged?.Invoke();

        if (Health_Current == 0f)
        {
            Game?.GameLost();
        }
    }

    public void RestoreHealth(float amount)
    {
        Health_Current = Mathf.Min(Health_Current + amount, Health_Start);
        OnHealthChanged?.Invoke();
    }
}
