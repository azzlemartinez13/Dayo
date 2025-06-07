using System;
using UnityEngine;

public class Base : MonoBehaviour
{
    public GameController Game;

    public float Health_Start;

    public float Health_Current;
    [SerializeField] private BaseUI baseUI;

    //public int BaseUpgradges;

    //public int RepairmenAmount;

    //public int HitmenAmount;

    public event Action OnHealthChanged;


    private void Awake()
    {
        Health_Current = Health_Start;
    }

 
    private void Start()
    {
       baseUI = FindObjectOfType<BaseUI>(); // Ensure this is assigned before use
                                                 


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

    public void RestoreHealth(float amount)
    {
        Health_Current = Mathf.Min(Health_Current + amount, Health_Start);
        OnHealthChanged?.Invoke();
    }


}
