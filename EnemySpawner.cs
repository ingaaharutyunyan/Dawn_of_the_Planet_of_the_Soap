using UnityEngine;
using System;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    private int enemiesPresent, enemiesPresentCap, totalEnemies;
    [SerializeField] private DoorOpen[] doorOpen;

    void OnEnable(){
        EnemyHealth.OnEnemyDead += DecreaseEnemiesPresent;
    }

    void Start(){
        enemiesPresent = 0;
        enemiesPresentCap = 2;
        totalEnemies = 5;
        StartCoroutine(SpawnEnemies());
    }

    public void DecreaseEnemiesPresent(){
        enemiesPresent--;
        totalEnemies--;
    }

    private IEnumerator SpawnEnemies()
    {
        if (totalEnemies <= 0) GameManager.instance.WinGame();
        while (enemiesPresent < enemiesPresentCap){
            doorOpen[UnityEngine.Random.Range(0, doorOpen.Length)].StartLetOutNPC(); 
            enemiesPresent++;
            yield return new WaitForSeconds(15f);
        }
    }
}