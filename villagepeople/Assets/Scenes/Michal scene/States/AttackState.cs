using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{


    public override State RunState()
    {
        Debug.Log("Attaaaaaaack!");
        return this;
    }
}
