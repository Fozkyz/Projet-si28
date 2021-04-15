using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

	public TMP_Text dialogue_UI;
	public Dialogue dial;
	bool waiting_for_start;

	private Queue<string> sentences;

	void Start()
	{
		sentences = new Queue<string>();
		waiting_for_start = true;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (waiting_for_start)
				StartDialogue(dial);
			else
				DisplayNextSentence();
		}
	}

	public void StartDialogue(Dialogue dialogue)
	{
		Debug.Log("Starting dialogue");
		waiting_for_start = false;
		sentences.Clear();

		foreach(string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		Debug.Log("Next sentence");
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		else
		{
			string sentence = sentences.Dequeue();
			StopAllCoroutines();
			StartCoroutine(TypeSentence(sentence));
			//dialogue_UI.SetText(sentence);
		}
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogue_UI.SetText("");
		foreach(char c in sentence.ToCharArray())
		{
			dialogue_UI.text += c;
			if (c == ' ')
				yield return new WaitForSeconds(.05f);
			else
				yield return new WaitForSeconds(.01f);
		}
	}

	public void EndDialogue()
	{
		waiting_for_start = true;
		dialogue_UI.SetText("");
		Debug.Log("Finished");
	}

}
