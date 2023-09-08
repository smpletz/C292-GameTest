using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPhysics : MonoBehaviour
{
    public static float Drag(Rigidbody rb, float area, float dc, float den)
    {
        return 0.5f * den * area * rb.velocity.magnitude;
    }
}
