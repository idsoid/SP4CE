using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2))
        {
            hit.transform.GetComponent<IInteract>()?.OnHover();
            if (Input.GetButtonDown("Interact"))
                hit.transform.GetComponent<IInteract>().OnInteract(inventory);
        }
        else
            UIManager.instance.OnHoverExit();
    }
}
