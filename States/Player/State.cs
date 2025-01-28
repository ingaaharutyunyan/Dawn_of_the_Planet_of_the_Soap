using UnityEngine;
using System;

// Abstract Base State
public abstract class State
{
    public virtual void OnEnter(PlayerAnim playerAnim) { }
    public virtual void OnExit() { }
    public abstract State OnUpdate(InputHandler inputHandler);
    public abstract State OnUpdate(InputHandler inputHandler, EnemyHealth enemyHealth);
}

