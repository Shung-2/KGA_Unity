using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

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
    public float findDistance = 8.0f;   // 플레이어 탐지 범위
    public float attackDistance = 2.0f; // 공격 범위
    public float moveDistance = 20.0f;  // X - 20정도로 줬기 때문에 이 정도로 합시다.
    public float moveSpeed = 5.0f;      // 움직이는 속도
    float currentTime = 0;
    float attackDelay = 2.0f;
    public int attackPower = 3;
    int maxHp = 18;
    int hp = 18;

    Vector3 originalPos;
    Quaternion originRot;   // 좀비가 가지고 있던 원래 회전값.
    Transform player;
    CharacterController cc;
    public Slider hpSlider;
    Animator anim;
    NavMeshAgent bond;  // 제임스 본드

    void Start()
    {
        estate = EnemyState.Idle;

        player = GameObject.Find("Player").transform; // 플레이어의 transform을 넣어준다.

        cc = GetComponent<CharacterController>();

        originalPos = transform.position; // 에너미가 자신의 처음 위치를 기억한다.
        originRot = transform.rotation;

        // 애니메이터를 가져온다.
        anim = transform.GetComponentInChildren<Animator>();
        //anim = transform.GetComponent<Animator>();

        bond = GetComponent<NavMeshAgent>();
    }

    void Idle()
    {
        // 발견했을 경우
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            estate = EnemyState.Move;
            print("상태 바뀜 : 기본 > 무브");
            
            originRot = transform.rotation;

            // 트리거로 파라미터
            anim.SetTrigger("IdleToMove");
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
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            // NavMesh를 사용하기때문에 주석처리
            // 방향을 구한다
            //Vector3 dir = (player.position - transform.position).normalized;

            //cc.Move(dir * moveSpeed * Time.deltaTime);

            // 좀비도 플레이어를 향해서 방향을 전환한다.
            //transform.forward = dir;

            bond.isStopped = true;
            bond.ResetPath();

            // 플레이어를 쫓아오다 공격거리가 되면 선다.
            bond.stoppingDistance = attackDistance;
            // 플레이어를 잡자.
            bond.destination = player.position;
            //bond.transform.rotation = player.transform.rotation;
            Vector3 dir = (player.position - transform.position).normalized;
            transform.forward = dir;
            //transform.rotation = player.rotation;
        }
        else // 플레이어에게 닿았을 경우
        {
            estate = EnemyState.Attack;
            print("상태 바뀜 : 무브 - 어택");

            // 누적 시간을 공격 딜레이만큼 미리 진행해놔야 바로 공격을 실행한다.
            currentTime = attackDelay;
            anim.SetTrigger("MoveToAttackDelay");
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
                anim.SetTrigger("Attack");
            }
        }
        else
        {
            estate = EnemyState.Move;
            print("상태 : 다시 쫓음");
            currentTime = 0;
            anim.SetTrigger("AttackToMove");

        }
    }

    void Return()
    {
        // 원래 위치에서 조금이라도 벗어났다면
        if (Vector3.Distance (transform.position, originalPos) > 0.1f)
        {
            // Vector3 dir = (originalPos - transform.position).normalized;
            // cc.Move(dir * moveSpeed * Time.deltaTime);
            
            // // 복귀방향 & 원래 회전값으로 전환.
            // transform.forward = dir;

            bond.destination = originalPos;
            bond.stoppingDistance = 0;
        }
        else
        {
            bond.isStopped = true;
            bond.ResetPath();

            hp = maxHp; // 보통 게임에서 제자리로 돌아오면 피가 차는것을 볼 수 있다
            estate = EnemyState.Idle;
            transform.position = originalPos;
            transform.rotation = originRot;
            print("상태변환 : Return -> Idle");
            anim.SetTrigger("MoveToIdle");
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
        yield return new WaitForSeconds(1.0f);

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
        
        // 이거 안해주면 마이클 잭슨 빙의해서 온다.
        bond.isStopped = true;
        bond.ResetPath();

        if (hp > 0)
        {
            estate = EnemyState.Damaged;
            print("상태 변환 : 피격되었다");
            anim.SetTrigger("Damaged");
            Damaged();
        }
        else
        {
            estate = EnemyState.Die;
            print("죽었다!");
            anim.SetTrigger("Die");
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
        if (GameManager0927.gm.gState != GameManager0927.GameState.Run) return;

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
