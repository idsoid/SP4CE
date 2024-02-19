using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private GameObject deathScreen;

    private List<int> inventoryIds = new();

    public int currCheckpoint;

    public bool isInUI;

    public Checkpoint checkpt;
    
    void Awake()
    {
        instance = this;
        accessLevel = 0;
        currCheckpoint = 0;
        isInUI = false;
        deathScreen.SetActive(false);
    }

    void Start()
    {
        
    }

    public void SetAccessLevel(int lvl)
    {
        if(accessLevel < lvl)
            accessLevel = lvl;
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

    public void OnCheckpointChanged()
    {
        checkpt?.DisableCheckpoint();
    }

    [SerializeField] private Image deathImage;
    [SerializeField] private Image skullImage;
    public void Die()
    {
        deathImage.material.SetFloat("_Effect",0f);
        skullImage.material.SetFloat("_Effect",0f);
        deathScreen.SetActive(true);
        StartCoroutine(FadeDeathScreen());
    }
    private IEnumerator FadeDeathScreen()
    {
        for(float eff = 0f; eff < 1f; eff+=0.1f)
        {
            deathImage.material.SetFloat("_Effect",eff);
            skullImage.material.SetFloat("_Effect",eff);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
