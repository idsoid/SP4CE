using UnityEngine;

public class LevelDoor : MonoBehaviour, IInteract
{
    [SerializeField] private bool startOpen;
    [SerializeField] private float openSpeed;
    [SerializeField] private Transform door;
    [SerializeField] private Transform closePosition;
    [SerializeField] private Transform openPosition;

    private bool open;

    private void Start()
    {
        open = startOpen;
        if (open)
            door.position = openPosition.position;
        else
            door.position = closePosition.position;
    }

    private void Update()
    {
        if (open)
            door.position = Vector3.Slerp(door.position, openPosition.position, openSpeed);
        else
            door.position = Vector3.Slerp(door.position, closePosition.position, openSpeed);
    }

    private void ToggleDoor()
    {
        open = !open;
    }

    public string GetItemName()
    {
        return null;
    }

    public void OnHover()
    {
        
    }

    public void OnInteract(GameObject inventory)
    {
        ToggleDoor();
    }
}
