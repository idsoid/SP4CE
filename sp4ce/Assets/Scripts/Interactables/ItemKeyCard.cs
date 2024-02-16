using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ItemKeyCard : MonoBehaviour, IInteract, IPhotoObserver, ISightObserver
{
    [SerializeField]
    private int accessLevel = 3;
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
        UIManager.instance.DisplayTip("Level 3 Access Keycard", "Access more of the facility.", true);
        GameManager.instance.SetAccessLevel(accessLevel);
        UIManager.instance.OnHoverExit();
        Destroy(gameObject);    
    }

    public void OnLookAway()
    {
    }

    public void OnPhotoTaken()
    {
        UIManager.instance.DisplayTip("Level 3 Access Keycard", "Access more of the facility.", true);
    }

    public void OnSighted()
    {
    }
}
