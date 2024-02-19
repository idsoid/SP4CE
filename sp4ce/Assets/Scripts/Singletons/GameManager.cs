using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;

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

    [SerializeField] 
    private GameObject winScreen;

    private List<int> inventoryIds = new();

    public int currCheckpoint;

    public bool isInUI;

    public Checkpoint checkpt;

    public GameObject lastHitEnemy;
    
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
    [SerializeField] private TMP_Text restartText;

    public void Die()
    {
        restartText.gameObject.SetActive(false);
        deathImage.material.SetFloat("_Effect", 0f);
        skullImage.material.SetFloat("_Effect", 0f);
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

        restartText.gameObject.SetActive(true);
    }

    void Update()
    {
        if(restartText.gameObject.activeInHierarchy)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void Escape()
    {
        Debug.Log("Escape");
        winScreen.SetActive(true);
        isInUI = true;
    }
}
