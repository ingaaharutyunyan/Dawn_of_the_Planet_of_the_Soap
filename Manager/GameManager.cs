using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private AudioManager audioManager;

    private void Awake()
{
    if (instance == null)
    {
        instance = this;
    }
    else
    {
        Destroy(gameObject);
        return; // Exit to avoid further execution
    }

    DontDestroyOnLoad(gameObject); // Optional if the GameManager is used across scenes

    if (playerStats == null)
    {
        Debug.LogError("PlayerStats is not assigned in the GameManager!");
    }
}


    public AudioManager GetAudioManager() => audioManager;
    public PlayerStats GetPlayerStats() => playerStats;

    void OnEnable()
    {
        playerStats.OnDied += OnPlayerDied;
        playerStats.InitHealthAndPP();
        audioManager.PlayMusic("MainTheme");
    }

    public void WinGame(){
        SceneManager.LoadScene(4, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(1);
        SceneManager.UnloadSceneAsync(2);
    }

    void OnPlayerDied()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(1);
        SceneManager.UnloadSceneAsync(2);
    }
}
