using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject startMenuPanel;
    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;

    private bool gameRunning = false;

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
        // Configurar los botones
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);

        // Mostrar la pantalla de inicio
        ShowStartMenu();

        // Inicializar los valores del juego
        ResetGameValues();
    }

    public bool IsGameRunning()
    {
        return gameRunning;
    }

    private void ShowStartMenu()
    {
        startMenuPanel.SetActive(true); // Mostrar la pantalla de inicio
        gameOverPanel.SetActive(false); // Asegurarse de que no se muestre el panel de Game Over
    }

    private void StartGame()
    {
        gameRunning = true;
        startMenuPanel.SetActive(false); // Ocultar la pantalla de inicio

        // Resetear los valores antes de comenzar
        ResetGameValues();

        // Iniciar oleadas y enemigos
        WaveManager.Instance.StartNewWave();
        EnemyManager.Instance.SpawnEnemies();
    }

    private void RestartGame()
    {
        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGameValues()
    {
        // Reiniciar valores clave del juego
        gameRunning = false;
        WaveManager.currentWave = 0;
        EnemyManager.enemiesKilled = 0;
        EnemyManager.enemiesSpawned = 0;

        // Reinicia cualquier otra variable global o estado necesario
        Debug.Log("Game values reset.");
    }

    public void GameOver()
    {
        gameRunning = false;
        gameOverPanel.SetActive(true); // Mostrar pantalla de Game Over
    }
}
