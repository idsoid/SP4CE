using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour, ISightObserver, IPhotoObserver
{
    public void OnLookAway()
    {
    }

    public void OnPhotoTaken()
    {
        //TODO: Save
        GameManager.instance.Save();
    }

    public void OnSighted()
    {
    }
}
