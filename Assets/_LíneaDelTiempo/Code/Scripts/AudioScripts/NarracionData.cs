using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nueva Narracion", menuName = "Narracion")]
public class NarracionData : ScriptableObject
{
    [SerializeField] private AudioClip[] _narracion;
    [SerializeField] private int _index = 0;
    [Range(0, 1)]
    [SerializeField] private float _volumen;
    [Range(0, 1)]
    [SerializeField] private float _pitch;

    public int index { get { return _index; } }
    public AudioClip[] audio { get { return _narracion; } }
    
    public float volumen { get { return _volumen; } }
    public float pitch { get { return _pitch; } }
}
