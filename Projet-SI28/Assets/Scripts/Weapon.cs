using UnityEngine.InputSystem;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameManager gm;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject muzzleflashPrefab;
    public float fire_rate;

    float cooldown;

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        cooldown = 1 / fire_rate;
        GameObject muzzleflash = Instantiate(muzzleflashPrefab, firePoint.position, firePoint.rotation);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Destroy(muzzleflash, .1f);
    }

    public void OnShoot(InputAction.CallbackContext value)
	{
        if (value.started && cooldown <= 0f && gm.GetState() == STATE.PLAYING)
		{
            Shoot();
		}
	}
}
