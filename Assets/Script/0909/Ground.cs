using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D bc;
    public float GroundSpeed = 1.0f;
    public float GroundXSize;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        GroundXSize = 2.5f;
    }

    void Update()
    {
        GroundSpeed += Time.deltaTime / 8;
        rb.velocity = new Vector2(-GroundSpeed, 0);

        if (transform.position.x < -GroundXSize)
        { // 화면 바깥으로 나간경우
            Vector2 groundOffset = new Vector2(GroundXSize * 2, 0);
            transform.position = (Vector2)transform.position + groundOffset;
        }
    }
}
