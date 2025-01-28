using UnityEngine;
using Movement;
using Attack;
using System.Collections;


public class EnemyAnim : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator walk;
    private AudioManager audioManager;

    void Start(){
        audioManager = GameManager.instance.GetAudioManager();
    }

   public void Walk()
    {
        walk.SetBool("Walk", true);
        walk.SetBool("Slide", false);
        walk.SetBool("Jump", false);
    }

    public void StopWalk()
    {
        walk.SetBool("Walk", false);
    }

    public void Flip(bool flip)
    {
        sr.flipX = flip;
    }
    public void Stand(){
       walk.SetBool("Stand", true);
       walk.SetBool("Stomp", false);
        walk.SetBool("Walk", false);
    }

    public void Stomp(){
        walk.SetBool("Stomp", true);
        walk.SetBool("Stand", false);
    }

    public void Dead(){
        walk.SetBool("Dead", true);
        walk.SetBool("Walk", false);  
        walk.SetBool("Stomp", false);
        walk.SetBool("Stand", false);
    }
}
