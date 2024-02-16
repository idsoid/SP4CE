using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int accessLevel {get; private set;}

    [SerializeField]
    private List<GameObject> inventoryObject = new();

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private PlayerData playerData;

    private List<int> inventoryIds = new();

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


        //Saving player data
        playerData.InventoryIDs = inventoryIds;
        playerData.LastCheckpoint = currCheckpoint;
        playerData.LastAccessLevel = accessLevel;
        playerData.Health = player.GetComponent<IHealth>().GetHealth();
        playerData.SaveData();
    }

    public void Load()
    {
        playerData.LoadData();
    }
}
