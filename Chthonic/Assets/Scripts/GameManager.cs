using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject firePrefab;
    private float xSpawn;
    private float ySpawn;

    // Awake is called before Start
    private void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFire(float x, float y)
    {
        xSpawn = x;
        ySpawn = y;
        Invoke("Spawn", 3);
    }

    private void Spawn()
    {
        Instantiate(firePrefab, new Vector3(xSpawn, ySpawn, 0), Quaternion.identity);
    }
}
