using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    Rigidbody2D rb;
    public float BackGroundSpeed = 1.0f;
    public float BackGroundXSize;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        BackGroundXSize = 2.2f;
    }

    void Update()
    {
        BackGroundSpeed += Time.deltaTime / 8;
        rb.velocity = new Vector2(-BackGroundSpeed, 0);

        if (transform.position.x < -BackGroundXSize)
        {
            Vector2 groundOffset = new Vector2(BackGroundXSize * 2, 0);
            transform.position = (Vector2)transform.position + groundOffset;
        } 
    }
}
