using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEMP : EnemyBase
{


    private new enum State
    {
        PATROL,
        BLAST,
        CHARGE,
        CHASE
    }

    private new State currentState;

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
            case State.PATROL: 
               
                break;
            case State.BLAST:

                break;
            case State.CHARGE:

                break;
            case State.CHASE:

                break;
            default:
                break;
        }
    }
}
