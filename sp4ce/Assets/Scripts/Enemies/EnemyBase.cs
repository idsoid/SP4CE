using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class EnemyBase : MonoBehaviour
{
    protected int damage;
    protected float speed;

    private bool allowAttack = false;

    //Move
    public virtual void Move(Transform targetTransform)
    {
        transform.Translate(speed * Time.deltaTime * (targetTransform.transform.position - transform.position).normalized);
    }

    //Attack
    public virtual void AttackPlayer()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2.5f))
        {
            if (hit.transform.CompareTag("Player"))
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
}
