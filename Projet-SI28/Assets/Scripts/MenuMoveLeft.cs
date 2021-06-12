using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMoveLeft : MonoBehaviour
{
    public float speed_multiplier;
    public float length;

    float speed = 10f;
    float start_x;

    // Start is called before the first frame update
    void Start()
    {
        start_x = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * speed_multiplier * Time.deltaTime);
        if (Mathf.Abs(transform.position.x - start_x) > length)
		{
            transform.position = transform.position + length * Vector3.right;
		}
    }
}
