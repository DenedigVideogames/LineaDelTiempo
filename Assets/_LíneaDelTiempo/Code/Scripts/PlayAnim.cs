using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
    public Animator anim;

    public void Play(){
        anim.Play("PopUp");
    }

    public void Off(){
        anim.Play("Off");
    }
}
