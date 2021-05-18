using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
	[SerializeField] GameManager gm;
	AudioSource source;

	private void Start()
	{
		source = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			source.Play();
			gm.DisplayWin();
		}
	}
}