using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    float speed = 0.0f;
    float maxSpeed = 10.0f;

    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * (3 + speed) * Time.deltaTime);
        }
        
        //만약에 부스터를 쓰는 키라고 가정해봅시다
        if (Input.GetKey(KeyCode.Space))
        {
            if (speed >= maxSpeed) speed = maxSpeed;
            else speed += 0.1f;

            Camera.main.fieldOfView = 60 + speed;
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            Camera.main.fieldOfView = 60;
            speed = 0.0f;
        }

    }
}
