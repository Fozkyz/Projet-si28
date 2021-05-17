using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	AudioSource source;

	private void Awake()
	{
		source = GetComponent<AudioSource>();
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