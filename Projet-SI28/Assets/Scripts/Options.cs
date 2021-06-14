using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
	[SerializeField] GameManager gm;
    [SerializeField] LeanTweenType tween_type;
    [SerializeField] float duration;
	[SerializeField] GameObject go;

	bool isDisplaying;
	bool inTransition;

	void Start()
	{
		isDisplaying = false;
		inTransition = false;
		go.transform.localScale = Vector3.zero;
	}

	public void DisplayOptions()
	{
		isDisplaying = true;
		go.SetActive(true);
		LeanTween.scale(go, Vector3.one, duration);
		gm.SetState(STATE.PAUSE);
		//LeanTween.alpha(go, 1f, duration).setEase(tween_type);
	}

    public void HideOptions()
	{
		inTransition = true;
		LeanTween.scale(go, Vector3.zero, duration);
		gm.SetState(STATE.PLAYING);
		//LeanTween.alpha(go, 0f, duration).setEase(tween_type);
		Invoke("Deactivate", duration);
	}

	void Deactivate()
	{
		go.SetActive(false);
		isDisplaying = false;
		inTransition = false;
	}

	public void MainMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void OnPressEsc(InputAction.CallbackContext value)
	{
		if (value.started && !inTransition)
		{
			if (isDisplaying)
			{
				HideOptions();
			}
			else if (gm.GetState() == STATE.PLAYING)
			{
				DisplayOptions();
			}
		}
	}

	public void OnVoiceSliderChanged(float volume)
	{
		if (DataContainer.instance != null)
			DataContainer.instance.voice_volume = volume;
	}

	public void OnMusicSliderChanged(float volume)
	{
		if (DataContainer.instance != null)
			DataContainer.instance.music_volume = volume;
	}
}
