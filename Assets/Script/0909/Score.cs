using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Flappy_Player fp;
    public Text copy_SocreText;
    public int copy_score;

    void Start()
    {
        //Debug.Log ("Socre : " + fp.Score);
        fp = FindObjectOfType<Flappy_Player>();
        copy_SocreText.GetComponent<Text>().text = "0";
    }
    void Update()
    {
        copy_score = fp.Score;    
        copy_SocreText.GetComponent<Text>().text = copy_score.ToString();
    }
}
