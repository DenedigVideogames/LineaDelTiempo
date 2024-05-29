using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBTN : MonoBehaviour
{
   
    public void BtnSonido(string nombre)
    {
        AudioManager.Instance.PlaySound2D(nombre);
        
    }

    
}
