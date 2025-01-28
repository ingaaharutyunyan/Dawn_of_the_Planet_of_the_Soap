using UnityEngine;
using Movement;
using Attack;
using System.Collections;


public class PlayerAnim : MonoBehaviour
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
       walk.SetBool("Jump", false);
       walk.SetBool("Walk", false);
       walk.SetBool("Punch", false);
    }

    public void Punch(){
        walk.SetBool("Punch", true);
        walk.SetBool("Stand", false);
    }

    public void Slide(){
        walk.SetBool("Slide", true);
        walk.SetBool("Walk", true);
        audioManager.PlaySFX("Slide");
        
    }

    public void Jump(){
        walk.SetBool("Jump", true);
        walk.SetBool("Walk", false);
        walk.SetBool("Punch", false);
    }

    public void GetHurt(){
        walk.SetBool("Hurt", true);
        walk.SetBool("Walk", false);
        walk.SetBool("Punch", false);
    }

    public void Bubble(){
        walk.SetBool("Bubble", true);
        walk.SetBool("Walk", false);
        walk.SetBool("Punch", false);
    }

}
