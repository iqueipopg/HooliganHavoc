using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject chargerPrefab;

  

    Transform enemiesParent;

    public static EnemyManager Instance;
    public static int maxEnemies = 0;
    public static int enemiesKilled = 0;
    public static int enemiesSpawned;

    private void Awake()
    {
       
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        enemiesParent = GameObject.Find("Enemies").transform;
    }

    private void Update()
    {
        if (!WaveManager.Instance.WaveRunning()) return;

   

        if (  enemiesSpawned<maxEnemies)
        {
            SpawnEnemies();
            enemiesSpawned++;
          
        }
    }

    Vector2 RandomPosition()
    {
        return new Vector2(Random.Range(-16, 16), Random.Range(-8, 8));
    }

    void SpawnEnemies()
    {
        Debug.Log("Spawning enemies...");  // Asegúrate de que esta
        var roll = Random.Range(0, 100);
        var enemyType = roll < 90 ? enemyPrefab : chargerPrefab;

        var e = Instantiate(enemyType, RandomPosition(), Quaternion.identity);
        e.transform.SetParent(enemiesParent);
        Debug.Log("Enemy spawned at: " + e.transform.position);
    }

    public void DestriyAllEnemies()
    {
        foreach (Transform e in enemiesParent)
        {
            Destroy(e.gameObject);
        }
    }
}


