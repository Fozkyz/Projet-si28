using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] PlayerMovement player;
	[SerializeField] Transform spawn_point;
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
