using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnti : EnemyBase
{
    private enum State
    {
        IDLE,
        KILL,
    }

    private State currState;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        currState = State.IDLE;
    }

    private void Update()
    {
        if (currState == State.IDLE)
        {
            Vector3 direction = target.position - transform.position;
            transform.forward = direction;
        }
        else if (currState == State.KILL)
        {
            Move(target);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public override void FSM()
    {
        
    }
}
