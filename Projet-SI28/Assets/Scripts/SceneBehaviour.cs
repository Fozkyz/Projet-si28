using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBehaviour : MonoBehaviour
{
    [SerializeField] protected DialogueManager dialogue_manager;
    [SerializeField] protected Queue<Dialogue> dialogues;

    void LoadDialogue()
    {
        Dialogue d = dialogues.Dequeue();
        dialogue_manager.ResetDialogue(d);
    }

    void LaunchDialogue()
    {
        dialogue_manager.StartDialogue();
    }
}
