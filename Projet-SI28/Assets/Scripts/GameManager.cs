using UnityEngine;
using TMPro;

public enum STATE { PLAYING, DIALOGUE, ULTIMATUM, GAMEOVER }

public class GameManager : MonoBehaviour
{
	[SerializeField] PlayerMovement player;
	[SerializeField] Transform spawn_point;
	[SerializeField] TMP_Text score_ui;
	[SerializeField] GameObject[] images;
	[SerializeField] GameObject game_over_ui;

	int score;
	int life_count;

	STATE state;

	void Start()
	{
		life_count = 2;
		state = STATE.PLAYING;
	}

	public void GetHit()
	{
		if (life_count >= 0)
		{
			images[life_count].SetActive(false);
			life_count--;
		}
		else
		{
			Die();
		}
	}

	public void Die()
	{
		player.gameObject.SetActive(false);
		player.StopAllCoroutines();
		game_over_ui.SetActive(true);
		state = STATE.GAMEOVER;
	}

	public void Reset()
	{
		foreach(GameObject im in images)
		{
			im.SetActive(true);
		}
		game_over_ui.SetActive(false);
		life_count = 2;
		player.transform.position = spawn_point.position;
		player.gameObject.SetActive(true);
		player.ResetVelocity();
		state = STATE.PLAYING;
	}

	public void AddScore(int add)
	{
		score += add;
		if (score_ui != null)
			score_ui.text = score.ToString("00000");
	}

	public STATE GetState()
	{
		return state;
	}

	public void SetState(STATE new_state)
	{
		state = new_state;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Reset();
		}
	}
}
