using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secuaz : Enemy
{
    protected override void ChangeStates()
    {
        if (scare <= 0)
            ChangeState(States.Scared);
    }

    protected override string RandomizeSkill()
    {
        return skillNames[Random.Range(0, skillNames.Length)];
    }
}
