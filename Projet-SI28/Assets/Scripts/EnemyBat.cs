using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AISTATE { IDLE, DETECTED, ATTACKING, ATTACKED}
public class EnemyBat : MonoBehaviour, IDamageable
{
    [SerializeField] PlayerMovement player;
    [SerializeField] GameManager gm;
    [SerializeField] float detection_range;
    [SerializeField] float attack_range;
    [SerializeField] float searching_speed;
    [SerializeField] float attacking_speed;
    Vector3 aim_point;
    float y_start_value;
    [SerializeField] float epsilon;
    [SerializeField] AISTATE state;

    // Start is called before the first frame update
    void Start()
    {
        state = AISTATE.IDLE;
        y_start_value = transform.position.y;
        aim_point = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
		{
            case AISTATE.IDLE:
                if (Vector2.Distance(transform.position, player.transform.position) <= detection_range) // Player is in detection range
				{
                    state = AISTATE.DETECTED;
				}
                break;

            case AISTATE.DETECTED:
                if (Vector2.Distance(transform.position, player.transform.position) < attack_range) // Player is in attack range
				{
                    state = AISTATE.ATTACKING;
				}
                else
				{
                    Vector3 translation = (player.transform.position - transform.position).normalized * searching_speed * Time.deltaTime;
                    translation.y *= 0.1f;
                    transform.Translate(translation);
				}
                break;
            
            case AISTATE.ATTACKING:
                if (aim_point == Vector3.zero)
				{
                    y_start_value = transform.position.y;
                    aim_point = player.transform.position + (player.transform.position - transform.position).normalized;
				}
                else
				{
                    if ((transform.position - aim_point).magnitude > epsilon)
					{
                        transform.position = Vector3.MoveTowards(transform.position, aim_point, attacking_speed * Time.deltaTime);
					}
                    else
					{
                        state = AISTATE.ATTACKED;
                        aim_point = Vector3.zero;
					}
				}
                break;

            case AISTATE.ATTACKED:
                if (transform.position.y <= y_start_value)
				{
                    transform.Translate(Vector3.up * searching_speed * Time.deltaTime);
				}
                else
				{
                    state = AISTATE.IDLE;
				}
                break;
		}
    }

    void IDamageable.TakeDamage(int damage)
	{
        gm.AddScore(70);
        Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null && !player.is_invulnerable)
        {
            player.GetHit(transform);
            Destroy(gameObject);
        }
    }

	void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detection_range);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attack_range);
	}
}
