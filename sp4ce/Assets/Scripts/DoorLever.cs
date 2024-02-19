using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLever : MonoBehaviour, IInteract
{
    [SerializeField]
    private MultiLeverDoor doorTarget;

    bool isOn;

    void Awake()
    {
        isOn = false;
    }

    public string GetItemName()
    {
        return "";
    }

    public void OnHover()
    {
        UIManager.instance.OnHover(isOn?"Switch off":"Switch On");
    }

    public void OnInteract(GameObject inventory)
    {
        isOn = !isOn;
        if(isOn) doorTarget.AddPower();
        else doorTarget.RemovePower();
    }
}
