using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered Zone.");
        collision.gameObject.transform.localScale = new Vector3(2, 2, 2);
        collision.gameObject.GetComponent<Player>().health -= 10;
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        collision.gameObject.
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.transform.localScale = new Vector3(1, 1, 1);
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
