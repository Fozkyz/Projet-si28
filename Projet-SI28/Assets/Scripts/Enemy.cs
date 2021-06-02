using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameManager gm;
    public int health = 100;
    public bool willDropPortal = false;
    public GameObject prefab;

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
        if (willDropPortal)
            DropPortal();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null && !player.is_invulnerable)
		{
            player.GetHit(this);
		}
    }

    private void DropPortal()
    {
        Instantiate(prefab, new Vector3(this.transform.position.x,this.transform.position.y + 2f), this.transform.rotation);
    }
}
