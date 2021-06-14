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

	Vector3 hide_pos;

	void Start()
	{
		hide_pos = transform.position;
	}

	public void DisplayOptions()
	{
		go.SetActive(true);
		LeanTween.move(gameObject, display_pos, duration);
	}

	public void HideOptions()
	{
		LeanTween.move(gameObject, hide_pos, duration);
		Invoke("Deactivate", duration);
	}

	void Deactivate()
	{
		go.SetActive(false);
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
