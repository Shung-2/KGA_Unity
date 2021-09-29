using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager0927 : MonoBehaviour
{
    public GameObject gameLabel;
    public static GameManager0927 gm;

    Text gameText;
    PlayerMove0923 Player;
    
    private void Awake()
    {
        // 싱글톤은 Awake에서 해준다.
        if (gm == null) gm = this;
    }

    public enum GameState
    {
        Ready,
        Run,
        GameOver
    }

    public GameState gState; // 이넘문 선언

    void Start()
    {
        // 초기화 시켜준다.
        gState = GameState.Ready;   

        // 화면에 게임상태를 표시해줄 텍스트 컴포넌트를 가져온다.
        gameText = gameLabel.GetComponent<Text>();

        gameText.text = "Ready!";

        gameText.color = new Color32(255, 185 , 0, 255);

        Player = GameObject.Find("Player").GetComponent<PlayerMove0923>();

        // 코루틴을 동작시켜준다
        StartCoroutine(ReadyToStart());
    }

    IEnumerator ReadyToStart()
    {
        // 대기에서 게임 시작으로 넘어가기 위한 2초간 대기
        yield return new WaitForSeconds(2f);

        // 게임이 시작했다는 것을 알려주는 텍스트 변경
        gameText.text = "START!!";

        // 잠깐 대기하는 이유는 이제 곧 텍스트를 꺼줘야 하기 때문
        yield return new WaitForSeconds(0.5f);
        // 텍스트를 꺼준다.
        gameLabel.SetActive(false);
        // 게임 상태로 변경
        gState = GameState.Run; 
    }

    void Update()
    {
        if (Player.hp <= 0)
        {
            // 게임 레이블을 다시 활성화 시켜준다.
            gameLabel.SetActive(true);

            // 게임 오버 텍스트를 띄워준다
            gameText.text = "Game Over";
            // 혹시 게임오버 됐을 때 움직임 있을수있으니까 멈춰!
            Player.GetComponentInChildren<Animator>().SetFloat("MoveMotion", 0f);

            // 빨간색으로 게임 오버를 띄워준다.
            gameText.color = new Color32(12, 45, 192, 255);

            // 게임 오버 상태로 변경
            gState = GameState.GameOver;
        }
    }
}
