using UnityEngine;
using Attack;   // Importing the namespace for attack states
using Movement; // Importing the namespace for movement states
using EnemyAttackState;

public class PlayerManager : MonoBehaviour
{
    private State currentAttackState;
    private State currentMovementState;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerAnim playerAnim;

    void Awake()
    {
        currentAttackState = new Attack.IdleState(); // Attack.IdleState
        currentMovementState = new Movement.IdleState(rb); // Movement.IdleState
        StompState.OnDamagedEnemy += GetHurt; 
    }

    private void GetHurt()
    {
        currentMovementState = new Movement.GetHurtState(rb, -1f);
    }

    void FixedUpdate()
    {
        // Handle Movement State
        if (currentMovementState != null)
        {
            State newMovementState = currentMovementState.OnUpdate(inputHandler);
            if (newMovementState != null)
            {
                currentMovementState.OnExit();
                currentMovementState = newMovementState;
                currentMovementState.OnEnter(playerAnim);
            }
        }

        // Handle Attack State
        if (currentAttackState != null)
        {
            State newAttackState = currentAttackState.OnUpdate(inputHandler, inputHandler.GetEnemyHealth());
            if (newAttackState != null)
            {
                currentAttackState.OnExit();
                currentAttackState = newAttackState;
                currentAttackState.OnEnter( playerAnim);
            }
        }
    }
}
