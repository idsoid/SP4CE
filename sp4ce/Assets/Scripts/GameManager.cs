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
        accessLevel = 0;
    }

    void Start()
    {
        
    }

    public void IncreaseAccessLevel()
    {
        accessLevel++;
    }

}
