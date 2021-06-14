using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	[SerializeField] GameObject cam;
    [SerializeField] float parallax_effect;

	float length;
	Vector3 start_pos;
	float cam_y_start;

	void Start()
	{
		cam_y_start = cam.transform.position.y;
		start_pos = transform.position;
		length = GetComponent<SpriteRenderer>().bounds.size.x;
		GameObject go =Instantiate(gameObject, transform.position + Vector3.left * length, Quaternion.identity, transform);
		Destroy(go.GetComponent<Parallax>());
		go = Instantiate(gameObject, transform.position + Vector3.right * length, Quaternion.identity, transform);
		Destroy(go.GetComponent<Parallax>());
	}

	void Update()
	{
		float distx = cam.transform.position.x * parallax_effect;
		float temp = cam.transform.position.x * (1 - parallax_effect);

		//transform.position = start_pos + Vector3.right * distx;
		transform.position = new Vector3(start_pos.x + distx, start_pos.y + cam.transform.position.y - cam_y_start, start_pos.z);
		
		if (temp > start_pos.x + length)
		{
			start_pos.x += length;
		}
		else if (temp < start_pos.x - length)
		{
			start_pos.x -= length;
		}
	}
}
