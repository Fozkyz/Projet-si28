using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	DataContainer data_container;
	AudioSource source;
	private static MusicManager instance;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			data_container = FindObjectOfType<DataContainer>();
			source = GetComponent<AudioSource>();
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void Update()
	{
		source.volume = data_container.music_volume;
	}

}
