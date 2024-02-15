using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ItemKeyCard : MonoBehaviour, IInteract
{
    public string GetItemName()
    {
        return "KeyCard";
    }

    public void OnHover()
    {
        UIManager.instance.OnHover(GetItemName());
    }

    public void OnInteract(GameObject inventory)
    {
        UIManager.instance.OnHoverExit();
        Destroy(gameObject);    
    }
}
