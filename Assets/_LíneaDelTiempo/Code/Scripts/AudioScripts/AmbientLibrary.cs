using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmbientLibrary : MonoBehaviour
{
	public AmbientGroup[] ambientGroups;


    // Diccionario que asigna nombres de grupos a sus clips de audio correspondientes
    Dictionary<string, AudioClip> groupDictionary = new Dictionary<string, AudioClip>();

	void Awake()
	{
		foreach (AmbientGroup ambientGroups in ambientGroups)
		{
			groupDictionary.Add(ambientGroups.name, ambientGroups.clip);
		}
	}

	public AudioClip GetClipFromName(string name)
	{
        // Verifica si el diccionario contiene el nombre del grupo
        if (groupDictionary.ContainsKey(name))
		{
			AudioClip ambient = groupDictionary[name];
			return ambient;
		}
		return null;
	}

    // Clase que define un grupo de sonido ambiental
    [System.Serializable]
	public class AmbientGroup
	{
		public string name;
		public AudioClip clip;
	}
}