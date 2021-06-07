using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] GameManager gm;
	[SerializeField] int value;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			gm.AddScore(value);
			gameObject.SetActive(false);
		}
	}

	
}
