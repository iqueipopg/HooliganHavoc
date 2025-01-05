using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] GameObject gunPrefab;

    Transform player;
    List<Vector2> gunPosition = new List<Vector2>();

    int spawnedGuns = 0;

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


        AddGun();
        AddGun();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            AddGun();
        }
    }
    void AddGun()
    {
        var pos = gunPosition[spawnedGuns];

        var newGun = Instantiate(gunPrefab, pos, Quaternion.identity);

        Gun gunScript = newGun.GetComponent<Gun>();
        gunScript.SetOffset(pos);
        gunScript.SetPlayer(player);

        spawnedGuns++;
    }
}
