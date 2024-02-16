using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int accessLevel {get; private set;}

    [SerializeField]
    private List<GameObject> inventoryObject;

    private List<int> inventoryIds;

    public int currCheckpoint;
    
    void Awake()
    {
        instance = this;
        accessLevel = 0;
        currCheckpoint = 0;
    }

    void Start()
    {
        
    }

    public void IncreaseAccessLevel()
    {
        accessLevel++;
    }

    public void Save()
    {
        inventoryIds.Clear();
        foreach(GameObject obj in inventoryObject)
        {
            inventoryIds.Add(obj.GetComponent<IItem>().GetItemID());
        }

        //TODO: save it




    }
}

//for ref only nino
class SaveData
{
    List<int> inventoryIds;
    int lastCheckpoint;
    int health;
    int lastAccessLevel;
}
