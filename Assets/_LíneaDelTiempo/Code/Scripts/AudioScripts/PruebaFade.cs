using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaFade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AudioManager.Instance.PlayMusicFade("prueba1", 5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
