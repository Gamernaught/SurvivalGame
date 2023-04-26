using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float xRange = 7;
    public float yRange = 18;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector2 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-xRange, xRange);
        float spawnPosY = Random.Range(-yRange, yRange);
        Vector2 randomPos = new Vector2(spawnPosY, spawnPosX);
        return randomPos;
    }
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
    }
}
