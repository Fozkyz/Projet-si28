using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public enum TYPE { MUSIC, VOICE};
public class AudioManager : MonoBehaviour
{
	public TYPE type;
	DataContainer container;
	AudioSource source;

	void Awake()
	{
		source = GetComponent<AudioSource>();
		container = FindObjectOfType<DataContainer>();
	}

	void Update()
	{
		if (type == TYPE.MUSIC)
			source.volume = container.music_volume;
		else
			source.volume = container.voice_volume;
	}

	public void PlayClipDelayed(AudioClip clip, float delay)
	{
		StartCoroutine(PlayClip(clip, delay));
	}

	public IEnumerator PlayClip(AudioClip clip, float delay)
	{
		yield return new WaitForSeconds(delay);
		if (source.isPlaying)
		{
			Debug.Log("Stopped audio source");
			source.Stop();
		}
		source.clip = clip;
		source.Play();
	}
}