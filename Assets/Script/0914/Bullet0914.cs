using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet0914 : MonoBehaviour
{
    float speed = 3.0f;
    
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 dir = Vector3.up;
        transform.position += dir * speed * Time.deltaTime;
    }
}
