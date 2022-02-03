using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Aprendiz : Enemy, EnemyInterface
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    public FloorAxe floorAxe;

    private enum Stages
    {
        Heavy,
        Medium,
        Light
    }

    [SerializeField] private Stages stage = Stages.Heavy;

    public Aprendiz(bool isCastingSkill, string[] skillNames, int scare, bool isDeath, int phases, string[] phasesName, float speedMovement, Animator _animator) : 
        base(isCastingSkill, skillNames, scare, isDeath, phases, phasesName, speedMovement, _animator)
    {
    }

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
      
        navigation.speed = speed * Time.deltaTime;
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

        switch (stage)
        {
            case Stages.Heavy:
                skillProbability = Random.Range(0, 3);
                break;
            case Stages.Medium:
                skillProbability = Random.Range(3, 6);
                break;
            case Stages.Light:
                skillProbability = Random.Range(6, 11);
                break;
        }

        return skillNames[skillProbability];
    }

    public void ThrowSkill(string skillName)
    {
        switch (skillName)
        {
            case "FloorAxe":
                GetComponent<FloorAxe>().enabled = true;
                break;
            case "AxeThrow":
                 GetComponent<AxeThrow>().enabled = true;
                break;
            case "FireAxe":
               // GetComponent<FireAxe>().enabled = true;
                break;               
            case "Shoot":
               // GetComponent<Shoot>().enabled = true;
                break;
            case "JumpArea":
                // GetComponent<JumpArea>().enabled = true;
                break;
            case "FloorAxeFast":
                // GetComponent<FloorAxeFast>().enabled = true;
                break;
            case "ShootLast":
                // GetComponent<ShootLast>().enabled = true;
                break;
            case "AxeThrowReThrow":
                // GetComponent<AxeThrowReThrow>().enabled = true;
                break;
            case "JumpChangeDirection":
                // GetComponent<JumpChangeDirection>().enabled = true;
                break;
            case "ChargePinball":
                // GetComponent<ChargePinball>().enabled = true;
                break;
        }
    }

    public IEnumerator CastSkill()
    {
        while (!IsDeath(scare)& !isCastingSkill)
        {
            yield return new WaitForSeconds(coolDown);
            ThrowSkill(RandomizeSkill());
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
}