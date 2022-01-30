using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{

    public ChaseState chase;
    public override State RunState()
    {

        if (chase.CanAttack())
        {
            Debug.Log("Attaaaaaaack!");
            return this;
        }
        else
        {
            return chase;
        }
        



    }



}
