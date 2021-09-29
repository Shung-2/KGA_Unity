using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction1 : MonoBehaviour
{
    public GameObject bombEffect;
    public int bombDamage = 10;
    public float explosionRadius = 0.2f;

    private void OnCollisionEnter(Collision other) 
    {
        // 수류탄 반경안에 몇개의 콜라이더가 들어올지 모르므로 
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius, 1 << 13);

        // 수류탄 폭발 반경안에 잡힌 좀비의 개수만큼 수류탄 데미지를 입힌다.
        for (int i = 0 ; i < cols.Length; ++i)
        {
            cols[i].GetComponent<EnemyFSM>().HitEnemy(bombDamage);
        }

        GameObject eff = Instantiate(bombEffect);

        // 폭탄 이펙트 위치는 현재 오브젝트의 위치와 동일시한다.
        eff.transform.position = transform.position;

        Destroy(gameObject);
    }
    void Update()
    {
        
    }
}
