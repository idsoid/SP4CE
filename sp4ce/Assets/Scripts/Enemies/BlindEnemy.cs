using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlindEnemy : EnemyBase, IAudioObserver
{
    [SerializeField]
    Vector3 audioPosition = Vector3.zero;

    [SerializeField]
    private List<Transform> waypoints;

    [SerializeField]
    int waypointIndex;

    private bool isChasingAudio;

    public override void FSM()
    {
        if(isChasingAudio)
        {
            if(Vector3.Distance(audioPosition,transform.position) > 1)
                MoveToPos(audioPosition);
            else
            {
                StartCoroutine(ReturnToPatrol());
            }
        }
        else
        {
            if(Vector3.Distance(waypoints[waypointIndex].position,transform.position) < 1)
            {
                waypointIndex = UnityEngine.Random.Range(0,waypoints.Count);
            }
            else
            {
                MoveToPos(waypoints[waypointIndex].position);
            }
        }
    }

    private IEnumerator ReturnToPatrol()
    {
        yield return new WaitForSeconds(3f);
        speed = 5f;
        isChasingAudio = false;
    }

    public void Notify(Vector3 position)
    {
        audioPosition = position;
        isChasingAudio = true;
        speed = 15f;
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        speed = 5f;
        isChasingAudio = false;
        waypointIndex = 0;
    }

    void Update()
    {
        FSM();
    }
}
