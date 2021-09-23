using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrel : MonoBehaviour
{
    public float MaxDepression = -5f;
    public float MaxElevation = 20f;

    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            transform.localEulerAngles = new Vector3 (Mathf.Clamp(transform.localEulerAngles.x, MaxDepression, MaxElevation), transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }
}
