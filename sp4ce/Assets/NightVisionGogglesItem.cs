using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class NightVisionGogglesItem : MonoBehaviour, IItem
{
    [SerializeField] private Volume globalVolume;
    [SerializeField] private GameObject model;
    [SerializeField] float fTime_elapsed;

    [Header("Night Vision Enemy")]
    [SerializeField] private GameObject nvEnemyPrefab;
    [SerializeField] private float spawnChance;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private LayerMask spawnLayer;

    private bool isOn;
    private GameObject spawnedEnemy;
    private float temperature;
    private Coroutine toggleCoroutine;

    void Start()
    {
        fTime_elapsed = 2f;
        isOn = false;
        temperature = 49.0f;
    }

    public int GetItemID()
    {
        return 1;
    }

    public void GetRequiredControllers(GameObject obj, GameObject sightController)
    {

    }

    public void OnPrimaryAction()
    {
        if(fTime_elapsed > 1f)
        {
            if(!isOn && temperature > 70f) return;
            fTime_elapsed = 0f;
            isOn = !isOn;
            toggleCoroutine = StartCoroutine(ToggleNightVision());
        }
    }

    public void OnPrimaryActionRelease()
    {
        
    }

    public void OnSecondaryAction()
    {
        
    }

    public void OnSecondaryActionRelease()
    {
        
    }

    private IEnumerator ToggleNightVision()
    {
        if(isOn)
        {
            yield return new WaitForSeconds(0.5f);
            if(!isOn) yield break;
            model.SetActive(false);
            if(globalVolume.profile.TryGet<NightVisionPostProcess>(out NightVisionPostProcess com))
            {
                com.active = true;
            }
            UIManager.instance.ToggleNightVisionUI();
            if (RandomNVEnemy())
                    SpawnNVEnemy();
        }
        else
        {
            isOn = true;
            yield return new WaitForSeconds(0.5f);
            if(globalVolume.profile.TryGet<NightVisionPostProcess>(out NightVisionPostProcess com))
            {
                com.active = false;
            }
            model.SetActive(true);
            UIManager.instance.ToggleNightVisionUI();
            isOn = false;
            if(temperature > 85f) temperature = 85f;
            if (spawnedEnemy)
                    Destroy(spawnedEnemy);
        }
        toggleCoroutine = null;
    }

    void Update()
    {
        if(isOn)
        {
            if(temperature > 90f && toggleCoroutine == null) 
            {
                //TODO: overheat
                fTime_elapsed = 0f;
                isOn = false;
                toggleCoroutine = StartCoroutine(ToggleNightVision());
            }
            else
            {
                temperature+=Time.deltaTime*2f;
                UIManager.instance.UpdateTemperatureText(temperature);
            }
        }
        else
        {
            temperature-=Time.deltaTime*0.5f;
            if(temperature < 49f) temperature = 49f;
        }
        fTime_elapsed += Time.deltaTime;
        if(globalVolume.profile.TryGet<NoisePostProcess>(out NoisePostProcess com))
        {
            if(fTime_elapsed <= 0.5f)
            {
                com.blend.value = Mathf.SmoothStep(0f,1f,fTime_elapsed * 2f);
            }
            else if (fTime_elapsed <= 1f)
            {
                com.blend.value = Mathf.SmoothStep(1f,0f,(fTime_elapsed-0.5f) * 4f);
            }
            else
            {
                com.blend.value = 0f;
            }
        }
        transform.LookAt(transform.position - Camera.main.transform.forward);
        if(isOn)
            transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position, Time.deltaTime * 5f);
        else
            transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position + (Camera.main.transform.forward + (Camera.main.transform.right - Camera.main.transform.up) * 0.5f) * 0.5f, Time.deltaTime * 5f);
    }

    public bool IsItemInUse()
    {
        return isOn;
    }

    private bool RandomNVEnemy()
    {
        int rng = Random.Range(0, 100);
        Debug.Log("RNG: " + rng);
        if (rng < spawnChance)
            return true;

        return false;
    }

    private void SpawnNVEnemy()
    {
        bool spawned = false;
        float x;
        float z;
        RaycastHit hit;

        x = transform.position.x + Random.Range(-distanceFromPlayer, distanceFromPlayer);
        z = transform.position.z + Random.Range(-distanceFromPlayer, distanceFromPlayer);

        if (Physics.Raycast(new Vector3(x, 1f, z), Vector3.down, out hit, 1.2f, spawnLayer))
        {
            spawnedEnemy = Instantiate(nvEnemyPrefab, hit.point, Quaternion.identity);
            spawned = true;
        }

        if (!spawned)
        {
            SpawnNVEnemy();
            Debug.Log("Retried spawned");
        }
    }
}
