using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameManager gm;
    public int health = 100;
    bool can_kill;

	private void Start()
	{
        can_kill = true;
	}

	public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

    void ChangeBool()
	{
        can_kill = true;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && can_kill)
        {
            gm.Die();
            can_kill = false;
            Invoke("ChangeBool", .5f);
        }
    }
}
