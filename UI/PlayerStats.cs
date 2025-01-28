using UnityEngine;
using System;
using System.Collections;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] public float health { get; private set; }
    [SerializeField] public float pp { get; private set; }

    public void InitHealthAndPP()
    {
        health = 50f;
        pp = 1f;
    }

    public void IncreaseHealth(float amount)
    {
        
        if (health > 50f) return;
        health += amount;
        OnHealthChanged?.Invoke(health);
    }

    public void DecreaseHealth(float amount)
    {
        Debug.Log($"DecreaseHealth called with amount {amount}");
        if (health <= 0f) OnDied?.Invoke();
        health -= amount;
        OnHealthChanged?.Invoke(health);
    }

    public void IncreasePP(float amount)
    {
        if (pp >= 30f) return;
        pp += amount;
        OnPPChanged?.Invoke(pp);
    }

    public void DecreasePP(float amount)
    {
        if (pp <= 1f) return;
        pp -= amount;
        OnPPChanged?.Invoke(pp);
    }

    public event Action OnDied;
    public event Action<float> OnHealthChanged, OnPPChanged;

}
