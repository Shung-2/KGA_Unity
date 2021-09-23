using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire0914 : MonoBehaviour
{
    public int poolSize = 10;
    public GameObject bulletFactory; // 총알 생상할 곳
    public GameObject firePosition; // 발사되어야할 곳
    
    // 1. 배열로 오브젝트 풀 한 것
    //GameObject[] bulletObjectPool;

    // 2. 리스트로 오브젝트 풀 한 것.
    //public List<GameObject> bulletPool;

    // 3. 큐룰 오브젝트 풀 한 것.
    public Queue<GameObject> bulletPool;

    void Start()
    {
        InitObejctPooling();
    }

    void InitObejctPooling()
    {       
        // 총알 오브젝트 풀 (현재는 10개) 생성 후

        // 1. 배열
        /*
        bulletObjectPool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; ++i)
        {
            // 총알 10개를 미리 생성 시켜놓는다.
            GameObject bullet = Instantiate(bulletFactory);

            // 풀에 생성한 총알 10개를 넣어주자 
            bulletObjectPool[i] = bullet;

            // 총알을 비활성화 시켜둔다
            bullet.SetActive(false);
        }
        */

        // 2. 리스트
        /*
        bulletPool = new List<GameObject>();
        for (int i = 0; i < poolSize; ++i)
        {
            // 총알 10개를 미리 생성시켜놓는다.
            GameObject bullet = Instantiate(bulletFactory);
            // 비활성화 시키고
            bullet.SetActive(false);
            // 풀에 넣어준다.
            bulletPool.Add(bullet);
        }
        */

        // 3. 큐
        bulletPool = new Queue<GameObject>();
        for (int i = 0 ; i < poolSize; ++i)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }
    

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // 1. 배열 오브젝트 풀링으로 쏠 때
            /*
            GameObject bullet = bulletObjectPool[i];
            if (bullet.activeSelf == false)
            {
                bullet.SetActive(true);
                bullet.transform.position = firePosition.transform.position;
                break;
            }
            */

            // 2. 리스트 오브젝트풀링
            /*
            if (bulletPool.Count > 0)
            {
                GameObject bullet = bulletPool[0];
                bullet.SetActive(true);
                bullet.transform.position = firePosition.transform.position;
                // 오브젝트 풀에서 빼준다.
                bulletPool.Remove(bullet);
            }
            else // 오브젝트 풀 공간이 비었으니까 늘려준다
            {
                GameObject bullet = Instantiate(bulletFactory);
                bullet.SetActive(false);
                bulletPool.Add(bullet);
                // 실질적으로 충돌할때에는 비워주는 작업이 필요하다
            }
            */

            // 3. 큐 오브젝트 풀링            
            if (bulletPool.Count > 0)
            {
                // Dequeue로 뽑아온다.
                GameObject bullet = bulletPool.Dequeue();
                bullet.SetActive(true);
                bullet.transform.position = firePosition.transform.position;
            }
            else
            {
                GameObject bullet = Instantiate(bulletFactory);
                bullet.SetActive(false);
                bulletPool.Enqueue(bullet);
            }
        }        
    }
}
