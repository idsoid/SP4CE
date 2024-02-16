using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    private List<int> inventoryIds;
    private int lastCheckpoint;
    private int health;
    private int lastAccessLevel;
    private List<string> discoveryIndex;

    public List<int> InventoryIDs { get => inventoryIds; set => inventoryIds = value; }
    public int LastCheckpoint { get => lastCheckpoint; set => lastCheckpoint = value; }
    public int Health { get => health; set => health = value; }
    public int LastAccessLevel { get => lastAccessLevel; set => lastAccessLevel = value; }
    public List<string> DiscoveryIndex { get => discoveryIndex; set => discoveryIndex = value; }
    public void SaveData()
    {
        string s = JsonUtility.ToJson(this);
        if (FileManager.WriteToFile("playerdata.json", s))
        {
            Debug.Log("Save player data successful");
        }
    }
    public void LoadData()
    {
        if (FileManager.LoadFromFile("playerdata.json", out string s))
        {
            JsonUtility.FromJsonOverwrite(s, this);
        }
    }
}
