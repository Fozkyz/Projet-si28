using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBehaviour : MonoBehaviour
{
    [SerializeField] protected DialogueManager dialogue_manager;
    [SerializeField] protected List<Dialogue> dialogues;

    protected void LoadDialogue(int i)
    {
        if (i < dialogues.Count)
		{
            Dialogue d = dialogues[i];
            dialogue_manager.ResetDialogue(d);
		}
    }

    protected IEnumerator LaunchDialogue(float delay)
	{
        yield return new WaitForSeconds(delay);
        dialogue_manager.StartDialogue();
	}

    public virtual void OnDialogueFinished()
	{
        return;
	}
}
