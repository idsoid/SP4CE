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

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<ISightObserver>() != null)
        {
            if(!objectsInRange.Contains(other.gameObject))
                objectsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInRange.Remove(other.gameObject);
    }

    void Update()
    {
        CheckObjectsInRange();
        SightObjectsInVision();
    }

    public void CheckObjectsInRange()
    {
        for(int i = 0; i < objectsInRange.Count; i++)
        {
            if(!objectsInRange[i])
            {
                objectsInRange.RemoveAt(i);
            }
        }
    }
    private void SightObjectsInVision()
    {
        foreach(GameObject obj in objectsInRange)
        {
            if(obj==null)continue;
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

    public List<GameObject> GetObjectsInRange(float thresholdAngle)
    {
        List<GameObject> visibleObjects = new List<GameObject>();
        foreach(GameObject obj in objectsInRange)
        {
            ISightObserver sightobj = obj.GetComponent<ISightObserver>();
            if(sightobj!=null)
            {
                Vector3 targetDir = (obj.transform.position - transform.position).normalized;
                if(Physics.Raycast(transform.position, targetDir,out RaycastHit hit, sphereCollider.radius))
                {
                    if(hit.transform.gameObject != obj) continue;
                    if(Vector3.Angle(targetDir,Camera.main.transform.forward) < thresholdAngle)
                    {
                        visibleObjects.Add(obj);
                    }
                }
            }
        }
        return visibleObjects;
    }
}
