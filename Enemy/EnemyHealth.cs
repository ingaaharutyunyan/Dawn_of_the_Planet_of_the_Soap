using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private PlayerStats playerStats;
    public static event Action OnEnemyDead;
    public bool Dead { get; private set; }
    public void TakeDamage()
    {
        if (health <= 0f) {
            OnEnemyDead?.Invoke();
            Dead = true;
            return;
        }
        health -= playerStats.pp;
    }
}