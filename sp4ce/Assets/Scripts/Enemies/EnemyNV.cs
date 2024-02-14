using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNV : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void FSM()
    {
        switch (currentState)
        {
            case State.IDLE:
                
                break;
            case State.PATROL:

                break;
            case State.CHASE:

                break;
            case State.FLEE:

                break;
            default:
                break;
        }
    }
}
