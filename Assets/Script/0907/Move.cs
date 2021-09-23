using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스크립트를 만들고 이름을 수정하면 유니티상과 스크립트 상 이름이 맞지 않아 동작되지 않는다.
// 즉. 스크립트 이름과 유니티 상에서 스크립트 이름이 같아야한다
public class Move : MonoBehaviour
{
    void Rotate1()
    {
        // Euler Angle 이용한 회전
        this.transform.eulerAngles = new Vector3(0.0f, 45.0f, 0.0f);
    }

    void Rotate2()
    {
        // Quaternion을 이용한 회전
        Quaternion target = Quaternion.Euler(0.0f, 45.0f, 0.0f);
        this.transform.rotation = target;
    }

    void Rotate3()
    {
        // 지속적인 회전
        this.transform.Rotate(Vector3.up * 30.0f * Time.deltaTime);
    }


    void Start()
    {
        // 양쌤
        // Start는 예전에 C++ 수업할 때 사용한 Init과 같다. 

        // 절대좌표 기준으로 이동을 시키려면 position을 사용한다.
        //this.transform.position = new Vector3(1.0f, 0.0f, 0.0f);

        // 상대좌표 기준으로 이동 시키려면 Translate
        this.transform.Translate(new Vector3(1.0f, 0.0f, 0.0f));
        // Translate를 절대적으로 많이 쓴다.

        // 인프런
        //float _yAngle = 0.0f;
    }

    void Update()
    {
        /* 양쌤 */
        float MoveSpeed = 5.0f * Time.deltaTime;
        //this.transform.Translate(Vector3.forward * MoveSpeed);

        // 제가 키를 눌렀을 때만 가고 싶어요
        // if (Input.GetKey(KeyCode.W))
        // {
        //     this.transform.Translate(Vector3.forward * MoveSpeed);
        // }
        // if (Input.GetKey(KeyCode.S))
        // {
        //     this.transform.Translate(Vector3.back * MoveSpeed);
        // }

        // Rotate1(); // 오일러 회전
        // Rotate2(); // 쿼터니언 회전
        // Rotate3(); // 지속적인 회전

        // if (Input.GetKey(KeyCode.A))
        // {
        //    this.transform.Rotate(Vector3.up * -45.0f);
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //    this.transform.Rotate(Vector3.up * 45.0f);
        // }

        // 그럼 매번 이렇게 해줘야 하나요?
        // 아니!
        // Edit - Project Setting - InputManager - Axes - Horizontal, Vertcal을 확인하자
        
        // float v = Input.GetAxis("Vertical");
        // float h = Input.GetAxis("Horizontal");
        // 위와 같이 설정해주면 키 입력을 WASD를 단 2줄로 끝난다.

        // this.transform.Translate(Vector3.forward * v * Time.deltaTime * 5);
        // this.transform.Rotate(Vector3.up * h * Time.deltaTime * 50);
        // 이렇게 설정하면 위에 설정한 45번 라인부터 66 라인의 기능을 모두 다 사용할 수 있다.

        
        /* 인프런 */        
        // _yAngle += MoveSpeed;

        // 절대 회전값
        //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);

        // +, - delta 만큼
        //transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));
        //transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));

        if (Input.GetKey(KeyCode.W))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * MoveSpeed;
            //transform.Translate(Vector3.forward * MoveSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * MoveSpeed;
            //transform.Translate(Vector3.forward * MoveSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * MoveSpeed;
            //transform.Translate(Vector3.forward * MoveSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * MoveSpeed;
            //transform.Translate(Vector3.forward * MoveSpeed);
        }
    }
}
