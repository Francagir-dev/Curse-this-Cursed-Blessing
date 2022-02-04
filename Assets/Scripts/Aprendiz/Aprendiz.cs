using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Aprendiz : Enemy, EnemyInterface
{
    [SerializeField] Transform playerTransform;
    [SerializeField] NavMeshAgent _navMeshAgent;
    [SerializeField] SkillAprendiz skill;

    private enum Stages
    {
        Heavy,
        Medium,
        Light
    }

    [SerializeField] private Stages stage = Stages.Heavy;

    public Aprendiz(bool isCastingSkill, string[] skillNames, int scare, bool isDeath, string[] phasesName,
        float speedMovement, Animator animatorCharacter, Animator animatorSkill) : base(isCastingSkill, skillNames,
        scare, isDeath, phasesName,
        speedMovement, animatorCharacter, animatorSkill)
    {
    }

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        StartCoroutine(CastSkill());
    }

    // Update is called once per frame
    void Update()
    {
        Movement(_navMeshAgent, playerTransform, speedMovement);
    }

    public void Movement(NavMeshAgent navigation, Transform playerTransform, float speed)
    {
        if (!isCastingSkill)
        {
            state = States.Moving;
            navigation.speed = speed * Time.deltaTime;
        }
        else
        {
            state = States.Attacking;
            navigation.speed = 0;
        }

        navigation.destination = playerTransform.position - offsetPlayer;
    }

    public bool IsDeath(int health)
    {
        if (health >= 100)
        {
            state = States.Scared;
            return true;
        }
        else
        {
            return false;
        }
    }

    public string RandomizeSkill()
    {
        int skillProbability = 0;
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

    public void ThrowSkill(string skillName)
    {
        ChangeNameSkill(skillName);
        skill.enabled = true;
    }

    public IEnumerator CastSkill()
    {
        while (!IsDeath(scare) && !isCastingSkill)
        {
            ThrowSkill(RandomizeSkill());
            yield return new WaitForSeconds(coolDown);
        }
    }

    public void ChangeStates()
    {
        if (scare > 33 && scare < 67)
        {
            stage = Stages.Medium;
        }
        else if (scare > 67)
        {
            stage = Stages.Light;
        }
    }

    public void ReceiveDamage(int damageReceived)
    {
        scare += damageReceived;
        scareLife.Current = scare;
        ChangeStates();

        if (IsDeath(scare))
        {
            return;
        }
        else
        {
            Debug.Log("Sigue vivo");
        }
    }

    void ChangeNameSkill(string name)
    {
        skill.SkillName = name;
    }
}