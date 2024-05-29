using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarracionLibrary : MonoBehaviour
{
    public NarracionData[] narracionGroups;
    Dictionary<int, AudioClip> groupDictionary = new Dictionary<int, AudioClip>();
    private void Awake()
    {
        foreach (NarracionData nGroup in narracionGroups)
        {
            groupDictionary.Add(nGroup.index, nGroup.audio[PlayerPrefs.GetInt("Idioma")]);
        }
    }

    public AudioClip GetNarracionFromName(int name)
    {
        if (groupDictionary.ContainsKey(name))
        {
            AudioClip narraciones = groupDictionary[name];
            return narraciones;
        }
        return null;
    }
    

    /*[System.Serializable]
    public class NarracionGroup
    {
        public string name;
        public AudioClip[] clip;
    }*/
}
