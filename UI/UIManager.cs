using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthSlider, ppSlider;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Image[] bubble; // bubbe image ui
   // [SerializeField] private Sprite[] bubbleProgress; // visual progress of how much the bubble loaded, 5 images
    private int bubbleIndex = 1; // how many bubbles you have left (2 max)
      void OnEnable()
    {
        playerStats.OnHealthChanged += UpdateHealthSlider;
        playerStats.OnPPChanged += UpdatePPSlider;
    }

    void OnDisable()
    {
        playerStats.OnHealthChanged -= UpdateHealthSlider;
        playerStats.OnPPChanged -= UpdatePPSlider;
    }

    void Start(){
        UpdateHealthSlider(playerStats.health);
        UpdatePPSlider(playerStats.pp);
    }
    private void UpdateHealthSlider(float health)
    {
        float updatedHealth = health/50f;
        healthSlider.value = updatedHealth;
    }

    private void UpdatePPSlider(float pp)
    {
        float newPP = pp/30f;
        ppSlider.value = pp;
    }

  /*   private void DecreaseBubble()
    {
        if (bubbleIndex == -1) return;
        bubbleIndex--;
        bubble[bubbleIndex].sprite = bubbleProgress[0];
        StartCoroutine(IncreaseBubble());
    }

    private IEnumerator IncreaseBubble()
    {   
        if (bubbleIndex == 1) yield return null;
        int i = 0;
        while (i < 5)
        {
            bubble[bubbleIndex].sprite = bubbleProgress[i];
            i++;
            yield return new WaitForSeconds(0.3f);
        }
    } */
}
