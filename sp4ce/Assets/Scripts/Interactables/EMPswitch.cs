using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EMPswitch : MonoBehaviour, IInteract
{
    [SerializeField] private EnemyEMP emp;

    public string GetItemName()
    {
        return "Press E to deactivate";
    }

    public void OnHover()
    {
        if (emp.active)
            UIManager.instance.OnHover(GetItemName());
    }

    public void OnInteract(GameObject inventory)
    {
        if (emp.active)
        {
            UIManager.instance.OnHoverExit();
            emp.active = false;
            emp.timer = emp.restTimerSet;
        }
    }
}
