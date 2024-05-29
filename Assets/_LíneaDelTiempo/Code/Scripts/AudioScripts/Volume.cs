using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider sliderMusic;

    // Start is called before the first frame update
    void Start()
    {
        // Establece el valor del slider al último volumen guardado en PlayerPrefs (o 0.5 si no hay ninguno)
        sliderMusic.value = PlayerPrefs.GetFloat("volumenMusica",0.5f);
        PlayerPrefs.SetFloat("volumenMusica", sliderMusic.value);
        // Establece el volumen del canal de audio de la música utilizando AudioManager
        AudioManager.Instance.SetVolume(PlayerPrefs.GetFloat("volumenMusica"), AudioManager.AudioChannel.Music);
        ;
    }

     public void ChangeSlider()
    {
        // Guarda el valor actual del slider en PlayerPrefs
        PlayerPrefs.SetFloat("volumenMusica", sliderMusic.value);
        // Establece el volumen del canal de audio de la música utilizando AudioManager
        AudioManager.Instance.SetVolume(PlayerPrefs.GetFloat("volumenMusica"),AudioManager.AudioChannel.Music);
    }

    

}
