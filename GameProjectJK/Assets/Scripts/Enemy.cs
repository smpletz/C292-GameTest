using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    [SerializeField] GameManager manager;

    bool dz = false;
    bool dodge = false;
    float direction;
    //float uBound;
    //float lBound;
    //float xuBound;
    //float xlBound;


    // Start is called before the first frame update
    void Start()
    {
        //Random number generator; determines if the ship is dodging or not (50% chance)
        float randX = Random.Range(0,2);
        if (randX < 1)
        {
            dodge = true;
        }

        //randomly determines the direction of the dodge
        direction = Random.Range(0, 2);

        //determine the boundaries of a "dodge zone"
        //uBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.6f, 0)).y;
        //lBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.4f, 0)).y;
        //xuBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.85f, 0)).x;
        //xlBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.15f, 0)).x;
        //15% from sides of screen makes it so the ship can never exit the screen
        //1:6 ratio of side to down (15 degrees left). Across the gap that is 20%
        //of the screen, the most that the ship can move is to the border, not beyond it
    }

    // Update is called once per frame
    void Update()
    {
        if (dodge)
        {
            //Get x and y position of enemy ship
            //float xPos = transform.position.x;
            //float yPos = transform.position.y;
            //deide whether or not the ship is in the "dodge zone"
            //if ((uBound >= yPos && yPos >= lBound) && (xuBound >= xPos && xPos >= xlBound))
            if (dz)
            {
                //Using the direction stat from earlier, decide whether the ship
                //goes left or right
                if (direction >= 1)
                {
                    transform.position -= new Vector3(speed/3, speed, 0) * Time.deltaTime;
                }
                else
                {
                    transform.position -= new Vector3(speed/3, speed, 0) * Time.deltaTime;
                }
            }
            else //when ship is not in the "dodge zone", go straight down
            {
                transform.position -= new Vector3(0, speed, 0) * Time.deltaTime;
            }
        }
        else // If the ship isn't dodging, go straight the whole time
        {
            transform.position -= new Vector3(0, speed, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.instance.InitateGameOver();
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Zone")
        {
            dz = true;
        }
        else
        {
            GameManager.instance.IncreaseScore(10);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Zone")
        {
            dz = false;
        }
    }
}
