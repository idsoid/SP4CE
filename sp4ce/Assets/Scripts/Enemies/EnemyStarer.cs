using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStarer : EnemyBase, ISightObserver
{
    [SerializeField]
    private GameObject playerTarget;
    [SerializeField]
    private Transform eyeSight;
    [SerializeField]
    private List<GameObject> patrolWaypoints = new();
    private int currWaypoint = 0;
    private bool goingUp = false;

    private float walkSpeed = 3.5f;
    private float chaseSpeed = 7.0f;
    private GameObject currTarget;
    private float waitTime = 0.0f;

    public bool canMove;
    public bool playerSpotted;
    public bool isSeen;
    public bool isFlashed;

    // Start is called before the first frame update
    void Start()
    {
        damage = 20;
        speed = walkSpeed;
        canMove = playerSpotted = false;
        agent = GetComponent<NavMeshAgent>();
        currentState = State.PATROL;
        currTarget = patrolWaypoints[currWaypoint];
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSeen();
        FSM();
        Move(currTarget);
    }

    //See Player
    public void PlayerSeen()
    {
        if (Physics.Linecast(eyeSight.position, playerTarget.transform.position, out RaycastHit hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.DrawLine(eyeSight.position, hit.point, Color.green);
                playerSpotted = true;
            }
            else
            {
                Debug.DrawLine(eyeSight.position, hit.point, Color.red);
                playerSpotted = false;
            }
        }
    }

    //Seen
    public void OnLookAway()
    {
        canMove = true;
    }
    public void OnSighted()
    {
        Invoke(nameof(Freeze), 1.0f);
    }
    public void Freeze()
    {
        canMove = false;
    }

    //StateManager
    public override void FSM()
    {
        switch (currentState)
        {
            case State.IDLE:
                waitTime += Time.deltaTime;
                if (waitTime >= 1.0f)
                {
                    if (!isSeen)
                    {
                        //Patrol
                        if (!playerSpotted)
                        {
                            currWaypoint = goingUp ? currWaypoint++ : currWaypoint--;
                            if ((currWaypoint + 1) > patrolWaypoints.Count || (currWaypoint - 1) < 0)
                            {
                                goingUp = !goingUp;
                            }
                            currTarget = patrolWaypoints[currWaypoint];
                            speed = walkSpeed;
                            currentState = State.PATROL;
                        }
                        //Chase
                        else if (playerSpotted)
                        {
                            currTarget = playerTarget;
                            speed = chaseSpeed;
                            currentState = State.CHASE;
                        }
                    }
                    //Spotted by player
                    else
                    {
                        waitTime = 0.0f;
                    }
                }
                break;
            case State.PATROL:
                if (!isSeen)
                {
                    //Idle
                    if (Vector3.Distance(currTarget.transform.position, transform.position) <= 0.75f)
                    {
                        speed = 0;
                        currentState = State.IDLE;
                    }
                    //Chase
                    else if (playerSpotted)
                    {
                        currTarget = playerTarget;
                        speed = chaseSpeed;
                        currentState = State.CHASE;
                    }
                }
                //Spotted by player
                else
                {
                    speed = 0;
                    currentState = State.IDLE;
                }
                break;
            case State.CHASE:
                if (!isSeen) 
                {
                    //Attack
                    if (Vector3.Distance(currTarget.transform.position, transform.position) <= 0.75f)
                    {
                        AttackPlayer();
                        speed = 0;
                        currentState = State.IDLE;
                    }
                }
                else
                {
                    speed = 0;
                    currentState = State.IDLE;
                }
                break;
            case State.FLEE:
                
                break;
            default:
                break;
        }
    }
}
