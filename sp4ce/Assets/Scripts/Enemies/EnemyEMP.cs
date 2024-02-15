using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyEMP : EnemyBase
{

    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float chaseSpeed = 10f;

    private enum State
    {
        PATROL,
        BLAST,
        CHARGE,
        CHASE
    }

    private State currentState;

    // Start is called before the first frame update
    void Start()
    {
        speed = walkSpeed;
        agent = GetComponent<NavMeshAgent>();
        currentState = State.PATROL;
        currWaypoint = 0;
        target = patrolWaypoints[currWaypoint];
    }

    // Update is called once per frame
    void Update()
    {
        FSM();
        Move(target);
    }

    public override void FSM()
    {
        switch (currentState)
        {
            case State.PATROL:
                target = patrolWaypoints[currWaypoint];

                if (agent.remainingDistance < .75f)
                    currWaypoint = Random.Range(0, patrolWaypoints.Count);
                break;
            case State.BLAST:

                break;
            case State.CHARGE:

                break;
            case State.CHASE:

                break;
            default:
                break;
        }
    }
}
