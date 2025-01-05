using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
  

public class GameManager: MonoBehaviour
{

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Button restartButton;

    private bool gameRunning;
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);

    }

    public bool IsGameRunning()
    {
        return gameRunning;
    }

    public void GameOver()
    {
        gameRunning = false;
        WaveManager.currentWave = 0;
        WaveManager.Instance.WaveComplete();
        gameOverPanel.SetActive(true);
    }

}
