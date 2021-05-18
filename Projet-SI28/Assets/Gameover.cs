using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    [SerializeField] AudioSource source;

	void OnEnable()
	{
        source.Play();
	}
}
