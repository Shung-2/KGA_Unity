using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // SceneManager.LoadScene(1); // 번호도 가능하다.
            SceneManager.LoadScene("Game"); // 문자열도 가능하다.
        }
    }
}
