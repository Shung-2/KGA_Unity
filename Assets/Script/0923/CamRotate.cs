using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float speed = 200f;
    float mx = 0;
    float my = 0;

    void Start()
    {
        
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        mx += mouseX * speed * Time.deltaTime;
        my += mouseY * speed * Time.deltaTime;
        
        my = Mathf.Clamp(my,-90f, 90f);

        transform.eulerAngles = new Vector3(-my, mx, 0);

        //Vector3 dir = new Vector3(-mouseY, mouseX, 0);
        //transform.eulerAngles += dir * speed * Time.deltaTime;

        //Vector3 rot = transform.eulerAngles;
        //rot.x = Mathf.Clamp(rot.x, -90f, 90f);
        //transform.eulerAngles = rot;
    }
}
