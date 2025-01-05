using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] GameObject gunPrefab;

    Transform player;
    List<Vector2> gunPosition = new List<Vector2>();

    int spawnedGuns = 0;
    int lastWaveAddedGun = 0; // Almacena la última ronda en la que se añadió un arma

    private void Start()
    {
        player = GameObject.Find("Player").transform;

        // 4 posiciones arriba (más separadas y centradas)
        gunPosition.Add(new Vector2(-0.9f, -0.3f));  // Arriba izquierda
        gunPosition.Add(new Vector2(-0.5f, -0.1f));  // Arriba medio-izquierda
        gunPosition.Add(new Vector2(-0.1f, -0.1f));  // Arriba medio-derecha
        gunPosition.Add(new Vector2(0.3f, -0.3f));   // Arriba derecha

        // 4 posiciones abajo (más separadas y centradas)
        gunPosition.Add(new Vector2(-0.9f, -1.8f)); // Abajo izquierda
        gunPosition.Add(new Vector2(-0.5f, -2.1f)); // Abajo medio-izquierda
        gunPosition.Add(new Vector2(-0.1f, -2.1f)); // Abajo medio-derecha
        gunPosition.Add(new Vector2(0.3f, -1.8f));  // Abajo derecha

        AddGun(); // Añadir armas iniciales
        AddGun();
    }

    private void Update()
    {
        if (WaveManager.currentWave % 3 == 0 && WaveManager.currentWave != lastWaveAddedGun)
        {
            AddGun();
            lastWaveAddedGun = WaveManager.currentWave; // Actualizar la ronda en la que se añadió un arma
        }
    }

    void AddGun()
    {
        if (spawnedGuns >= gunPosition.Count)
        {
            Debug.LogWarning("No hay más posiciones disponibles para armas.");
            return;
        }

        var pos = gunPosition[spawnedGuns];

        var newGun = Instantiate(gunPrefab, pos, Quaternion.identity);

        Gun gunScript = newGun.GetComponent<Gun>();
        gunScript.SetOffset(pos);
        gunScript.SetPlayer(player);

        spawnedGuns++;
    }
}
