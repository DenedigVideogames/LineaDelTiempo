using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audios : MonoBehaviour
{
    public string nombreClip;
   
    // Start is called before the first frame update
    void Start()
    {
        
        MusicaFade(nombreClip);
    }



    public void MusicaFade(string nombreclip)
    {     
        StartCoroutine(AudioManager.Instance.PlayMusicFade(nombreclip, 5f));
    }

    
}
