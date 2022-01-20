using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

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
    public Aprendiz(bool isCastingSkill, string[] skillNames, int scare, bool isDeath, int phases, string[] phasesName, float speedMovement, Animator _animator) : base(isCastingSkill, skillNames, scare, isDeath, phases, phasesName, speedMovement, _animator)
    { }
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
        //  ReceiveDamage(1);
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
                ;
                break;
            case "AxeThrow":
                ;
                break;
            case "FireAxe":
                ;
                break;
            case "Shoot":
                ;
                break;
            case "JumpArea":
                ;
                break;
            case "FloorAxeFast":
                ;
                break;
            case "ShootLast":
                ;
                break;
            case "AxeThrowReThrow":
                ;
                break;
            case "JumpChangeDirection":
                ;
                break;
            case "ChargePinball":
                ;
                break;
        }

        Debug.Log(skillName);
    }
    public IEnumerator CastSkill()
    {
        while (!IsDeath(scare))
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
}