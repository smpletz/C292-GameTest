using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float spawnRate = 2f;
    [SerializeField] GameObject enemyPrefab;

    float xMin;
    float xMax;
    float ySpawn;
    // Start is called before the first frame update
    void Start()
    {
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0, 0)).x;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0, 0)).x;
        ySpawn = Camera.main.ViewportToWorldPoint(new Vector3(0, 1.25f, 0)).y;
        InvokeRepeating("SpawnEnemy", 0, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        float randX = Random.Range(xMin, xMax);
        Instantiate(enemyPrefab, new Vector3(randX, ySpawn, 0), Quaternion.identity);
    }
}
