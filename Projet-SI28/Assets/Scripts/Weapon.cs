using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameManager gm;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fire_rate;

    float cooldown;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.LeftShift)) && gm.isPlaying && cooldown <= 0f)
        {
            Shoot();
        }
        cooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        cooldown = 1 / fire_rate;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
