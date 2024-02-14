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

    private bool canMove;
    private bool inSight;

    // Start is called before the first frame update
    void Start()
    {
        damage = 20;
        speed = 3.5f;
        canMove = inSight = false;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSeen();
        if (inSight)
        {
            Move(playerTarget);
        }
    }

    //See Player
    public void PlayerSeen()
    {
        if (Physics.Linecast(eyeSight.position, playerTarget.transform.position, out RaycastHit hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.DrawLine(eyeSight.position, hit.point, Color.green);
                inSight = true;
            }
            else
            {
                Debug.DrawLine(eyeSight.position, hit.point, Color.red);
                inSight = false;
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
}
