using UnityEngine.Audio;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
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
