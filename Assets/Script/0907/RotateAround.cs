using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public GameObject target; 
    // 위와 같이 선언을 해주면 스크립트에 게임 오브젝트를 넣을 수 있도록 변환시켜 준다.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, 45 * Time.deltaTime);
        // RotateAround ~주변을 회전하다. 라는 느낌
        // 자식으로 넣어주면 Player 주변에 회전한다.
    }
}
