using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager0914 : MonoBehaviour
{
    public int poolSize = 10;
    GameObject[] enemyObjectPool;
    public Transform[] spawnPoint; // 스폰 지점 배열

    float minTime = 0.5f;
    float maxTime = 1.5f;

    float currentTime; // 현재시간
    public float createTime = 1.0f; // 생성시간

    public GameObject enemyFactory; // 적을 찍어내자

    void Start()
    {
        createTime = Random.Range(minTime, maxTime);

        enemyObjectPool = new GameObject[poolSize];

        for (int i = 0 ; i < poolSize; ++i)
        {
            GameObject enemy = Instantiate(enemyFactory);

            enemyObjectPool[i] = enemy;

            enemy.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (createTime < currentTime)
        {
            for (int i = 0 ; i < poolSize; ++i)
            {
                GameObject enemy = enemyObjectPool[i];
                
                if (enemy.activeSelf == false)
                {
                    enemy.SetActive(true);

                    int Index = Random.Range(0, spawnPoint.Length);
                    // 에너미 위치는 현재 (에너미매니저라는 이름의 빈오브젝트)의 위치
                    enemy.transform.position = spawnPoint[Index].position;
                    break;
                }
            }

            // 생성시간이 되었다. 에너미 객체를 생성하자
            // GameObject enemy = Instantiate(enemyFactory);

            // 에너미 위치는 현재 (에너미매니저란 이름의 빈 오브젝트)
            //enemy.transform.position = transform.position;

            createTime = Random.Range(minTime, maxTime);
            currentTime = 0;

        }
    }
}
