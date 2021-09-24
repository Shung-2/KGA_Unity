using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove0923 : MonoBehaviour
{
    public float moveSpeed = 7.0f;
    public float jumpPower = 5.0f;
    public bool isJumping = false;
    float gravity = -20f; // 중력
    float yVellocity = 0; // y방향

    int maxHp = 30;
    int hp = 30;

    public Slider hpSlider;
    public GameObject hitEffect;    // 힛 이펙트

    CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    public void DamageAction(int damage)
    {
        hp -= damage;

        if (hp > 0)
        {
            // 코루틴으로 피격처리를 해보자
            StartCoroutine(PlayerHitEffect());
        }
        else
        {

        }
    }

    IEnumerator PlayerHitEffect()
    {
        hitEffect.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        hitEffect.SetActive(false);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical"); 

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir);
        // 카메라 오브젝트 방향으로 치환해준다.

        // 플레이더 체력을 슬라이더에 반영하자.
        hpSlider.value = (float)hp / (float)maxHp;

        // Below == 아래영역, Above == 위영역, Side == 옆
        if (isJumping && cc.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;
            yVellocity = 0;
            // 플래피버드때 addforce 전에 제로 벨로시티한 것과 같다.
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVellocity = jumpPower;
            isJumping = true;
        }
        
        yVellocity += gravity * Time.deltaTime;
        dir.y = yVellocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);
        // transform.position += dir * moveSpeed * Time.deltaTime;
    }
}