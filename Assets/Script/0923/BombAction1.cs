using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction1 : MonoBehaviour
{
    public GameObject bombEffect;

    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);

        GameObject eff = Instantiate(bombEffect);

        // 폭탄 이펙트 위치는 현재 오브젝트의 위치와 동일시한다.
        eff.transform.position = transform.position;
    }
    void Update()
    {
        
    }
}
