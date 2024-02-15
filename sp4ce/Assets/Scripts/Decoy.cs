using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Decoy : MonoBehaviour, IItem
{
    [SerializeField]
    private List<GameObject> objectsInRange;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private AudioSource src;

    [SerializeField]
    private AudioClip pulseClip;

    public int GetItemID()
    {
        return 2;
    }

    public void GetRequiredControllers(GameObject obj, GameObject sightController)
    {

    }

    public void OnPrimaryAction()
    {
        StartCoroutine(Throw());
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

    void Start()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void Update()
    {
        
        if(transform.parent != null)
            transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position + (Camera.main.transform.forward + (Camera.main.transform.right - Camera.main.transform.up) * 0.5f) * 0.5f, Time.deltaTime * 25f);
    }

    int pulses = 5;
    private IEnumerator Throw()
    {
        rb.constraints = RigidbodyConstraints.None;
        transform.parent = null;
        rb.AddForce(Camera.main.transform.forward * 15f + Camera.main.transform.up, ForceMode.Impulse);

        yield return new WaitForSeconds(5);
        StartCoroutine(Pulse());
    }

    private IEnumerator Pulse()
    {
        if(pulses <= 0) yield break;
        pulses--;
        src.PlayOneShot(pulseClip);
        foreach(GameObject obj in objectsInRange)
        {
            obj.GetComponent<IAudioObserver>().Notify(transform.position);
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(Pulse());
    }

    void OnTriggerEnter(Collider other)
    {
        IAudioObserver iao = other.gameObject.GetComponent<IAudioObserver>();
        if(iao != null)
        {
            objectsInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(objectsInRange.Contains(other.gameObject))
        {
            objectsInRange.Remove(other.gameObject);
        }
    }

    public bool IsItemInUse()
    {
        return false;
    }
}
