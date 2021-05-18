using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	[SerializeField] PlayerMovement player;
	[SerializeField] Transform spawn_point;
	[SerializeField] DialogueManager dialogue_manager;
	[SerializeField] TMP_Text score_ui;
	[SerializeField] GameObject game_over_ui;
	[SerializeField] GameObject win_ui;
	[SerializeField] GameObject[] images;
	[SerializeField] GameObject collectibles;
	[SerializeField] AudioSource game_over_sound;

	public float y_boundary;
    public bool isPlaying;
    public bool isOnUltimatum;
	bool game_paused;
	int score;
	int life_count;

	void Start()
	{
		isPlaying = true;
		isOnUltimatum = false;
		game_paused = false;
		life_count = 2;
	}

	void Update()
	{
		if (player.transform.position.y < y_boundary)
		{
			Die();
		}
		isPlaying = !dialogue_manager.in_dialogue && !game_paused;
	}

	public void Reset()
	{
		player.gameObject.SetActive(true);
		player.transform.position = spawn_point.position;
		player.ResetVelocity();
		isPlaying = true;
		isOnUltimatum = false;
		game_paused = false;
		foreach (GameObject im in images)
		{
			im.SetActive(true);
			life_count = 2;
		}
		foreach(Transform col in collectibles.transform)
		{
			col.gameObject.SetActive(true);
		}
		game_over_ui.SetActive(false);
		win_ui.SetActive(false);
		score = 0;
		AddScore(0);
	}

	public void Die()
	{
		player.ResetVelocity();
		if (life_count >= 1)
		{
			player.transform.position = spawn_point.position;
			images[life_count].SetActive(false);
			life_count--;
		}
		else
		{
			player.gameObject.SetActive(false);
			images[0].SetActive(false);
			game_over_ui.SetActive(true);
			game_paused = true;
		}
	}

	public void DisplayWin()
	{
		win_ui.SetActive(true);
		game_paused = true;
	}

	public void AddScore(int add)
	{
		score += add;
		score_ui.text = score.ToString();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(new Vector3(-1000f, y_boundary, 0f), new Vector3(1000f, y_boundary, 0f));
	}
}
