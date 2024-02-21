using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;
using System.Text;
using Unity.VisualScripting;

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

    public bool bGameOver;

    public Checkpoint checkpt;

    public GameObject lastHitEnemy;

    public bool nvIsOn;

    [SerializeField]
    private GameObject settings;
    
    void Awake()
    {
        bGameOver = false;
        instance = this;
        accessLevel = 0;
        currCheckpoint = 0;
        isInUI = false;
        deathScreen.SetActive(false);
        winScreen.SetActive(false);
        settings.SetActive(false);
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

        // GameObject[] floors = GameObject.FindGameObjectsWithTag("Object");
        // foreach (GameObject floor in floors)
        // {
        //     data.objName = obj.name;
        //     data.posX = obj.transform.position.x;
        //     data.posY = obj.transform.position.y;
        //     data.posZ = obj.transform.position.z;
        //     string json = JsonUtility.ToJson(data, true);
        //     Debug.Log(json.ToString());
        //     sb.AppendLine(json);
        // }

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
    [SerializeField] private GameObject deathButtons;
    [SerializeField] private GameObject map;

    public void Die()
    {
        
        deathButtons.SetActive(false);
        deathImage.material.SetFloat("_Effect", 0f);
        skullImage.material.SetFloat("_Effect", 0f);
        deathScreen.SetActive(true);
        StartCoroutine(FadeDeathScreen());
    }

    public void DisableMap()
    {
        map.SetActive(false);
    }
    
    private IEnumerator FadeDeathScreen()
    {
        for(float eff = 0f; eff < 1f; eff+=0.1f)
        {
            deathImage.material.SetFloat("_Effect",eff);
            skullImage.material.SetFloat("_Effect",eff);
            yield return new WaitForSeconds(0.1f);
        }
        Cursor.lockState = CursorLockMode.None;
        deathButtons.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !bGameOver)
        {
            if (!settings.active)
            {
                isInUI = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                settings.SetActive(true);
            }
            else
            {
                isInUI = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
                settings.SetActive(false);
            }
        }

        if(deathButtons.activeInHierarchy)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                bGameOver = true;
                RestartGame();
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void Escape()
    {
        winScreen.SetActive(true);
        isInUI = true;
        bGameOver = true;
    }
}
