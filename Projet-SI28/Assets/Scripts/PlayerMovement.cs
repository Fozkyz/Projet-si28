using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    BoxCollider2D box_collider;

    [SerializeField] GameManager gm;

    [Header("Detection")]
    [SerializeField] LayerMask what_is_wall;
    [SerializeField] BoxCollider2D ground_check;
    [SerializeField] BoxCollider2D ceiling_check;
    [SerializeField] Transform left_wall_check;
    [SerializeField] Transform right_wall_check;

    [Header("Animation")]
    [SerializeField] Animator animator;

    [Header("Movement")]
    [SerializeField] float ground_acceleration;
    [SerializeField] float air_acceleration;
    [SerializeField] float ground_deceleration;
    [SerializeField] float air_deceleration;
    [SerializeField] float speed;
    [SerializeField] bool was_facing_right;
    [SerializeField] bool facing_right;
    [SerializeField] Transform fire_point;
    [SerializeField] Transform fire_point_ph;
    [SerializeField] Transform fire_point_on_wall_ph;

    [Header("Camera")]
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] float position_y_up;
    [SerializeField] float position_y_down;

    [Header("Jump")]
    [SerializeField] float jump_height;
    [SerializeField] float gravity_scale;
    [SerializeField] float gravity_when_on_wall;
    [SerializeField] float wall_jump_force;
    [SerializeField] float jump_time_tolerance;
    [SerializeField] float wall_time_tolerance;
    [SerializeField] float jump_time_grounded_tolerance;

    [Header("Dash")]
    [SerializeField] float dash_force;
    [SerializeField] float dash_time;
    [SerializeField] float dash_cooldown;

    [Header("Combat")]
    [SerializeField] float invulnerability_time;
    [SerializeField] float knockdown_time;
    [SerializeField] float knockdown_force;
    [HideInInspector] public bool is_invulnerable;

    [Header("Debug")]
    float horizontal_input;
    Vector2 velocity;
    float acceleration;
    float deceleration;
    float gravity;
    float last_time_grounded;
    float time_since_on_wall;
    float last_time_dash;
    bool is_on_wall;
    bool wall_on_left;
    bool is_grounded;
    bool is_jumping;
    bool is_dashing;
    bool is_knockingdown;

    // Start is called before the first frame update
    void Start()
    {
        box_collider = GetComponent<BoxCollider2D>();
        gravity = gravity_scale;
        fire_point.position = fire_point_ph.position;
        animator.SetBool("isGrounded", true);
        is_knockingdown = false;
        is_dashing = false;
        is_jumping = false;
        is_on_wall = false;
        is_invulnerable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.GetState() == STATE.PLAYING)
        {
            /*if (Input.GetAxisRaw("Vertical") < 0f)
            {
                //var transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
                var transposer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
                transposer.m_TrackedObjectOffset.y = position_y_down;
            }
            else
            {
                var transposer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
                transposer.m_TrackedObjectOffset.y = position_y_up;
            }*/

            PerformMovement();

            last_time_grounded += Time.deltaTime;
            last_time_dash += Time.deltaTime;

            acceleration = is_grounded ? ground_acceleration : air_acceleration;
            deceleration = is_grounded ? ground_deceleration : air_deceleration;

            animator.SetBool("isGrounded", is_grounded);
            Detect();
            DetectGround();
            DetectWalls();
            DetectCeiling();

            transform.Translate(velocity * Time.deltaTime);
        }
        else if (is_grounded)
        {
            animator.SetBool("isGrounded", true);
            animator.SetBool("isRunning", false);
        }
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        horizontal_input = value.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            is_jumping = true;
            if (is_on_wall)
            {
                if (!is_grounded)
                    velocity.x = wall_on_left ? wall_jump_force : -wall_jump_force;
                velocity.y = Mathf.Sqrt(2 * jump_height * gravity_scale * 10);
            }
            else
            {
                if (last_time_grounded <= jump_time_tolerance)
                {
                    velocity.y = Mathf.Sqrt(2 * jump_height * gravity_scale * 10);
                    last_time_grounded += jump_time_tolerance;
                    animator.SetTrigger("Jump");
                }
            }
        }
    }

    public void OnDash(InputAction.CallbackContext value)
	{
        if (value.started && !is_grounded && (last_time_dash >= dash_cooldown) && !is_dashing)
		{
            StartCoroutine(PerformDash());
		}
	}

    void PerformMovement()
    {
        if (horizontal_input != 0 && !is_dashing && !is_knockingdown)
        {
            facing_right = (horizontal_input > 0);
            if (was_facing_right != facing_right)
            {
                was_facing_right = facing_right;
                Flip();
            }
            velocity.x = Mathf.MoveTowards(velocity.x, horizontal_input * speed, acceleration * 100 * Time.deltaTime);
            animator.SetBool("isRunning", is_grounded);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);

            animator.SetBool("isRunning", false);
        }

        if (is_on_wall)
        {
            if (is_grounded)
                time_since_on_wall = 0f;
            else
                fire_point.position = fire_point_on_wall_ph.position;

            animator.SetBool("isOnWall", true);
            last_time_dash = dash_cooldown;
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
            fire_point.position = fire_point_ph.position;
            animator.SetBool("isOnWall", false);
            gravity = gravity_scale;
            time_since_on_wall = 0f;
        }
        if (is_grounded)
        {
            last_time_grounded = 0f;
            last_time_dash = dash_cooldown;
            if (!is_jumping)
                velocity.y = 0f;
        }
        else if (!is_dashing)
        {
            velocity.y -= gravity * 10 * Time.deltaTime;
        }
        if (is_jumping && velocity.y <= 0f)
		{
            is_jumping = false;
        }
    }

    IEnumerator PerformDash()
	{
        is_dashing = true;
        velocity.x = facing_right ? dash_force : -dash_force;
        velocity.y = 0f;
        animator.SetBool("isDashing", true);

        yield return new WaitForSeconds(dash_time);
        
        velocity.x = 0f;
        is_dashing = false;
        last_time_dash = 0f;
        animator.SetBool("isDashing", false);
	}

    void Detect()
    {
        if (box_collider != null)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, box_collider.size, 0, what_is_wall);

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
        if (is_on_wall)
        {
            if (wall_on_left && velocity.x < 0f || !wall_on_left && velocity.x > 0f)
                velocity.x = 0f;
            if (!is_grounded)
            {
                if (wall_on_left && !facing_right)
                {
                    Flip();
                    facing_right = true;
                    was_facing_right = true;
                }
                else if (!wall_on_left && facing_right)
                {
                    Flip();
                    facing_right = false;
                    was_facing_right = false;
                }
            }
        }
    }

    void DetectGround()
    {
        is_grounded = false;

        if (ground_check != null)
        {
            is_grounded = Physics2D.OverlapBox(ground_check.transform.position, ground_check.size, 0f, what_is_wall);
        }
    }

    void DetectCeiling()
    {
        if (Physics2D.OverlapBox(ceiling_check.transform.position, ceiling_check.size, 0f, what_is_wall) && velocity.y > 0f)
        {
            velocity.y = 0f;
        }
    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
        Transform temp = right_wall_check;
        right_wall_check = left_wall_check;
        left_wall_check = temp;
        fire_point.Rotate(0f, 180f, 0f);
    }

    public void GetHit(Enemy enemy)
	{
        // Faire perdre des points de vie
        float dir = Mathf.Sign(transform.position.x - enemy.transform.position.x);
        velocity.x = 0f;
        StartCoroutine(Knockdown(dir));
        gm.GetHit();
	}

    IEnumerator Knockdown(float dir)
	{
        is_knockingdown = true;
        is_invulnerable = true;
        velocity.x = dir * knockdown_force;
        animator.SetBool("isHit", true);

        yield return new WaitForSeconds(knockdown_time);

        is_knockingdown = false;
        velocity.x = 0f;
        animator.SetBool("isHit", false);

        yield return new WaitForSeconds(invulnerability_time - knockdown_time);

        is_invulnerable = false;
        //--------------------------------------------------------------------------------
	}

    public void ResetVelocity()
    {
        velocity = Vector2.zero;
    }

	private void OnEnable()
	{
        is_knockingdown = false;
        is_invulnerable = false;
        is_dashing = false;
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
