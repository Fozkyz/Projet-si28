using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPlaying;
    public bool isOnUltimatum;

	void Start()
	{
		isPlaying = true;
		isOnUltimatum = false;
	}
}
