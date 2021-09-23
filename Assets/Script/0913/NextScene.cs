using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void ChangeScene()
    {
        GameObject girl = GameObject.Find("girl");
        girl.GetComponent<girl>().SaveCuurrentDresses();

        // 다음 씬 전환
        SceneManager.LoadScene("Game");
    }
}
