using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	[HideInInspector] public Dialogue dial;
	[SerializeField] GameObject dialogue_box;
	[SerializeField] GameObject enter_name_ui;
	[SerializeField] TMP_Text dialogue_UI;
	[SerializeField] AudioManager audio_manager;

	bool waiting_for_start;
	public bool in_dialogue;
	public bool dialogue1_ended;

	private Queue<Sentence> sentences;

	void Awake()
	{
		dialogue1_ended = false;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && in_dialogue)
		{
			CancelInvoke();
			if (waiting_for_start)
				StartDialogue();
			else
				DisplayNextSentence();
		}
	}

	public void ResetDialogue()
	{
		sentences = new Queue<Sentence>();
		waiting_for_start = true;
	}

	public void StartDialogue()
	{
		//Debug.Log("Starting dialogue");
		dialogue_box.SetActive(true);
		waiting_for_start = false;
		sentences.Clear();

		foreach(Sentence sentence in dial.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
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

	IEnumerator TypeSentence (string sentence, float wait)
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
		Invoke("DisplayNextSentence", wait);
	}

	public void EndDialogue()
	{
		waiting_for_start = true;
		dialogue_UI.SetText("");
		//Debug.Log("Finished");
		dialogue_box.SetActive(false);
		in_dialogue = false;
		if (!dialogue1_ended)
		{
			in_dialogue = true;
			LeanTween.scale(enter_name_ui, Vector3.one, .5f).setEase(LeanTweenType.easeInSine);
			enter_name_ui.SetActive(true);
			dialogue1_ended = true;
		}
	}

}
