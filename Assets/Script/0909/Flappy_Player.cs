using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flappy_Player : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    Rigidbody2D rb;
    public float jumpPower = 100.0f;
    public gm gm;
    public Text ScoreText;
    public int Score;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        gm = GetComponent<gm>();
        ScoreText.GetComponent<Text>().text = "0";
        Score = 0 ;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    { 
        // 충돌처리
        gm.singleton.GameOver = true;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    { 
        // 점수 상승 (싱글톤 이용 방법)
        // gm.singleton.Score++;
        // Debug.Log(gm.singleton.Score);
        Score++;
        ScoreText.GetComponent<Text>().text = Score.ToString();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.zero; // 점프 중첩을 막기 위해 힘을 0으로 만들어 준다.
            rb.AddForce(Vector2.up * jumpPower);
        }
    }
}
