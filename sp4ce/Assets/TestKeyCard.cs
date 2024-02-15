using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKeyCard : MonoBehaviour, IPhotoObserver, ISightObserver
{
    public void OnLookAway()
    {
        //throw new System.NotImplementedException();
    }

    public void OnPhotoTaken()
    {
        Debug.Log("yeah");
        GameManager.instance.IncreaseAccessLevel();
        Destroy(gameObject);
    }

    public void OnSighted()
    {
        //throw new System.NotImplementedException();
    }
}
