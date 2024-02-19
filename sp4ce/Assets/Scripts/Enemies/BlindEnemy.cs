using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class BlindEnemy : EnemyBase, IAudioObserver, ISightObserver, IPhotoObserver
{
    [SerializeField]
    Vector3 audioPosition = Vector3.zero;

    [SerializeField]
    private List<Transform> waypoints;

    [SerializeField]
    int waypointIndex;

    private bool isChasingAudio;
    private Animator animator;

    //year
    private GameObject targetObject;

    public override void FSM()
    {
        if(isChasingAudio)
        {
            if(Vector3.Distance(audioPosition,transform.position) > 1)
            {
                MoveToPos(audioPosition);
                animator.SetInteger("Move", 2);
            }
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
                animator.SetInteger("Move", 1);
            }
        }
    }

    private IEnumerator ReturnToPatrol()
    {
        yield return new WaitForSeconds(3f);
        speed = 5f;
        isChasingAudio = false;
    }

    public void Notify(Vector3 position, GameObject source)
    {
        if(targetObject != null)
        {
            if(targetObject.CompareTag("Decoy"))
            {
                return;
            }
        }
        animator.SetTrigger("Attack");
        targetObject = source;
        Debug.Log("hi");
        audioPosition = position;
        isChasingAudio = true;
        speed = 15f;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        speed = 5f;
        isChasingAudio = false;
        waypointIndex = 0;
    }
    void Update()
    {
        FSM();
    }

    public void OnSighted()
    {
    }

    public void OnLookAway()
    {
    }

    public void OnPhotoTaken()
    {
        UIManager.instance.DisplayTip("EYELESS", "BLIND BUT EXTREMELY SENSITIVE TO SOUND.\nPROCEED WITH CAUTION.", true);
    }

    public string GetDetails()
    {
        return "DANGER";
    }
}
