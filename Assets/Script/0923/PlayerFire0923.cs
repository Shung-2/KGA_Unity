using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire0923 : MonoBehaviour
{

    enum WeaponMode
    {
        Normal,
        Sniper
    }
    
    WeaponMode wMode;

    bool ZoomMode = false;

    public GameObject firePosition; // 폭탄 발사 위치
    public GameObject bombFactory; // 수류탄
    public GameObject bulletEffect; // 총알 피격자국
    public Text wModeText; // 무기 모드를 표현해줄 텍스트 UI
    ParticleSystem ps; // 파티클 시스템 
    Animator anim;

    public GameObject[] eff_flush;

    public float throwPower = 15f; // 던지는 힘
    public int weaponPower = 3; // 총 데미지

    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();
        wModeText.text = "Normal Mode";
    }

    void Update()
    {
        if (GameManager0927.gm.gState != GameManager0927.GameState.Run) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            wMode = WeaponMode.Normal;
            Camera.main.fieldOfView = 60;
            wModeText.text = "Normal Mode";
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            wMode = WeaponMode.Sniper;
            wModeText.text = "Sniper Mode";
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (anim.GetFloat("MoveMotion") == 0)
            {
                anim.SetTrigger("Attack");
            }
            
            // 레이를 생성해서 쏜다. (레이 발사 위치, 레이 진행 방향)
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            // 이제 뭐가 필요하느냐? 레이가 부딪힌 대상의 정보를 저장할 변수가 필요하다.
            RaycastHit hitinfo = new RaycastHit();

            // ref, out
            // out은 무조건 한번 초기화해서 사용한다.

            if (Physics.Raycast(ray, out hitinfo))
            {
                if(hitinfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy0924"))
                {
                    // 데미지 처리
                    EnemyFSM eFSM = hitinfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(weaponPower);
                }
                else // 그게 아니면 피격 이펙트 플레이
                {
                    bulletEffect.transform.position = hitinfo.point; // 피격 이펙트가 발생할 위치는 레이캐스트가 맞은 위치이다.
                    bulletEffect.transform.forward = hitinfo.normal; // 피격 이펙트의 방향을 포워드 방햐으로 해서 레이가 부딪힌 지점의 법선 벡터와 일치시켜준다.
                    ps.Play(); // 파티클 재생
                }

            }

            StartCoroutine(ShootEffectOn(0.05f));
            
        }

        IEnumerator ShootEffectOn(float duration)
        {
            int num = Random.Range(0, eff_flush.Length - 1);

            eff_flush[num].SetActive(true);
            yield return new WaitForSeconds(duration);
            eff_flush[num].SetActive(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            switch(wMode)
            {
                case WeaponMode.Normal:
                    GameObject bomb = Instantiate(bombFactory);

                    bomb.transform.position = firePosition.transform.position;

                    Rigidbody rb = bomb.GetComponent<Rigidbody>();
                    rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);

                    // Force            == 영속적인 힘을 가함. 질량 영향을 받는다
                    // Accelation       == 연속적인 힘을 가함. 질량 영향 받지 않음
                    // Impulse          == 순간적인 힘을 가함. 질량 영향을 받는다.
                    // VelocityChange   == 순간적인 힘을 가함. 질량 영향 받지 않음

                break;

                case WeaponMode.Sniper:
                    if (!ZoomMode)
                    {
                        Camera.main.fieldOfView = 15f;
                        ZoomMode = true;
                    }
                    else
                    {
                        Camera.main.fieldOfView = 60;
                        ZoomMode = false;
                    }

                break;
            }
        }
    }
}
