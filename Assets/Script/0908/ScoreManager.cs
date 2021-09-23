using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI 네임 스페이스를 등록해줘야 UI를 사용할 수 있다.

public class ScoreManager : MonoBehaviour
{
    public Text ScoreText;
    public int Score;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        ScoreText.GetComponent<Text>().text = "부딪치면 점수오름";
    }

    public void ScoreUp()
    {
        Score++;
        ScoreText.GetComponent<Text>().text = Score.ToString();
        // ToString이 int든 뭐든 다 변경해줌 ㅅㅂ;
    }
}
