using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool timeStopped;
    void Awake()
    {
        timeStopped = false;
        instance = this;
    }

    
}
