using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyEMP : EnemyBase, ISightObserver, IPhotoObserver
{
    [SerializeField] private Transform player;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float chaseSpeed = 10f;

    [SerializeField] private GameObject blastPrefab;

    [SerializeField]
    private float patrolTimerSet = 30f;
    private float chaseTimerSet = 20f;
    public float restTimerSet = 50f;
    public float timer;

    public bool active = true;

    private int chargeCount = 0;

    private enum State
    {
        PATROL,
        CHARGE,
        CHASE,
        ATTACK,
        REST
    }

    private State currentState;

    // Start is called before the first frame update
    void Start()
    {
        damage = 30;

        timer = patrolTimerSet;

        speed = walkSpeed;

        ar = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentState = State.PATROL;
        currWaypoint = 0;
        target = patrolWaypoints[currWaypoint];
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer);
        if (!active)
        {
            Debug.Log("deactivated");
            speed = 0;
            chargeCount = 0;
            ar.SetTrigger("rest");
            ar.SetBool("charge", false);
            ar.SetBool("chase", false);
            currentState = State.REST;
        }

        FSM();

        Move(target);

        ar.SetFloat("move", agent.velocity.normalized.magnitude);
    }

    public override void FSM()
    {
        switch (currentState)
        {
            case State.PATROL:
                timer -= Time.deltaTime;
                speed = walkSpeed;
                if (timer <= 0f && Random.Range(0, 100) > 40)
                {
                    currentState = State.CHARGE;
                    break;
                }
                else if (timer > 0)
                {
                    target = patrolWaypoints[currWaypoint];
                }
                else
                    timer = patrolTimerSet;

                if (agent.remainingDistance < .75f)
                    currWaypoint = Random.Range(0, patrolWaypoints.Count);
                break;
            case State.CHARGE:
                ar.SetBool("charge", true);
                speed = 0;
                if (chargeCount == 4)
                {
                    timer = chaseTimerSet;
                    ar.SetBool("charge", false);
                    currentState = State.CHASE;
                    chargeCount = 0;
                    break;
                }
                break;
            case State.CHASE:
                ar.SetBool("chase", true);
                speed = chaseSpeed;
                timer -= Time.deltaTime;

                if (agent.remainingDistance < 3)
                {
                    ar.SetBool("chase", false);
                    currentState = State.ATTACK;
                    break;
                }

                if (timer <= 0)
                {
                    timer = restTimerSet;
                    ar.SetBool("chase", false);
                    currentState = State.REST;
                    break;
                }

                speed = chaseSpeed;
                target = player;

                break;
            case State.ATTACK:
                speed = 0;
                agent.velocity = Vector3.zero;
                gameObject.transform.LookAt(target);
                ar.SetTrigger("attack");

                break;
            case State.REST:
                active = false;
                speed = 0;
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    active = true;
                    timer = patrolTimerSet;
                    currentState = State.PATROL;
                }

                break;
            default:
                break;
        }
    }

    public void Blast()
    {
        Instantiate(blastPrefab, transform);
        chargeCount++;
    }

    public void AttackDone()
    {
        ar.SetTrigger("rest");
        currentState = State.REST;
    }

    public void OnSighted()
    {
    }

    public void OnLookAway()
    {
    }

    public void OnPhotoTaken()
    {
        UIManager.instance.DisplayTip("EMP", "put ya desc here stoopid", true);
    }

    public string GetDetails()
    {
        return "DANGER";
    }
}
