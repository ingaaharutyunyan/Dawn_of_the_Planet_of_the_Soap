using UnityEngine;
using System;
// Namespace for Attack States
namespace Attack
{
    public class IdleState : State
    {
        public override void OnEnter(PlayerAnim playerAnim)
        {
            playerAnim.Stand();
        }

        public override State OnUpdate(InputHandler inputHandler, EnemyHealth enemyHealth)  
        {
            if (inputHandler.PunchPressed)
            {
                return new PunchState();
            }
            else if (inputHandler.BubblePressed)
            {
                return new BubbleState();
            }
            return null; // Remain in this state
        }

        public override State OnUpdate(InputHandler inputHandler){ return null;}

        public override void OnExit()
        {
        }
    }

    public class PunchState : State
    {
        public override void OnEnter(PlayerAnim playerAnim)
        {
            playerAnim.Punch();
        }

        public override State OnUpdate(InputHandler inputHandler, EnemyHealth enemyHealth)
        {
            Debug.Log("Entered Punch State");
            if (!inputHandler.PunchPressed)
            {
                return new IdleState();
            }
            if (inputHandler.GetEnemyHealth() != null)
            {
                Debug.Log("Enemy Hit");
                enemyHealth.TakeDamage();
            }
            return null; // Replace with state transition
        }

        public override State OnUpdate(InputHandler inputHandler){ return null;}

        public override void OnExit()
        {
        }
    }

    public class BubbleState : State
    {
        public override void OnEnter(PlayerAnim playerAnim)
        {
            playerAnim.Bubble();
        }

        public override State OnUpdate(InputHandler inputHandler, EnemyHealth enemyHealth)
        {
            if (!inputHandler.BubblePressed)
            {
                return new IdleState();
            }
            return null; // Replace with state transition
        }

        public override State OnUpdate(InputHandler inputHandler){ return null;}

        public override void OnExit()
        {
        }
    }

    public class HarvestFatState : State
    {
        public override void OnEnter(PlayerAnim playerAnim)
        {
           // playerAnim.HarvestFat();


        }

        public override State OnUpdate(InputHandler inputHandler, EnemyHealth enemyHealth)
        {
            if (!inputHandler.HarvestFatPressed)
            {
                return new IdleState();
            }
            return null; // Replace with state transition
        }

        public override State OnUpdate(InputHandler inputHandler){ return null;}

        public override void OnExit()
        {
        }
    }
}

