using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    BoxCollider2D box_collider;
    
    [Header("Detection")]
    [SerializeField] LayerMask what_is_wall;
    [SerializeField] Transform ground_check;
    [SerializeField] Transform left_wall_check;
    [SerializeField] Transform right_wall_check;
    
    [Header("Movement")]
    [SerializeField] float run_acceleration;
    [SerializeField] float air_acceleration;
    [SerializeField] float ground_deceleration;
    [SerializeField] float air_deceleration;
    [SerializeField] float jump_height;
    [SerializeField] float speed;
    
    [Header("Jump")]
    [SerializeField] float gravity_scale;
    [SerializeField] float gravity_when_on_wall;
    [SerializeField] float wall_jump_force;
    [SerializeField] float jump_time_tolerance;
    [SerializeField] float wall_time_tolerance;

    [Header("Debug")]
    Vector2 velocity;
    float acceleration;
    float deceleration;
    float gravity;
    float last_time_grounded;
    float time_since_on_wall;
    bool is_on_wall;
    bool wall_on_left;
    bool is_grounded;

    // Start is called before the first frame update
    void Start()
    {
        box_collider = GetComponent<BoxCollider2D>();
        gravity = gravity_scale;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (is_on_wall)
		{
            time_since_on_wall = 0f;
            gravity = (velocity.y <= 0) ? 0f : gravity_scale;
        }
        if (is_on_wall && time_since_on_wall >= wall_time_tolerance)
		{
            Debug.Log("oui");
            gravity = (velocity.y <= 0) ? gravity_when_on_wall : gravity_scale;
        }*/
        if (is_on_wall)
		{
            if (is_grounded)
                time_since_on_wall = 0f;

            time_since_on_wall += Time.deltaTime;
            if (time_since_on_wall < wall_time_tolerance)
			{
                gravity = (velocity.y <= 0) ? 0f : gravity_scale;
            }
            else
			{
                gravity = gravity_when_on_wall;
            }
		}
        else
		{
            gravity = gravity_scale;
            time_since_on_wall = 0f;
		}
        if (is_grounded)
		{
            velocity.y = 0f;
            last_time_grounded = 0f;
		}
        else
		{
            velocity.y -= gravity * 10 * Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && is_on_wall)
		{
            if (!is_grounded)
                velocity.x = wall_on_left ? wall_jump_force : -wall_jump_force;
            velocity.y = Mathf.Sqrt(2 * jump_height * gravity_scale * 10);
        }
        if (Input.GetButton("Jump"))
        {
            if ((last_time_grounded <= jump_time_tolerance))
            {
                velocity.y = Mathf.Sqrt(2 * jump_height * gravity_scale * 10);
                last_time_grounded += jump_time_tolerance;
            }
        }
        last_time_grounded += Time.deltaTime;



        /*if (is_grounded)
        {
            velocity.y = 0f;
            if (Input.GetButton("Jump"))
            {
                velocity.y = Mathf.Sqrt(2 * jump_height * gravity * 10);
            }
        }
        else if (is_on_wall)
		{
            //gravity = gravity_when_on_wall;
            velocity.y = (velocity.y <= 0) ? 0f : (velocity.y - gravity * 10 * Time.deltaTime);
            if (Input.GetButtonDown("Jump"))
            {
                velocity.x = wall_on_left ? wall_jump_force : -wall_jump_force;
                velocity.y = Mathf.Sqrt(2 * jump_height * gravity_scale * 10);
            }
        }
        else
		{
            velocity.y -= gravity * 10 * Time.deltaTime;
            gravity = gravity_scale;
        }*/

        acceleration = is_grounded ? run_acceleration : air_acceleration;
        deceleration = is_grounded ? ground_deceleration : air_deceleration;

        float horizontal_input = Input.GetAxisRaw("Horizontal");
        if (horizontal_input != 0)
		{
            velocity.x = Mathf.MoveTowards(velocity.x, horizontal_input * speed, acceleration * 100 * Time.deltaTime);
		}
        else
		{
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
        }

        transform.Translate(velocity * Time.deltaTime);

        Detect();
        DetectGround();
        DetectWalls();

        //gravity = gravity_scale;
    }

    void Detect()
    {
        is_grounded = false;
        if (box_collider != null)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, box_collider.size, 0);

            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Player"))
                    continue;

                ColliderDistance2D colliderDistance = hit.Distance(box_collider);

                if (colliderDistance.isOverlapped)
                {
                    transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                }
            }
        }
    }

    void DetectWalls()
	{
        is_on_wall = false;
        wall_on_left = false;

        if ((left_wall_check != null) && (right_wall_check != null))
		{
            wall_on_left = Physics2D.OverlapBox(left_wall_check.position, left_wall_check.localScale, 0f, what_is_wall);
            is_on_wall = wall_on_left || Physics2D.OverlapBox(right_wall_check.position, right_wall_check.localScale, 0f, what_is_wall);
		}
    }

    void DetectGround()
	{
        is_grounded = false;

        if (ground_check != null)
		{
            is_grounded = Physics2D.OverlapBox(ground_check.position, ground_check.localScale, 0f, what_is_wall);
		}
	}

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.red;

        if (left_wall_check != null)
		{
            Gizmos.DrawWireCube(left_wall_check.position, left_wall_check.localScale);
		}
        if (right_wall_check != null)
		{
            Gizmos.DrawWireCube(right_wall_check.position, right_wall_check.localScale);
		}
    }
}
