using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndrewTestScript2 : MonoBehaviour, IPhotoObserver
{
    public void OnPhotoTaken()
    {
        Destroy(gameObject);
    }
}
