using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptionUI : MonoBehaviour
{
	[SerializeField] LeanTweenType tween_type;
	[SerializeField] Transform display_pos;
	[SerializeField] float duration;
	[SerializeField] GameObject go;
	[SerializeField] GameObject creditsgo;

	Vector3 hide_pos;

	void Start()
	{
		hide_pos = transform.position;
	}

	public void DisplayOptions()
	{
		go.SetActive(true);
		LeanTween.move(gameObject, display_pos, duration).setEase(tween_type);
	}

	public void DisplayCredits()
    {
		creditsgo.SetActive(true);
		LeanTween.scale(creditsgo, Vector3.one, duration).setEase(tween_type);
    }

	public void HideCredits()
    {
		LeanTween.scale(creditsgo, Vector3.zero, duration).setEase(tween_type);
		Invoke("Deactivate", duration);
	}
	public void HideOptions()
	{
		LeanTween.move(gameObject, hide_pos, duration).setEase(tween_type);
		Invoke("Deactivate", duration);
	}

	void Deactivate()
	{
		go.SetActive(false);
		creditsgo.SetActive(false);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void Play()
	{
		int index = SceneManager.GetActiveScene().buildIndex;
		int newindex = index + 1;
		SceneManager.LoadScene(newindex);
	}
}
