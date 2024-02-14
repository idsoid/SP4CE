using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class EnemyBase : MonoBehaviour
{
    protected int damage;
    protected float speed;
    protected string state;

    private bool allowAttack = false;

    public virtual void AttackPlayer(Transform enemyTransform)
    {
        if (Physics.Raycast(enemyTransform.position, enemyTransform.forward, out RaycastHit hit, 2.5f))
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
