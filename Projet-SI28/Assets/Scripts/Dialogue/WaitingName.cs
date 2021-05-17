using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaitingName : MonoBehaviour
{
    [SerializeField] DM_Scene01 dm;
    TMP_InputField input;

	void Start()
	{
		input = GetComponent<TMP_InputField>();
		if (input != null)
		{
			input.Select();
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			dm.EnteredName();
		}
	}
}
