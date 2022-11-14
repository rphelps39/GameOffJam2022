using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{

    public Rigidbody2D rb;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        rb.AddForce(new Vector2(1, 0));
    }
}
