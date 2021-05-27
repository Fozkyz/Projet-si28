using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	[SerializeField] GameManager game_manager;
	[SerializeField] AudioManager audio_manager;
	[SerializeField] GameObject dialogue_box;
	[SerializeField] TMP_Text dialogue_UI;

	private Dialogue dial;
	private Queue<Sentence> sentences;

	private bool in_dialogue;

	public void ResetDialogue(Dialogue dialogue)
	{
		StopDialogue();
		dial = dialogue;
		sentences = new Queue<Sentence>();
	}

	public void StartDialogue()
	{
		dialogue_box.SetActive(true);
		game_manager.SetState(STATE.DIALOGUE);
		sentences.Clear();

		foreach (Sentence sentence in dial.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	void DisplayNextSentence()
	{
		//Debug.Log("Next sentence");
		in_dialogue = true;
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		else
		{
			Sentence sentence = sentences.Dequeue();
			if (sentence.audio_clip != null)
				audio_manager.PlayClipDelayed(sentence.audio_clip, sentence.delay);
			StopAllCoroutines();
			StartCoroutine(TypeSentence(sentence.sentence, sentence.duration));
			//dialogue_UI.SetText(sentence);
		}
	}

	IEnumerator TypeSentence(string sentence, float wait)
	{
		dialogue_UI.SetText("");
		foreach (char c in sentence.ToCharArray())
		{
			dialogue_UI.text += c;
			if (c == ' ')
				yield return new WaitForSeconds(.05f);
			else
				yield return new WaitForSeconds(.01f);
		}
		Invoke("DisplayNextSentence", wait);
	}

	void EndDialogue()
	{
		dialogue_UI.SetText("");
		dialogue_box.SetActive(false);
		in_dialogue = false;
	}

	void StopDialogue()
	{
		CancelInvoke();
		if (in_dialogue)
		{
			EndDialogue();
		}
	}

	public void OnNextDialogue(InputAction.CallbackContext value)
	{
		if (value.started && in_dialogue)
		{
			DisplayNextSentence();
		}
	}

}
