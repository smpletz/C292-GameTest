using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float force;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        //Vector3(0,1,0)
        rb.AddForce(Vector3.up);
        rb.AddForce(Vector3.down * MyPhysics.Drag(rb, 1, 0.15f, 1.293f));
        Debug.Log(rb.velocity.magnitude);
    }
}
