using UnityEngine;
using TMPro;

public enum STATE { PLAYING, DIALOGUE, ULTIMATUM }

public class GameManager : MonoBehaviour
{
	[SerializeField] PlayerMovement player;
	[SerializeField] Transform spawn_point;
	[SerializeField] TMP_Text score_ui;
	[SerializeField] GameObject[] images;
	[SerializeField] AudioSource game_over_sound;
	[SerializeField] float y_boundary;

	int score;
	int life_count;

	STATE state;

	void Start()
	{
		life_count = 2;
		state = STATE.PLAYING;
	}

	void Update()
	{
		if (player.transform.position.y < y_boundary)
		{
			Die();
		}
	}

	public void Die()
	{
		player.ResetVelocity();
		if (images != null)
		{
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
			}
		}
	}

	public void AddScore(int add)
	{
		score += add;
		if (score_ui != null)
			score_ui.text = score.ToString();
	}

	public STATE GetState()
	{
		return state;
	}

	public void SetState(STATE new_state)
	{
		state = new_state;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(new Vector3(-1000f, y_boundary, 0f), new Vector3(1000f, y_boundary, 0f));
	}
}
