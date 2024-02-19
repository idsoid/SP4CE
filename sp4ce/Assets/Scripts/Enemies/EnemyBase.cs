using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected List<Transform> patrolWaypoints = new();
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    [SerializeField] protected Transform target;

    protected NavMeshAgent agent;
    protected int currWaypoint = 0;
    protected bool allowAttack = false;

    protected Animator ar;

    //Move
    public virtual void Move(Transform targetTransform)
    {
        agent.speed = speed;
        agent.SetDestination(targetTransform.position);
    }

    public virtual void MoveToPos(Vector3 pos)
    {
        agent.speed = speed;
        agent.SetDestination(pos);
    }

    //Attack
    public virtual void AttackPlayer()
    {
        target.GetComponent<IHealth>().UpdateHealth(-damage);
        Debug.Log("hit");
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
