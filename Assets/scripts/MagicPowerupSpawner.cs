using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPowerUpSpawner : MonoBehaviour
{
    public GameObject magicPowerupPrefab;
    public float spawnInterval = 5f;
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);

    private void Start()
    {
        InvokeRepeating("SpawnMagicPowerup", 0f, spawnInterval);
    }

    private void SpawnMagicPowerup()
    {
        Vector3 randomPosition = transform.position + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            0f,
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        if(magicPowerupPrefab != null) {
            Instantiate(magicPowerupPrefab, randomPosition, Quaternion.identity);
        }
    }
}
