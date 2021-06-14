using System.Collections;
using UnityEngine;

public enum TYPE { MUSIC, VOICE};
public class AudioManager : MonoBehaviour
{
	public TYPE type;
	DataContainer container;
	AudioSource source;

	void Start()
	{
		source = GetComponent<AudioSource>();
		container = FindObjectOfType<DataContainer>();
	}

	void Update()
	{
		if (type == TYPE.MUSIC)
			source.volume = DataContainer.instance.music_volume;
		else
			source.volume = DataContainer.instance.voice_volume;
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