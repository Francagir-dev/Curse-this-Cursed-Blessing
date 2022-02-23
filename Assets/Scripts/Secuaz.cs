using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secuaz : Enemy
{
    public override void ChangeStates()
    {
    }

    protected override string RandomizeSkill()
    {
        return skillNames[Random.Range(0, skillNames.Length)];
    }
}
