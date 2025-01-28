using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EnemyState currentAttackState;
    [SerializeField] private EnemyHandler enemyHandler;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private EnemyAnim enemyAnim;


    private void Start()
    {
        if (playerStats != null)
        {
            currentAttackState = new EnemyAttackState.IdleState(rb, playerStats, enemyAnim);
        }
        else
        {
            Debug.LogError("PlayerStats is null in Start!");
        }
    }

    private void FixedUpdate()
    {
        if (currentAttackState != null)
        {
            EnemyState newMovementState = currentAttackState.OnUpdate(enemyHandler);
            if (newMovementState != null)
            {
                currentAttackState.OnExit();
                currentAttackState = newMovementState;
                currentAttackState.OnEnter(enemyHandler);
            }
        }
    }
}
