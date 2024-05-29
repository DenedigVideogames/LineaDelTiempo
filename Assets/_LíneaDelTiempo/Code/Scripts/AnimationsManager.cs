using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    public Animator menu, idiomas, setings, leader;



    public void EntraMenu(){
        menu.SetBool("Entra", true);
    }
    public void SaleMenu(){
        menu.SetBool("Entra", false);
        menu.SetBool("Sale", true);
    }

    public void SaleIdioma(){
        idiomas.SetBool("Sale", true);
    }

    public void EntraSetings(){
        setings.Play("GetIn");
    }
    public void SaleSetings(){
        setings.Play("GetOut");
    }

    public void EntraLeader(){
        leader.Play("GetIn");
    }
    public void SaleLeader(){
        leader.Play("GetOut");
    }
}
