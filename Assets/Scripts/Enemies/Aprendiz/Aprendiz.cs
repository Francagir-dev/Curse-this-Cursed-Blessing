using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Aprendiz : Enemy
{
    private enum Stages
    {
        Heavy,
        Medium,
        Light
    }

    [SerializeField] private Stages stage = Stages.Heavy;

    protected override void ChangeStates()
    {
        if (scare > 33 && scare < 67)
        {
            stage = Stages.Medium;
        }
        else if (scare > 67 && scare < 100)
        {
            stage = Stages.Light;
        }
        else
        {
            ChangeState(States.Scared);
        }
    }

    protected override string RandomizeSkill()
    {
        int skillProbability;
        int numSkill = -1;
        float distance = Vector3.Distance(playerTransf.position, transform.position);
        Debug.Log(distance);
        switch (stage)
        {
            case Stages.Heavy:
                if (distance > offsetDistanceSkill)
                {
                    skillProbability = Random.Range(10, 30);
                    if (skillProbability >= 10 && skillProbability < 20)
                        numSkill = 1;
                    else numSkill = 2;
                }
                else
                {
                    numSkill = 0;
                }

                break;
            case Stages.Medium:
                if (distance > 16)
                {
                    skillProbability = Random.Range(30, 50);
                    if (skillProbability >= 30 && skillProbability < 40)
                        numSkill = 3;
                    else
                        numSkill = 4;
                }
                else
                {
                    skillProbability = Random.Range(30, 60);
                    if (skillProbability >= 30 && skillProbability < 40)
                        numSkill = 3;
                    else if (skillProbability >= 40 && skillProbability < 50)
                        numSkill = 4;
                    else
                        numSkill = 5;
                }

                break;
            case Stages.Light:
                skillProbability = Random.Range(60, 110);
                if (skillProbability >= 60 && skillProbability < 70)
                    numSkill = 6;
                else if (skillProbability >= 70 && skillProbability < 80)
                    numSkill = 7;
                else if (skillProbability >= 80 && skillProbability < 90)
                    numSkill = 8;
                else if (skillProbability >= 90 && skillProbability < 100)
                    numSkill = 9;
                else
                    numSkill = 10;
                break;
        }

        return skillNames[numSkill];
    }
}