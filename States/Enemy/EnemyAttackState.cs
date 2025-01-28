using UnityEngine;
using System;
using DG.Tweening;

// Namespace for Attack States
namespace EnemyAttackState
{
    public class IdleState : EnemyState
    {
        private Rigidbody2D rb;
        private PlayerStats playerStats;
        private EnemyAnim enemyAnim;

        public IdleState(Rigidbody2D rb, PlayerStats playerStats, EnemyAnim enemyAnim)
        {
            this.rb = rb;
            this.playerStats = playerStats;
            this.enemyAnim = enemyAnim;
        }

        public override void OnEnter(EnemyHandler enemyHandler)
        {
            rb.linearVelocity = Vector2.zero; // Changed from `linearVelocity` to `velocity`
            enemyAnim.Stand();
        }

        public override EnemyState OnUpdate(EnemyHandler enemyHandler)
        {
            if (enemyHandler.Move)
            {
                return new MoveState(enemyHandler.distance, rb, playerStats, enemyAnim);
            }
            if (!enemyHandler.Alive)
            {
                return new DeadState(rb, enemyAnim);
            }
            return null; // Remain in this state
        }

        public override void OnExit()
        {
        }
    }

    public class StompState : EnemyState
    {
        private Rigidbody2D rb;
        private PlayerStats playerStats;
        public static event Action OnDamagedEnemy;
        private EnemyAnim enemyAnim;

        public StompState(Rigidbody2D rb, PlayerStats playerStats, EnemyAnim enemyAnim)
        {
            this.rb = rb;
            this.playerStats = playerStats;
            this.enemyAnim = enemyAnim;
        }

        public override void OnEnter(EnemyHandler enemyHandler)
        {
            Debug.Log("Stomp State Entered");
            // Play stomp animation
            if (enemyHandler.InPlayerTerritory)
            {
                playerStats.DecreaseHealth(5f);
                OnDamagedEnemy?.Invoke();
            }
            rb.linearVelocity = Vector2.zero; // Changed from `linearVelocity` to `velocity`
            enemyAnim.Stomp();
        }

        public override EnemyState OnUpdate(EnemyHandler enemyHandler)
        {
            if (enemyHandler.Idle)
            {
                return new IdleState(rb, playerStats, enemyAnim);
            }
            if (!enemyHandler.Alive)
            {
                return new DeadState(rb, enemyAnim);
            }
            return null; // Remain in this state
        }

        public override void OnExit()
        {
        }
    }

    public class MoveState : EnemyState
    {
        private Vector2 distance;
        private Rigidbody2D rb;
        private PlayerStats playerStats;
        private EnemyAnim enemyAnim;

        public MoveState(Vector2 distance, Rigidbody2D rb, PlayerStats playerStats, EnemyAnim enemyAnim)
        {
            this.distance = distance;
            this.rb = rb;
            this.playerStats = playerStats;
            this.enemyAnim = enemyAnim;
        }

        public override void OnEnter(EnemyHandler enemyHandler)
        {
            if (distance.x > 10f || distance.x < -10f)
            {
                distance.x = distance.x / 2f;
            }
            if (distance.x < 0)
            {
                enemyAnim.Flip(true);
            }
            else
            {
                enemyAnim.Flip(false);
            }
            rb.transform.DOMoveX(distance.x, 1.5f);
            enemyAnim.Walk();
        }

        public override EnemyState OnUpdate(EnemyHandler enemyHandler)
        {
            if (enemyHandler.Idle)
            {
                return new IdleState(rb, playerStats, enemyAnim);
            }
            if (enemyHandler.Stomp)
            {
                return new StompState(rb, playerStats, enemyAnim);
            }
            if (!enemyHandler.Alive)
            {
                return new DeadState(rb, enemyAnim);
            }
            return null; // Remain in this state
        }

        public override void OnExit()
        {
        }
    }

    public class DeadState : EnemyState
    {
        private Rigidbody2D rb;
        private EnemyAnim enemyAnim;

        public DeadState(Rigidbody2D rb, EnemyAnim enemyAnim)
        {
            this.rb = rb;
            this.enemyAnim = enemyAnim;
        }

        public override void OnEnter(EnemyHandler enemyHandler)
        {
            rb.linearVelocity = Vector2.zero; // Changed from `linearVelocity` to `velocity`
            rb.transform.rotation = Quaternion.Euler(0, 0, 90f);
            enemyAnim.Dead();

            // Change sprite and destroy self
        }

        public override EnemyState OnUpdate(EnemyHandler enemyHandler)
        {
            return null; // Remain in this state
        }

        public override void OnExit()
        {
        }
    }
}
