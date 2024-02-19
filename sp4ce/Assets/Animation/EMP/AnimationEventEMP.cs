using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventEMP : MonoBehaviour
{
    [SerializeField] private EnemyEMP emp;
    private Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();
        col.enabled = false;
    }

    public void Blast()
    {
        emp.Blast();
    }

    public void ColliderToggle()
    {
        if (col.enabled)
            col.enabled = false;
        else
            col.enabled = true;
    }

    public void AttackDone()
    {
        emp.AttackDone();
    }

    private void OnTriggerEnter(Collider other)
    {
        emp.AttackPlayer();
    }
}
