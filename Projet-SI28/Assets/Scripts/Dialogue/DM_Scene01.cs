using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DM_Scene01 : MonoBehaviour
{
    [SerializeField] DialogueManager dialogue_manager;
    [SerializeField] Dialogue dialogue1;
    [SerializeField] Dialogue dialogue2;
    [SerializeField] GameObject enter_name_ui;

    // Start is called before the first frame update
    void Start()
    {
        LaunchDialogue(dialogue1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LaunchDialogue(Dialogue dial)
	{
        dialogue_manager.dial = dial;
        dialogue_manager.ResetDialogue();
        dialogue_manager.StartDialogue();
    }

    public void EnteredName()
	{
        dialogue_manager.in_dialogue = false;
        //LeanTween.alpha(enter_name_ui, 1, .5f).setEase(LeanTweenType.easeInSine);
        LeanTween.scale(enter_name_ui, Vector3.zero, .5f).setEase(LeanTweenType.easeInSine);
        Invoke("DeactivateUI", .5f);
        LaunchDialogue(dialogue2);
	}

    void DeactivateUI()
	{
        enter_name_ui.SetActive(false);
    }
}
