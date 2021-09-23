using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   // AI 네임 스페이스를 명시해줘야 네비 메쉬 사용 가능

public class Enemy : MonoBehaviour
{
    public Transform target;
    NavMeshAgent nav;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        // 실질적으로 nav에 NavMeshAgent를 넣어준다.
    }

    void Update()
    {
        nav.destination = target.position;
        // nav의 목적지는 target(설정해야 한다)의 위치.
    }

    /* 
    OnTriggerEnter와 OnCollisionEnter 차이
    is trigger 체크유무. (민재가 알려줬다 기특한놈)
    */ 

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}
