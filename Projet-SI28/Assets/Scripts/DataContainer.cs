using UnityEngine;

public class DataContainer : SceneBehaviour
{
    public float music_volume;
    public float voice_volume;

    bool waiting;
    int i;

    private DataContainer instance;

	private void Awake()
	{
		if (instance != null)
		{
            Destroy(gameObject);
		}
        else
		{
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        music_volume = 1f;
        voice_volume = 1f;
        i = 0;
        waiting = false;
    }

    public void ChangeMusicVolume(float volume)
	{
        music_volume = volume;
	}

    public void ChangeVoiceVolume(float volume)
    {
        voice_volume = volume;
        StartNextSentence();
    }

    public override void OnDialogueFinished()
    {
        waiting = false;
    }

    public void StartNextSentence()
    {
        if (!waiting)
        {
            waiting = true;
            LoadDialogue(i);
            StartCoroutine(LaunchDialogue(0f));
            i = (i + 1) % dialogues.Count;
        }
    }
}
