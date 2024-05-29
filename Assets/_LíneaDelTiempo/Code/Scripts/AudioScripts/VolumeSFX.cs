using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSFX : MonoBehaviour
{
    public Slider sliderSFX;
    // Start is called before the first frame update
    void Start()
    {
        // Establece el valor del slider al valor guardado en PlayerPrefs o a un valor predeterminado de 0.5
        sliderSFX.value = PlayerPrefs.GetFloat("volumenSFX", 0.5f);
        // Guarda el valor del slider en PlayerPrefs
        PlayerPrefs.SetFloat("volumenSFX", sliderSFX.value);
        // Establece el volumen de los efectos de sonido utilizando el AudioManager
        AudioManager.Instance.SetVolume(PlayerPrefs.GetFloat("volumenSFX"), AudioManager.AudioChannel.Music);
    }

    public void ChangeSliderSFX()
    {
        PlayerPrefs.SetFloat("volumenSFX", sliderSFX.value);

        AudioManager.Instance.SetVolume(PlayerPrefs.GetFloat("volumenSFX"), AudioManager.AudioChannel.fx);
    }
}
