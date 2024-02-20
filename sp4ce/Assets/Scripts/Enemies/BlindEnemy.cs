using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BlindEnemy : EnemyBase, IAudioObserver, ISightObserver, IPhotoObserver
{
    [SerializeField]
    Vector3 audioPosition = Vector3.zero;

    [SerializeField]
    private List<Transform> waypoints;

    [SerializeField]
    int waypointIndex;

    [SerializeField]
    private bool isChasingAudio;

    [SerializeField]
    private Animator animator;

    //year
    private GameObject targetObject;

    Coroutine patrolCooldownCoroutine;
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
                if(patrolCooldownCoroutine==null)
                    patrolCooldownCoroutine = StartCoroutine(ReturnToPatrol());
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
        Debug.Log("returning to patrol...");
        yield return new WaitForSeconds(3f);
        speed = 3f;
        isChasingAudio = false;
        targetObject = null;
        patrolCooldownCoroutine = null;
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

        if(source.CompareTag("Player") && targetObject!=source) PlayerAudioController.instance.PlayAudio(AUDIOSOUND.ADRENALINE);
        targetObject = source;
        Debug.Log("hi");
        audioPosition = position;
        isChasingAudio = true;
        speed = 10f;
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        speed = 3f;
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
        UIManager.instance.DisplayTip("EYELESS", "BLIND BUT EXTREMELY SENSITIVE TO SOUND.\nPROCEED WITH CAUTION.", true, true);
    }

    public string GetDetails()
    {
        return "DANGER";
    }
}
