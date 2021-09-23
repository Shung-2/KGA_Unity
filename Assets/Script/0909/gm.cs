using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gm : MonoBehaviour
{
    public static gm singleton;
    public bool GameOver = false;
    public int Score = 0;

    void Start()
    {
        singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver)
        {
            SceneManager.LoadScene("End");
        }
    }
}
