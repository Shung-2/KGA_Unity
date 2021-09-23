using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager0916 : MonoBehaviour
{
    public Text currentScoreUI;
    public Text bestScoreUI;
    private int currentScore = 0;
    private int bestScore = 0;
    public static ScoreManager0916 Instance = null;
    
    public int Socre
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
            currentScoreUI.text = "현재 점수 : " + currentScore;

            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                bestScoreUI.text = "최고 점수 : " + bestScore;

                PlayerPrefs.SetInt("BestScore", bestScore);
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }    
    }

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreUI.text = "최고 점수 : " + bestScore;
    }

    void Update()
    {
        
    }
}
