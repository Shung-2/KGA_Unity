using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround0915 : MonoBehaviour
{
    public Material bgMaterial;
    public float scrollSpeed = 0.2f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 dir = Vector2.up; // v3가 아닌 v2인 이유 > 메테리얼이 2D라서
        
        bgMaterial.mainTextureOffset += dir * scrollSpeed * Time.deltaTime;
    }
}
