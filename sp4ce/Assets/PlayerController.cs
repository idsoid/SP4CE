using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHealth
{
    [SerializeField]
    private List<GameObject> inventory;

    [SerializeField]
    private GameObject inventoryObject;

    [SerializeField]
    private GameObject sightController;

    private int equippedIndex;

    void Start()
    {
        GetInventory();

        equippedIndex = 0;
        foreach(GameObject obj in inventory)
        {
            obj.SetActive(false);
        }
        inventory[equippedIndex].SetActive(true);
    }

    void Update()
    {
        sightController.transform.position = transform.position;
        if(Input.GetMouseButtonDown(0))
        {
            inventory[equippedIndex].GetComponent<IItem>().OnPrimaryAction();
        }
        if(Input.GetMouseButtonUp(0))
        {
            inventory[equippedIndex].GetComponent<IItem>().OnPrimaryActionRelease();
        }

        if(Input.GetMouseButtonDown(1))
        {
            inventory[equippedIndex].GetComponent<IItem>().OnSecondaryAction();
        }
        if(Input.GetMouseButtonUp(1))
        {
            inventory[equippedIndex].GetComponent<IItem>().OnSecondaryActionRelease();
        }
    }

    void GetInventory()
    {
        inventory.Clear();
        //get inventory
        foreach(Transform child in inventoryObject.transform)
        {
            child.GetComponent<IItem>().GetRequiredControllers(gameObject, sightController);
            inventory.Add(child.gameObject);
        }
    }

    public void UpdateHealth(int amt)
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void InitHealth()
    {
        throw new System.NotImplementedException();
    }
}
