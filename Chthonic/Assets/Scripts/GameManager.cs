using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject firePrefab;
    private float xSpawn;
    private float ySpawn;

    bool playerDead = false;

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
        if (playerDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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

    public void InitatePlayerDead()
    {
        playerDead = true;
    }
}
