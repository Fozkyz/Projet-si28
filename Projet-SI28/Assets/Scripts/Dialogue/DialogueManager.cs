using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	[SerializeField] GameObject dialogue_box;
	[SerializeField] GameObject enter_name_ui;
	public TMP_Text dialogue_UI;
	[HideInInspector] public Dialogue dial;
	bool waiting_for_start;
	public bool in_dialogue;
	public bool dialogue1_ended;

	private Queue<string> sentences;
	private Queue<AudioClip> audioClips;
	private Queue<float> durations;

	void Awake()
	{
		ResetDialogue();
		StartDialogue();
		dialogue1_ended = false;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
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
		sentences = new Queue<string>();
		audioClips = new Queue<AudioClip>();
		durations = new Queue<float>();
		waiting_for_start = true;
	}

	public void StartDialogue()
	{
		Debug.Log("Starting dialogue");
		dialogue_box.SetActive(true);
		waiting_for_start = false;
		sentences.Clear();

		/*foreach(string sentence in dial.sentences)
		{
			sentences.Enqueue(sentence);
		}*/
        for (int i = 0; i < dial.sentences.Length; i++)
        {
			sentences.Enqueue(dial.sentences[i]);
			//audioClips.Enqueue(dial.audioClips[i]);
			durations.Enqueue(dial.durations[i]);
        }
		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		Debug.Log("Next sentence");
		in_dialogue = true;
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
		Invoke("DisplayNextSentence", durations.Dequeue());
	}

	public void EndDialogue()
	{
		waiting_for_start = true;
		dialogue_UI.SetText("");
		Debug.Log("Finished");
		dialogue_box.SetActive(false);
		//in_dialogue = false;
		if (!dialogue1_ended)
		{
			LeanTween.scale(enter_name_ui, Vector3.one, .5f).setEase(LeanTweenType.easeInSine);
			enter_name_ui.SetActive(true);
			dialogue1_ended = true;
		}
	}

}
