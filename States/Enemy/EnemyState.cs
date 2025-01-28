using UnityEngine;
using System;

// Abstract Base State
public abstract class EnemyState
{
    public virtual void OnEnter(EnemyHandler enemyHandler) { }
    public virtual void OnExit() { }
    public abstract EnemyState OnUpdate(EnemyHandler enemyHandler); 
}

