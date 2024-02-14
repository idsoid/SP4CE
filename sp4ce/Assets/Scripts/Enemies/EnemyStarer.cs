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

    private bool canMove;
    private bool playerSpotted;
    private GameObject currTarget;
    private float waitTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        damage = 20;
        speed = 3.5f;
        canMove = playerSpotted = false;
        agent = GetComponent<NavMeshAgent>();
        currentState = State.PATROL;
        currTarget = patrolWaypoints[currWaypoint];
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSeen();
        Move(currTarget);
        //if (playerSpotted && canMove)
        //{
        //    Move(playerTarget);
        //}
        //else if (!playerSpotted && canMove) 
        //{
        //    Move(patrolWaypoints[currWaypoint]);
        //}
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
                if (waitTime >= 1.0f && !playerSpotted)
                {
                    currWaypoint = goingUp ? currWaypoint++ : currWaypoint--; 
                    if ((currWaypoint + 1) > patrolWaypoints.Count || (currWaypoint - 1) < 0)
                    {
                        goingUp = !goingUp;
                    }
                    currTarget = patrolWaypoints[currWaypoint];
                    currentState = State.PATROL;
                }
                else if (playerSpotted) 
                {
                    currentState = State.CHASE;
                }
                break;
            case State.PATROL:
                
                break;
            case State.CHASE:
                
                break;
            case State.FLEE:
                
                break;
            default:
                break;
        }
    }
}
