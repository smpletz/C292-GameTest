using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 20;
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");
        transform.position += new Vector3(xMovement, yMovement, 0) * Time.deltaTime * speed;
    }

    public void Die()
    {
        if(health < 0)
        {
            Destroy(gameObject);
        }
    }
}
