using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AISTATE { IDLE, DETECTED, ATTACKING}
public class EnemyBat : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] float detection_distance;
    [SerializeField] float attack_distance;
    AISTATE state;

    // Start is called before the first frame update
    void Start()
    {
        state = AISTATE.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == AISTATE.IDLE && Vector2.Distance(transform.position, player.transform.position) <= detection_distance)
		{
            state = AISTATE.DETECTED;
		}
        else if (state == AISTATE.DETECTED && Vector2.Distance(transform.position, player.transform.position) < attack_distance)
		{
            state = AISTATE.ATTACKING;
		}
    }
}
