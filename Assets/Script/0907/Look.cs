using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public GameObject target; 

    void Update()
    {
        Vector3 dirToTarget = target.transform.position - this.transform.position;

        this.transform.forward = dirToTarget.normalized;
        // 내가 바라 봐야하는 방향 = 
        // 에너미에 해당 스크립트를 넣고 바라 볼 객체를 설정하면 바라본다!
    }
}