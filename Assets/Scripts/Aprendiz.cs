using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Aprendiz : Enemy, EnemyInterface
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private NavMeshAgent _navMeshAgent;


    private enum Stages
    {
        Heavy,
        Medium,
        Light
    }

    [SerializeField] private Stages stage = Stages.Heavy;

    public Aprendiz(bool isCastingSkill, string[] skillNames, int scare, bool isDeath, int phases,
        string[] phasesName, float speedMovement, Animator _animator) :
        base(isCastingSkill, skillNames, scare, isDeath, phases, phasesName, speedMovement, _animator)
    {
    }

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
       // _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement(_navMeshAgent, playerTransform, speedMovement);
        //  ReceiveDamage(1);
        CastingSkill(skillNames[0]);
    }


    public void Movement(NavMeshAgent navigation, Transform playerTransform, float speed)
    {
        navigation.speed = speed * Time.deltaTime;
        navigation.destination = playerTransform.position;
    }

    public bool IsDeath(int health)
    {
        if (health >= 100)
        {
            state = States.Scared;
            Debug.Log("Lo has asustao");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CastingSkill(string skillName)
    {
        switch (skillName)
        {
            case "HachaSuelo":
                _animator.SetBool("HachaSuelo", true);
                break;
        }
    }

    public void ReceiveDamage(int damageReceived)
    {
        scare += damageReceived;
        scareLife.Current = scare;
        IsDeath(scare);
    }
}