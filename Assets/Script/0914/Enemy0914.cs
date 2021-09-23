using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy0914 : MonoBehaviour
{
    public float speed = 5.0f;
    Vector3 dir;
    public GameObject explosionFactory;

    void Start()
    {
        int rndValue = Random.Range(0, 10);

        if (rndValue < 3)
        {
            GameObject target = GameObject.Find("Player");

            dir = target.transform.position - transform.position;
            // 방향을 구한다
            dir.Normalize();   
            // 방향의 크기를 1로 한다.         
        }
        else
        {
            dir = Vector3.down;
        }
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other) 
    {
        // 씬에서 ScoreManager 객체를 찾아온다.
        // GameObject scoreManager = GameObject.Find("ScoreManager");
        // ScoreManager 게임 오브젝트를 가져온다.
        // ScoreManager0916 sm = scoreManager.GetComponent<ScoreManager0916>();
        // ScoreManager 클래스의 int 변수에 값을 변경해준다.

        // sm.SetScore(sm.GetScore() + 1);

        // ScoreManager0916.Instance.SetScore(ScoreManager0916.Instance.GetScore() + 1);
        
        ScoreManager0916.Instance.Socre++;

        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = transform.position;

        //Destroy(other.gameObject); // 너 죽인다음
        //Destroy(gameObject); // 나도 죽는다.

        if(other.gameObject.name.Contains("bullet0914"))
        {
            other.gameObject.SetActive(false);
        }
        
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
