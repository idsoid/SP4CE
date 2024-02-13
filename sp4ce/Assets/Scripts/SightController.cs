using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SightController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objectsInRange;

    [SerializeField]
    private SphereCollider sphereCollider;

    [SerializeField]
    private GameObject followPlayer;

    private void OnTriggerStay(Collider other)
    {
        if(!objectsInRange.Contains(other.gameObject))
            objectsInRange.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInRange.Remove(other.gameObject);
    }

    void Update()
    {
        transform.position = followPlayer.transform.position;
        SightObjectsInVision();
    }

    private void SightObjectsInVision()
    {
        foreach(GameObject obj in objectsInRange)
        {
            ISightObserver sightobj = obj.GetComponent<ISightObserver>();
            if(sightobj!=null)
            {
                Vector3 targetDir = (obj.transform.position - transform.position).normalized;
                if(Physics.Raycast(transform.position, targetDir, sphereCollider.radius))
                {
                    if(Vector3.Angle(targetDir,Camera.main.transform.forward) < 60)
                    {
                        sightobj.OnSighted();
                    }
                    else
                    {
                        sightobj.OnLookAway();
                    }
                }
            }
        }
    }
}
