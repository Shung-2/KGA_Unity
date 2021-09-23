using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_Move : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);    
    }
}
