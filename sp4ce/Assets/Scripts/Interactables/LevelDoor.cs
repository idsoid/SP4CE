using System;
using UnityEngine;

public class LevelDoor : MonoBehaviour, IInteract, IPhotoObserver, ISightObserver
{
    [SerializeField] private bool startOpen;
    [SerializeField] private float openSpeed;
    [SerializeField] private Transform door;
    [SerializeField] private Transform closePosition;
    [SerializeField] private Transform openPosition;

    [SerializeField]
    private AudioClip doorOpen;

    [SerializeField]
    private AudioClip doorClose;

    [SerializeField]
    private AudioSource source;

    private BoxCollider collider;

    private bool open;

    [SerializeField]
    int accessLevel = 2;

    float fTime_elapsed;

    private void Start()
    {
        fTime_elapsed = 0f;
        collider = GetComponent<BoxCollider>();
        open = startOpen;

        if (open)
        {
            door.position = openPosition.position;
            collider.center = openPosition.localPosition;
        }
        else
        {
            door.position = closePosition.position;
            collider.center = closePosition.localPosition;
        }
    }

    private void Update()
    {
        if(fTime_elapsed < 1f) fTime_elapsed+=Time.deltaTime;
        if (open)
        {
            door.position = Vector3.Slerp(door.position, openPosition.position, openSpeed);
            collider.center = Vector3.Slerp(collider.center, openPosition.localPosition, openSpeed);
        }
        else
        {
            door.position = Vector3.Slerp(door.position, closePosition.position, openSpeed);
            collider.center = Vector3.Slerp(collider.center, closePosition.localPosition, openSpeed);
        }
    }

    private void ToggleDoor()
    {
        if(fTime_elapsed < 1f) return;
        if(accessLevel <= GameManager.instance.accessLevel)
        {
            fTime_elapsed = 0f;
            open = !open;
            UIManager.instance.OnHoverExit();

            if(open)
                source.PlayOneShot(doorOpen);
            else
                source.PlayOneShot(doorClose);
        }
    }

    public string GetItemName()
    {
        string text = null;
        if (!open)
            text = "Open door";
        else
            text = "Close door";
        
        return text;
    }

    public void OnHover()
    {
        if(fTime_elapsed < 1f) return;
        if(accessLevel > GameManager.instance.accessLevel)
            UIManager.instance.OnHover("Requires Level "+accessLevel+" access");
        else
        {
            UIManager.instance.OnHover("Open");
        }
    }

    public void OnInteract(GameObject inventory)
    {
        ToggleDoor();
    }

    public void OnPhotoTaken()
    {
        UIManager.instance.DisplayTip("Door", "This door needs level "+accessLevel+" access to open.");
    }

    public void OnSighted()
    {
    }

    public void OnLookAway()
    {
    }
}
