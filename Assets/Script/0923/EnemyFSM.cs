using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die   
    }

    EnemyState estate;
    public float findDistance = 8.0f;    // 플레이어 탐지 범위
    public float attackDistance = 2.0f; // 공격 범위
    public float moveDistance = 20.0f; // X - 20정도로 줬기 때문에 이 정도로 합시다.
    public float moveSpeed = 5.0f; // 움직이는 속도
    float currentTime = 0;
    float attackDelay = 2.0f;
    public int attackPower = 3;
    int maxHp = 18;
    int hp = 18;

    Vector3 originalPos;
    Transform player;
    CharacterController cc;
    public Slider hpSlider;

    void Start()
    {
        estate = EnemyState.Idle;

        player = GameObject.Find("Player").transform; // 플레이어의 transform을 넣어준다.

        cc = GetComponent<CharacterController>();

        originalPos = transform.position; // 에너미가 자신의 처음 위치를 기억한다.
    }

    void Idle()
    {
        // 발견했을 경우
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            estate = EnemyState.Move;
            print("상태 바뀜 : 기본 > 무브");
        }
    }

    void Move()
    {
        // 현재 위치가 초기 위치에서 벗어나야할 특정 범위를 벗어났다면
        if (Vector3.Distance (transform.position, originalPos) > moveDistance)
        {
            estate = EnemyState.Return;
            print("상태 : 제자리로!");
        }

        // 플레이어에게 도달하지 못할 경우
        if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            // 방향을 구한다
            Vector3 dir = (player.position - transform.position).normalized;

            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else // 플레이어에게 닿았을 경우
        {
            estate = EnemyState.Attack;
            print("상태 바뀜 : 무브 - 어택");

            // 누적 시간을 공격 딜레이만큼 미리 진행해놔야 바로 공격을 실행한다.
            currentTime = attackDelay;
        }
    }

    void Attack()
    {
        // 공격 범위 안에 있으면
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;

            // 한대 때린다.
            if (currentTime > attackDelay)
            {
                player.GetComponent<PlayerMove0923>().DamageAction(attackPower); // 플레이어에게 데미지를 준다.
                print("상태 바뀜 : 어택");
                currentTime = 0;
            }
        }
        else
        {
            estate = EnemyState.Move;
            print("상태 : 다시 쫓음");
            currentTime = 0;
        }
    }

    void Return()
    {
        // 원래 위치에서 조금이라도 벗어났다면
        if (Vector3.Distance (transform.position, originalPos) > 0.1f)
        {
            Vector3 dir = (originalPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            hp = maxHp; // 보통 게임에서 제자리로 돌아오면 피가 차는것을 볼 수 있다
            estate = EnemyState.Idle;
            print("상태변환 : Return -> Idle");
        }
    }

    void Damaged() // Enemy가 맞을 경우
    {
        // 피격 상태처리를 위한 코루틴이다.
        StartCoroutine(DamageProcess());
    }

    // yield 명령어
    // yield return null                    다음 프레임까지 대기
    // yield return WaitForSeconds          지정된 시간(초) 만큼 대기
    // yield return WaitForFixedUpdate      다음 고정(물리) 프레임까지 대기
    // yield return WaitForEndOfFrame       모든 렌더링이 끝날때까지 대기
    // yield return StartCoroutine(string)  특정 코루틴이 끝날떄까지 대기
    // yield return new WWW(string)         웹 통신 작업이 끝날때까지 대기
    // yield return new AsyncOperation      비동기 씬 로드가 끝날때까지 대기

    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(0.5f);

        estate = EnemyState.Move;

        print("데미지 상태 > 무브 상태");
    }

    public void HitEnemy(int damage)
    {
        // 아래와 같은 상태라면 피격 처리를 하지 않는다
        if (estate == EnemyState.Damaged || estate == EnemyState.Die || 
            estate == EnemyState.Return)
        {
            return;
        }

        hp -= damage;
        
        if (hp > 0)
        {
            estate = EnemyState.Damaged;
            print("상태 변환 : 피격되었다");
            Damaged();
        }
        else
        {
            estate = EnemyState.Die;
            print("죽었다!");
            Die();
        }
    }

    void Die()
    {
        // 죽었으므로 모든 코리툰을 중지한다. (사실 피격코루틴을 중지하는 것)
        StopAllCoroutines();
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        // 우선 첫번째 캐릭터 컨트롤러 사용 중지
        cc.enabled = false;
        yield return new WaitForSeconds(2f);
        print("시체 소멸");
        Destroy(gameObject);
    }

    void Update()
    {
        hpSlider.value = (float)hp / (float)maxHp;
        
        switch (estate)
        {
            case EnemyState.Idle:
                Idle();
            break;

            case EnemyState.Move:
                Move();
            break;

            case EnemyState.Attack:
                Attack();
            break;

            case EnemyState.Return:
                Return();
            break;

            case EnemyState.Damaged:
                //Damaged();
                //HitEnemy(3);
            break;

            case EnemyState.Die:
            break;
        }
    }
}