using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] PlayerMovement player;
	[SerializeField] Transform spawn_point;
	[SerializeField] DialogueManager dialogue_manager;
	public float y_boundary;
    public bool isPlaying;
    public bool isOnUltimatum;

	void Start()
	{
		isPlaying = true;
		isOnUltimatum = false;
	}

	void Update()
	{
		if (player.transform.position.y < y_boundary)
		{
			Die();
		}
		isPlaying = !dialogue_manager.in_dialogue;
	}

	public void Die()
	{
		player.transform.position = spawn_point.position;
		player.ResetVelocity();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(new Vector3(-1000f, y_boundary, 0f), new Vector3(1000f, y_boundary, 0f));
	}
}
