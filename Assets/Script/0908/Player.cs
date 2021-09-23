using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animation anim;
    Rigidbody rb;

    void Start()
   {
       anim = GetComponent<Animation>();
       rb = GetComponent<Rigidbody>();
   }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        this.transform.Translate(Vector3.forward * 5.0f * v * Time.deltaTime);
        this.transform.Rotate(Vector3.up * 100.0f * h * Time.deltaTime);

        // 움직이는 키가 안눌러져 있을 경우
        if (v == 0 && h == 0)
        {
            anim.CrossFade("Idle");
            // Fadein, Out과 같이 느낌. 바로 형태가 바뀌는 것이 아닌 천천히 효과가 적용된다.
        }
        else 
        {
            anim.CrossFade("Run");
        }
        // RigidBody에서 Freeze Position, Rotate에서 원하는 X/Y/Z축 누르면 부딪쳤을 때 덜그럭거리지 않는다.

        // Jump
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 250);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌 판정이 시작될 때
        Debug.Log(collision.gameObject.name);
    }

    private void OnCollisionStay(Collision other)
    {
        // 충돌 판정이 지속되고 있을 때 
    }

    private void OnCollisionExit(Collision other)
    {
        // 충돌 판정이 끝날 때 
    }
 
    // 콜라이더에서 트리거 체크 했을 때만 동작합니다
    private void OnTriggerEnter(Collider other) 
    {
        // 충돌 판정이 시작될 떄
    }

    private void OnTriggerStay(Collider other)
    {
        // 충돌 판정이 지속되고 있을 때
    }

    private void OnTriggerExit(Collider other)
    {
        // 충돌 판정이 끝날 때
    }

    
}
