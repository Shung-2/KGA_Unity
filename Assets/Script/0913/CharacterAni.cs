using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAni : MonoBehaviour
{
    Animator anim;

    int animNumber = 0;

    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    public void ChangeAni()
    {
        animNumber++;

        if (animNumber > 3 ) animNumber = 0;

        anim.SetInteger("aniNum", animNumber);
    }

}
