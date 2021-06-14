using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBehaviourIntrogun : SceneBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] FadeAnimation fade_anim;

    int dial_num;

    // Start is called before the first frame update
    void Start()
    {
        dial_num = 0;
        fade_anim.gameObject.SetActive(true);
        LoadDialogue(dial_num);
        StartCoroutine(LaunchDialogue(fade_anim.duration + .5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
