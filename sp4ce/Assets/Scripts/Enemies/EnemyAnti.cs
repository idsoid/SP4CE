using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnti : EnemyBase
{
    [Header("Anti-NV")]
    [SerializeField] private Material material;
    [SerializeField] private float rageBuildUp = 1f;

    private enum State
    {
        IDLE,
        RAGE,
    }

    private State currState;
    private float rage;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        ChangeState(State.IDLE);
    }

    private void Update()
    {
        FSM();
    }

    private void ChangeState(State newState)
    {
        if (newState == State.IDLE)
        {
            rage = 0f;
            material.SetFloat("_REFLECTIONS_WEIGHT", rage);
        }
        else if (newState == State.RAGE)
        {
            
        }

        currState = newState;
    }

    public override void FSM()
    {
        if (currState == State.IDLE)
        {
            Vector3 direction = target.position - transform.position;
            transform.forward = direction;

            rage += rageBuildUp * Time.deltaTime;
            material.SetFloat("_REFLECTIONS_WEIGHT", rage);

            if (rage >= 50f)
                ChangeState(State.RAGE);
        }
        else if (currState == State.RAGE)
        {
            Move(target);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}