using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSpawner : MonoBehaviour
{
    public GameObject dragonPrefab;
    public float spawnInterval = 5f;
    public Vector3 spawnAreaSize = new Vector3(10f, 5f, 0f); // Adjust based on your 2D game area

    private float nextSpawnTime;
    public int dragonsPerSpawn = 2;

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnDragon();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnDragon()
    {
        for (int i = 0; i < dragonsPerSpawn; i++)
        {
            Vector3 randomPosition = transform.position + new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
                0
            );

            Instantiate(dragonPrefab, randomPosition, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}