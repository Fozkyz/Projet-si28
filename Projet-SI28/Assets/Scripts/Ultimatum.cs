using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ultimatum : MonoBehaviour
{
	[SerializeField] GameManager gm;
    public RectTransform left_panel, right_panel;
	
	bool isActive;
	int selection = 0;

	void Start()
	{
		isActive = false;
	}

	void Update()
	{
		if (isActive)
		{
			GetActiveInputs();
			
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.A))
			{
				ActivateUltimatum();
			}
		}
	}

	void ActivateUltimatum()
	{
		isActive = true;
		left_panel.gameObject.SetActive(true);
		right_panel.gameObject.SetActive(true);
		selection = 0;
		gm.isPlaying = false;
		gm.isOnUltimatum = true;
	}

	void DeactivateUltimatum()
	{
		isActive = false;
		left_panel.gameObject.SetActive(false);
		right_panel.gameObject.SetActive(false);
		right_panel.anchorMin = new Vector2(.5f, 0f);
		right_panel.offsetMin = Vector2.zero;
		left_panel.anchorMax = new Vector2(.5f, 1f);
		left_panel.offsetMax = Vector2.zero;
		gm.isPlaying = true;
		gm.isOnUltimatum = false;
	}

	void GetActiveInputs()
	{
		if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			selection = -1;
			right_panel.anchorMin = new Vector2(.6f, 0f);
			right_panel.offsetMin = Vector2.zero;
			left_panel.anchorMax = new Vector2(.6f, 1f);
			left_panel.offsetMax = Vector2.zero;
		}
		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			selection = 1;
			right_panel.anchorMin = new Vector2(.4f, 0f);
			right_panel.offsetMin = Vector2.zero;
			left_panel.anchorMax = new Vector2(.4f, 1f);
			left_panel.offsetMax = Vector2.zero;
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			DeactivateUltimatum();
			Debug.Log("Selection : " + selection);
		}
	}
}
