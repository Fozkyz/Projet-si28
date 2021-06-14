using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBehaviourIntrogun : SceneBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] FadeAnimation fade_anim;
    [SerializeField] Ultimatum ultim;

    int dial_num;

    // Start is called before the first frame update
    void Start()
    {
        dial_num = 0;
        fade_anim.gameObject.SetActive(true);
        gm.SetState(STATE.DIALOGUE);
        LoadDialogue(dial_num);
        StartCoroutine(LaunchDialogue(fade_anim.duration + .5f));        
    }

    public override void OnDialogueFinished()
    {
        if (dial_num == 0)
        {
            dial_num = 1;
            ultim.gameObject.SetActive(true);
            gm.SetState(STATE.ULTIMATUM);
        }
        else
        {
            gm.SetState(STATE.PLAYING);
        }
        
    }

    public void Selection(int i)
    {
        ultim.gameObject.SetActive(false);
        LoadDialogue(i);
        StartCoroutine(LaunchDialogue(.5f));
    }


}
