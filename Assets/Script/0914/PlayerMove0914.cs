using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove0914 : MonoBehaviour
{
    float speed = 3.0f;
    public int Score;
    public Text ScoreText;
    public Vector2 margin; // 뷰포트 좌표는 0.0 ~ 1.0 사이이므로 v2써도 됌

    void Start()
    {
        Score = 0;
        ScoreText.GetComponent<Text>().text = "0";
        margin = new Vector2(0.08f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        //Vector3 dir = Vector3.right * h + Vector3.up * v;
        //Vector3 dir = new Vector3(h, v, 0);

        // P = P0 + vt (등속운동)
        // V = V0 + at (등가속도 운동)
        // F = ma (가속도 구하는 공식) (m - 질량, a - 가속도)
        Vector3 dir = new Vector3(h, v, 0);
        transform.position += dir * speed * Time.deltaTime;
        transform.Translate(dir * speed * Time.deltaTime);

        Score++;
        ScoreText.GetComponent<Text>().text = Score.ToString();

        MoveInSreen();
    }

    private void MoveInSreen()
    {
        //Vector3 position = transform.position;
        //position.x = Mathf.Clamp(position.x, -2.5f, 2.5f);
        //position.y = Mathf.Clamp(position.y, -3.5f, 3.5f);
        //transform.position = position;

        // 메인카메라의 뷰포트를 가져와서 처리할 수도 있다.
        // 스크린좌표 : 왼쪽하단 (0, 0), 우측상단(maxX, maxY);
        // 뷰포트좌표 : 왼쪽하단 (0, 0), 우측상단(1.0, 1.0);

        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        position.x = Mathf.Clamp(position.x, 0.0f + margin.x, 1.0f - margin.x);
        position.y = Mathf.Clamp(position.y, 0.0f + margin.y, 1.0f - margin.y);
        transform.position = Camera.main.ViewportToWorldPoint(position);
    }
}
