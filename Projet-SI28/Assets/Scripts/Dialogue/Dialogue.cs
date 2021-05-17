using UnityEngine.Audio;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    /*[TextArea(3, 10)]
    public string[] sentences;
    public AudioClip[] audioClips;
    public float[] durations;*/
    public Sentence[] sentences;
}

[System.Serializable]
public class Sentence
{
    [TextArea(3, 10)]
    public string sentence;
    public AudioClip audio_clip;
    public float duration;
    public float delay;
}
