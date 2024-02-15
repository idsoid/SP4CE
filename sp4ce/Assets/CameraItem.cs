using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraItem : MonoBehaviour, IItem
{
    private bool isAiming;

    [SerializeField]
    private Material screenMaterial;

    [SerializeField]
    private Material whiteMaterial;

    [SerializeField]
    private MeshRenderer mr;

    [SerializeField]
    private SightController sc;

    [SerializeField]
    private Light lightSource;

    private float cd;

    void Start()
    {
        cd = 0f;
        isAiming = false;
        lightSource.enabled = false;
    }

    public int GetItemID()
    {
        return 0;
    }

    public void OnPrimaryAction()
    {
        if(isAiming && cd <= 0f)
        {
            StartCoroutine(TakePhoto());
        }
    }

    public void OnPrimaryActionRelease()
    {
    }

    public void OnSecondaryAction()
    {
        isAiming = true;
    }

    public void OnSecondaryActionRelease()
    {
        isAiming = false;
    }

    void Update()
    {
        if(cd > 0f)
        {
            cd -= Time.deltaTime;
        }
        transform.LookAt(transform.position - Camera.main.transform.forward);
        if(isAiming)
            transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position + Camera.main.transform.forward * 0.1f, Time.deltaTime * 25f);
        else
            transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position + (Camera.main.transform.forward + (Camera.main.transform.right - Camera.main.transform.up) * 0.5f) * 0.5f, Time.deltaTime * 25f);
    }

    private IEnumerator TakePhoto()
    {
        foreach(GameObject obj in sc.GetObjectsInRange())
        {
            IPhotoObserver ipo = obj.GetComponent<IPhotoObserver>();
            if(ipo != null)
            {
                ipo.OnPhotoTaken();
            }
        }
        cd = 2f;
        mr.material = whiteMaterial;
        lightSource.enabled = true;
        yield return new WaitForSeconds(0.1f);
        mr.material = screenMaterial;
        lightSource.enabled = false;
    }

    public void GetRequiredControllers(GameObject obj, GameObject sightController)
    {
        sc = sightController.GetComponent<SightController>();
    }

    public bool IsItemInUse()
    {
        return isAiming;
    }
}
