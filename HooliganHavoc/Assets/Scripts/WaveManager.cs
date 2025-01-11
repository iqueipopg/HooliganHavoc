
using UnityEngine;
using TMPro;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI waveText;


    public static WaveManager Instance;

    bool waveRunning = true;
    public static int currentWave = 1;



    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        if (!GameManager.instance.IsGameRunning()) return;
        StartNewWave();
        timeText.text = "Enemies left: " + EnemiesPerWave();
        waveText.text = "Wave: 1";
    }

    private void Update()
    {

        int e = EnemiesPerWave();

        if (EnemyManager.enemiesKilled >= e)
        {
            WaveComplete();
            timeText.text = "Well done!!";
            StartCoroutine(DelayBeforeNewWave(1.5f)); 
        }
        timeText.text = "Enemies left: " + (e - EnemyManager.enemiesKilled); 
    }

    public bool WaveRunning() => waveRunning;

    private IEnumerator DelayBeforeNewWave(float delay)
    {
        yield return new WaitForSeconds(delay); 
        StartNewWave(); 
    }


    private int EnemiesPerWave()
    {
        return currentWave * 5;
    }

    public void StartNewWave()
    {
        StopAllCoroutines();
        EnemyManager.enemiesKilled = 0;
        EnemyManager.enemiesSpawned = 0;
        waveRunning = true;
        waveText.text = "Wave: " + currentWave;
    }




    public void WaveComplete()
    {
        StopAllCoroutines();
        currentWave++;
        EnemyManager.enemiesKilled = 0;
        EnemyManager.maxEnemies = EnemiesPerWave();
        EnemyManager.enemiesKilled = 0;
        EnemyManager.enemiesSpawned = 0;
        EnemyManager.Instance.DestriyAllEnemies();
        waveRunning = false;

    }




}