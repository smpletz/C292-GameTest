using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(collision.gameObject.tag == "Player")
        {
            
        }
    }

    public float GetXPos()
    {
        return xPos;
    }
    public float GetYPos()
    {
        return yPos;
    }
}
