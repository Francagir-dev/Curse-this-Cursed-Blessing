using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Aprendiz : Enemy
{
    [SerializeField] Transform playerTransform;
    [SerializeField] SkillAprendiz skill;

    private Vector3 _playerPos;

    private enum Stages
    {
        Heavy,
        Medium,
        Light
    }

    [SerializeField] private Stages stage = Stages.Heavy;

    public Aprendiz(bool isCastingSkill, string[] skillNames, int scare, bool isDeath, float speedMovement)
        : base(isCastingSkill, skillNames, scare, isDeath, speedMovement)
    {
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        speedMovement = navMeshAgent.speed;
        StartCoroutine(CastSkill());
    }


    private void Update()
    {
        Movement(navMeshAgent, playerTransform, speedMovement);
        if (canRotate)
            transform.LookAt(new Vector3(_playerPos.x, 3.0f, _playerPos.z));
    }

    #region Movement

    protected override void Movement(NavMeshAgent navigation, Transform pistachitoTransform, float speed)
    {
        if (state != States.Moving) return;
        _playerPos = pistachitoTransform.position;
        navigation.SetDestination(_playerPos);
    }

    protected override void StopAgent()
    {
        navMeshAgent.speed = 0;
        navMeshAgent.isStopped = true;
    }

    #endregion

    #region States

    public override void ChangeState(States stateToChange)
    {
        state = stateToChange;
        switch (state)
        {
            case States.Moving:
                navMeshAgent.speed = speedMovement;
                navMeshAgent.isStopped = false;
                IsCastingSkill = false;
                break;
            case States.Attacking:
                StopAgent();
                break;
            case States.Scared:
                isDeath = true;
                break;
        }
    }

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

    #endregion

    #region Skills

    protected override string RandomizeSkill()
    {
        int skillProbability;
        int numSkill = -1;
        float distance = Vector3.Distance(playerTransform.position, transform.position);
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

    protected override void ThrowSkill(string skillName)
    {
        ChangeNameSkill(skillName);
        ChangeState(States.Attacking);
        skill.enabled = true;
    }

    protected override IEnumerator CastSkill()
    {
        while (state != States.Scared)
        {
            while (isCastingSkill)
                yield return null;
            yield return new WaitForSeconds(coolDown);
            ThrowSkill(RandomizeSkill());
            isCastingSkill = true;
        }
    }


    protected override void ChangeNameSkill(string nameOfSkill)
    {
        skill.SkillName = nameOfSkill;
    }

    #endregion

    #region Life

    public override void ReceiveDamage(int damageReceived)
    {
        scare += damageReceived;
        scareLife.Current = scare;
    }

    #endregion
}