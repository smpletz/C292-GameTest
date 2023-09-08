using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float yPosition;
    [SerializeField] GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
        yPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 convertedPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(convertedPositon.x, yPosition, 0);

        if (Input.GetButtonDown("FireLaser"))
        {
            Instantiate(laser, transform.position, Quaternion.identity);
        }
    }
}
