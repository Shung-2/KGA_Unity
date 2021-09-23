using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject ScoreManager;

    void Start()
    {
        ScoreManager = GameObject.Find("ScoreManager");    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            ScoreManager.GetComponent<ScoreManager>().ScoreUp();
            Destroy(gameObject);
        }   
    }

}
