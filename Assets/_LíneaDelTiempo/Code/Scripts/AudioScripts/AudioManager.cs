using UnityEngine;
using System.Collections;
public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;
	public enum AudioChannel { Master, Music, Ambient, fx };

	[Range(0, 1)]
	public float masterVolume = 1; // Overall volume
	[Range(0, 1)]
	public float musicVolume = 1f; // Music volume
	[Range(0, 1)]
	public float ambientVolume = 1; // Ambient volume
	[Range(0, 1)]
	public float fxVolume = 1; // FX volume

	public bool MusicIsLooping = true;
	public bool AmbientIsLooping = true;
	public bool CoroutineRun; 

	
	public AudioSource musicSource;
	AudioSource ambientSource;
	public AudioSource fxSource;

	
	SoundLibrary soundLibrary;
	MusicLibrary musicLibrary;
	AmbientLibrary ambientLibrary;

	
	private void Awake()
	{
		if (Instance == null) Instance = this;
		else if (Instance != this) Destroy(gameObject);

		DontDestroyOnLoad(gameObject); 

		
		soundLibrary = GetComponent<SoundLibrary>();
		musicLibrary = GetComponent<MusicLibrary>();
		ambientLibrary = GetComponent<AmbientLibrary>();

		
		GameObject newfxSource = new GameObject("2D fx source");
		fxSource = newfxSource.AddComponent<AudioSource>();
		newfxSource.transform.parent = transform;
		fxSource.playOnAwake = false;

		GameObject newMusicSource = new GameObject("Music source");
		musicSource = newMusicSource.AddComponent<AudioSource>();
		newMusicSource.transform.parent = transform;
		musicSource.loop = MusicIsLooping; 
		musicSource.playOnAwake = false;

		GameObject newAmbientsource = new GameObject("Ambient source");
		ambientSource = newAmbientsource.AddComponent<AudioSource>();
		newAmbientsource.transform.parent = transform;
		ambientSource.loop = AmbientIsLooping; 
		ambientSource.playOnAwake = false;

		
		SetVolume(masterVolume, AudioChannel.Master);
		SetVolume(fxVolume, AudioChannel.fx);
		SetVolume(musicVolume, AudioChannel.Music);
		SetVolume(ambientVolume, AudioChannel.Ambient);
	}

    //establecer el volumen de un canal de audio específico
    public void SetVolume(float volumePercent, AudioChannel channel)
	{
		switch (channel)
		{
			case AudioChannel.Master:
				masterVolume = volumePercent;
				break;
			case AudioChannel.fx:
				fxVolume = volumePercent;
				break;
			case AudioChannel.Music:
				musicVolume = volumePercent;
				break;
			case AudioChannel.Ambient:
				ambientVolume = volumePercent;
				break;
		}

		
		fxSource.volume = fxVolume * masterVolume;
		musicSource.volume = musicVolume * masterVolume;
		ambientSource.volume = ambientVolume * masterVolume;
	}

	
	
	// Cuando delay es 0 = No delay
	
	public void PlayMusic(string musicName, float delay)
	{
		musicSource.clip = musicLibrary.GetClipFromName(musicName);
		musicSource.PlayDelayed(delay);
	}

	
	// fade
	
	public IEnumerator PlayMusicFade(string musicName, float duration)
	{
        
		StartCoroutine(StopMusicFade(1f));
		yield return new WaitForSeconds(1.1f);
		CoroutineRun = true; 

		float startVolume = 0;
		float targetVolume = musicSource.volume;
		float currentTime = 0;

		musicSource.clip = musicLibrary.GetClipFromName(musicName);
		if (musicSource)
		{

		}
		musicSource.Play();

		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			musicSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
			yield return null;
		}

		CoroutineRun = false; 

		yield break;
	}

	
	// Stop music
	
	public void StopMusic()
	{
		musicSource.Stop();
	}

	
	// Stop music fade out
	
	public IEnumerator StopMusicFade(float duration)
	{
		CoroutineRun = true; 

		float currentVolume = musicSource.volume;
		float startVolume = musicSource.volume;
		float targetVolume = 0;
		float currentTime = 0;

		
		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			musicSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
			yield return null;
		}
		musicSource.Stop();
		musicSource.volume = currentVolume;

		CoroutineRun = false; 

		yield break;
	}

	
	 //  play ambient sound with delay 0 = No delay
	
	public void PlayAmbient(string ambientName, float delay)
	{
		ambientSource.clip = ambientLibrary.GetClipFromName(ambientName);
		ambientSource.PlayDelayed(delay);
	}

	
	// Stop ambient sound
	
	public void StopAmbient()
	{
		ambientSource.Stop();
	}

	
	// FX Audio
	
	public void PlaySound2D(string soundName)
	{
		fxSource.PlayOneShot(soundLibrary.GetClipFromName(soundName), fxVolume * masterVolume);
	}

	public void PlaySound3D(string soundName, Vector3 soundPosition)
	{
		AudioSource.PlayClipAtPoint(soundLibrary.GetClipFromName(soundName), soundPosition, fxVolume * masterVolume);
	}

	public void StopSound()
    {
		fxSource.Stop();
	}
}
