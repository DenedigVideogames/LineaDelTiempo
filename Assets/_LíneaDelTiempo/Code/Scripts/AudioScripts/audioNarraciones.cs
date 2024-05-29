using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioNarraciones : MonoBehaviour
{

    

    public void StopAnteriorNarracion(string nombreclip)
    {
        AudioManager.Instance.StopSound();
        AudioManager.Instance.PlaySound2D("");
    }

    public void SigNarracion(string nombreclip)
    {
        AudioManager.Instance.StopSound();
        AudioManager.Instance.PlaySound2D(nombreclip);
    }


    

}
