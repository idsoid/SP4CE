using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int accessLevel {get; private set;}
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        accessLevel = 0;
    }

    public void IncreaseAccessLevel()
    {
        accessLevel++;
    }

}
