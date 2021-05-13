using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    [SerializeField] GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        //Debug.Log("coucou");
        if (collision.gameObject.CompareTag("Physical"))
		{
            GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(exp, .1f);
            Destroy(gameObject);
		}
    }
}
