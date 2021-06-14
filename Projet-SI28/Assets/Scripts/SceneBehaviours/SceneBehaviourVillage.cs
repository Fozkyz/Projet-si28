using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBehaviourVillage : SceneBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadDialogue(0);
        StartCoroutine(LaunchDialogue(0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
