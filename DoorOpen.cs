using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spr;
    [SerializeField] private Sprite[] door;
    [SerializeField] private GameObject enemyNPC;

    public void StartLetOutNPC(){
        StartCoroutine(LetOutNPC());
    }
 
    public IEnumerator LetOutNPC(){
        Debug.Log("LetOutNPC entered");
        spr.sprite = door[1];
        yield return new WaitForSeconds(0.9f);
        spr.sprite = door[2];
        GameObject enemy = Instantiate(enemyNPC, transform.position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(enemy, SceneManager.GetSceneByBuildIndex(2));
        yield return new WaitForSeconds(5f);
        spr.sprite = door[1];
        yield return new WaitForSeconds(0.2f);
        spr.sprite = door[0];
    }
}
