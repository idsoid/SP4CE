using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public void Save()
    {
        string s = JsonUtility.ToJson(this);
        if (FileManager.WriteToFile("playerdata.json", s))
        {
            Debug.Log("Save player data successful");
        }
    }
    public void Load()
    {
        if (FileManager.LoadFromFile("playerdata.json", out string s))
        {
            JsonUtility.FromJsonOverwrite(s, this);
        }
    }
}
