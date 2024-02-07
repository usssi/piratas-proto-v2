using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShipSpawner : MonoBehaviour
{
    public GameObject prefabEnemyShip;

    public GameObject[] listaDeSpawned;

    void Start()
    {
        listaDeSpawned = new GameObject[5];
        SpawnInitialEnemyShips();
    }

    void Update()
    {
        // Check if any enemy ships have been destroyed
        for (int i = 0; i < listaDeSpawned.Length; i++)
        {
            if (listaDeSpawned[i] == null)
            {
                SpawnNewEnemyShip(i);
            }
        }
    }

    void SpawnInitialEnemyShips()
    {
        // Spawn initial enemy ships
        for (int i = 0; i < listaDeSpawned.Length; i++)
        {
            GameObject newEnemyShip = Instantiate(prefabEnemyShip);
            listaDeSpawned[i] = newEnemyShip;
        }
    }

    void SpawnNewEnemyShip(int index)
    {
        // Spawn new enemy ship to replace the destroyed one
        GameObject newEnemyShip = Instantiate(prefabEnemyShip);
        listaDeSpawned[index] = newEnemyShip;
    }
}
