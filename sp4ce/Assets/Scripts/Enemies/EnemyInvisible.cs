using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyInvisible : EnemyBase
{
    [SerializeField] private float timeBetweenPhase;
    
    [Header("Idle State")]
    [SerializeField] private float idleTime;

    private enum State
    {
        IDLE,
        PATROL,
        ATTACK,
    }

    private State currState;
    private bool phase = true;
    private bool isSeen;
    private float phaseTimer = 0f;
    private float idleTimer;

    private void Start()
    {
        currState = State.PATROL;
    }

    private void Update()
    {
        phaseTimer += Time.deltaTime;
        if (phaseTimer >= timeBetweenPhase)
        {
            phaseTimer = 0f;
            phase = !phase;
        }

        if (phase)
            SetIsSeen(true);
        else if (!phase && GameManager.instance.nvIsOn)
            SetIsSeen(true);
        else
            SetIsSeen(false);
    }

    private void SetIsSeen(bool newIsSeen)
    {
        isSeen = newIsSeen;
        if (isSeen)
        {
            gameObject.layer = 0;
            SetLayerInChildren(transform, 0);
        }
        else
        {
            gameObject.layer = 7;
            SetLayerInChildren(transform, 7);
        }
    }

    private void SetLayerInChildren(Transform transform, int layerInt)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.layer = layerInt;
            if (child.transform.childCount > 0)
                SetLayerInChildren(child, layerInt);
        }
    }

    private void ChangeState(State newState)
    {
        if (newState == State.IDLE)
        {
            idleTimer = 0f;
        }
        else if (newState == State.PATROL)
        {

        }
        else if (newState == State.ATTACK)
        {

        }

        currState = newState;
    }

    public override void FSM()
    {
        if (currState == State.IDLE)
        {
            
        }
        else if (currState == State.PATROL)
        {

        }
        else if (currState == State.ATTACK)
        {

        }
    }
}
