using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int damage);
}

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] GameManager gm;
    public int health = 100;
    public bool willDropPortal = false;
    public GameObject prefab;

    public PlayerMovement player;
    public float radius;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject muzzleflashPrefab;
    public float fire_rate;
    private bool is_shooting = false;
    private float cooldown;

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        cooldown -= Time.deltaTime;
        if (distance < radius)
            is_shooting = true;
        else
            is_shooting = false;
        if (is_shooting && cooldown <= 0)
            Shoot();
    }

    void IDamageable.TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    void Shoot()
    {
        cooldown = 1 / fire_rate;
        GameObject muzzleflash = Instantiate(muzzleflashPrefab, firePoint.position, firePoint.rotation);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Destroy(muzzleflash, .1f);
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
            player.GetHit(transform);
		}
    }

    private void DropPortal()
    {
        Instantiate(prefab, new Vector3(this.transform.position.x,this.transform.position.y + 2f), this.transform.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

