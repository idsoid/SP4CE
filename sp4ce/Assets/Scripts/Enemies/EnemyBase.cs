using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public abstract class EnemyBase : MonoBehaviour
{
    protected int damage;
    protected float speed;
    protected NavMeshAgent agent;
    protected GameObject target;
    
    protected enum State
    {
        IDLE,
        PATROL,
        CHASE,
        FLEE
    }
    protected State currentState;

    protected bool allowAttack = false;

    //Move
    public virtual void Move(GameObject targetTransform)
    {
        agent.speed = speed;
        agent.SetDestination(targetTransform.transform.position);
    }

    //Attack
    public virtual void AttackPlayer()
    {
        if (Physics.Linecast(transform.position, target.transform.position, out RaycastHit hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                hit.transform.GetComponent<IHealth>().UpdateHealth(-damage);
            }
        }
        allowAttack = false;
        Invoke(nameof(ReadyToAttack), 1.0f);
    }
    public void ReadyToAttack()
    {
        allowAttack = true;
    }

    //StateManager
    public abstract void FSM();
}
