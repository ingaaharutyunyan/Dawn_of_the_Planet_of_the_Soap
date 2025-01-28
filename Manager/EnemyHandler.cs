using UnityEngine;
using System;
using System.Collections;

public class EnemyHandler : MonoBehaviour
{
    public bool Move { get; set; }
    public bool Idle { get; set; }
    public bool Stomp { get; set; }
    public bool InPlayerTerritory { get; set; }
    public bool Alive { get; set; }
    private Transform player;
    [SerializeField] private Rigidbody2D enemyRB;
    public Vector2 distance;
    private float health;

    void OnEnable(){
        Alive = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyHealth.OnEnemyDead += SetDead;
    }
    void Start()
    {
        StartCoroutine(EnemyStateMachine());
        health = 20f;
    }

    private IEnumerator EnemyStateMachine()
    {
        while (Alive){
            yield return new WaitForSeconds(2f);
            distance = player.position;
            Move = true;
            Stomp = false;
            Idle = false;
            yield return new WaitForSeconds(0.5f);
            Move = false;
            Idle = false;
            Stomp = true;
            yield return new WaitForSeconds(1f);
            Move = false;
            Idle = true;
            Stomp = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            InPlayerTerritory = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            InPlayerTerritory = false;
        }
    }

    public void SetDead(){
        Alive = false;
    }
}