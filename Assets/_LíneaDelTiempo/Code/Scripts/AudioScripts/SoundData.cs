using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Sound", menuName = "Sound")]
public class SoundData : ScriptableObject
{
    [SerializeField] private AudioClip _audio;
    [SerializeField] private float _volumen;
    [SerializeField] private float _pitch;

    public AudioClip audio {get{return _audio;}}
    public float volumen {get{return _volumen;}}
    public float pitch {get{return _pitch;}}
}
