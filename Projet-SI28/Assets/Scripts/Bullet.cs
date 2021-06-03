using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public float lifetime;
    public Rigidbody2D rb;
    public bool enemy_bullet;
    [SerializeField] GameObject explosion;

    // Start is called before the first frame update
    void Start()
    { 
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (enemy_bullet)
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player)
                player.GetHit(transform);
        }
        else
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(40);
            }
            //Debug.Log("coucou");
            
        }
        if (collision.gameObject.CompareTag("Physical") || collision.gameObject.CompareTag("Player") && enemy_bullet || collision.gameObject.CompareTag("Enemy") && !enemy_bullet)
        {
            GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(exp, .1f);
            Destroy(gameObject);
        }
    }
}
