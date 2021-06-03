using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class ThinPlatformEffector : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    TilemapCollider2D col;

    bool effector_pressed;

	private void Start()
	{
        col = GetComponent<TilemapCollider2D>();
	}

	// Update is called once per frame
	void Update()
    {
        if (!col.enabled && !effector_pressed && player.velocity.y <= 0f)
		{
            col.enabled = true;
		}
    }

    public void OnEffector(InputAction.CallbackContext value)
	{
        if (value.performed)
		{
            effector_pressed = true;
            col.enabled = false;
        }
        if (value.canceled)
		{
            effector_pressed = false;
		}
	}

    public void OnJump(InputAction.CallbackContext value)
	{
        if (value.started)
		{
            col.enabled = false;
		}
	}
}
