using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashlightItem : MonoBehaviour, IItem
{
    bool isOn;

    [SerializeField]
    private Light lightSource;

    [SerializeField]
    private AudioSource src;

    [SerializeField]
    private AudioClip clickSound;

    [SerializeField]
    private AudioClip equipSound;

    public int GetItemID()
    {
        return 3;
    }

    void OnEnable()
    {
        src.PlayOneShot(equipSound);
    }

    public void GetRequiredControllers(GameObject obj, GameObject sightController)
    {

    }

    public bool IsItemInUse()
    {
        return false;
    }

    public void OnPrimaryAction()
    {
        isOn = !isOn;
        lightSource.enabled = isOn;
        PlayerAudioController.instance.PlayAudio(AUDIOSOUND.FLASHLIGHT);
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

    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        lightSource.enabled = isOn;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
        transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position + (Camera.main.transform.forward + (Camera.main.transform.right - Camera.main.transform.up) * 0.5f) * 0.5f, Time.deltaTime * 25f);
    }

    public void RunBackgroundProcesses()
    {

    }
}