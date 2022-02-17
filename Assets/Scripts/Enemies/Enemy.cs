using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [Header("Skills")] 
    [SerializeField] protected bool isCastingSkill;
    [SerializeField] protected string[] skillNames;
    [SerializeField] [Range(0, 30)] protected float coolDown;
    [SerializeField] [Range(0f, 50f)] protected float offsetDistanceSkill;

    [Header("Health")] 
    [SerializeField] [Range(0, 100)] protected int scare;

    [SerializeField] protected bool isDeath;
    [SerializeField] protected Sprite[] scareImages;
    [SerializeField] protected Image imageScare;
    [SerializeField] protected ProgressBar scareLife;

    [Header("Other Stats")] 
    [SerializeField] [Range(400f, 1000f)] protected float speedMovement;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected bool canRotate;

    [Header("States")] 
    [SerializeField] protected States state = States.Idle;

    [Header("Anims")] 
    [SerializeField] protected Animator animatorCharacter;
    [SerializeField] protected Animator animatorSkill;

    [Header("Events")] 
    [SerializeField] public UnityEvent<int> OnDamageReceived;
    [SerializeField] private UnityEvent OnCastSkill;


    public enum States
    {
        Idle,
        Moving,
        Attacking,
        Scared
    }
    
    public States State
    {
        get => state;
        set => state = value;
    }

    public bool IsCastingSkill
    {
        set => isCastingSkill = value;
    }

    public NavMeshAgent NavMeshAgent => navMeshAgent;


    public Enemy(bool isCastingSkill, string[] skillNames, int scare, bool isDeath, float speedMovement)
    {
        this.isCastingSkill = isCastingSkill;
        this.skillNames = skillNames;
        this.scare = scare;
        this.isDeath = isDeath;
        this.speedMovement = speedMovement;
    }

    /// <summary>
    /// Movement of enemy through the NavMesh
    /// </summary>
    /// <param name="navigation">Component indicates will move through mesh</param>
    /// <param name="playerTransform">Position where Enemy will move</param>
    /// <param name="speed">Movement Speed (Will be multiplied by Time.deltaTime)</param>
    protected abstract void Movement(NavMeshAgent navigation, Transform pistachitoTransform, float speed);

    /// <summary>
    /// Calculate Skill to throw
    /// </summary>
    /// <returns>Name of Skill to Cast</returns>
    protected abstract string RandomizeSkill();

    /// <summary>
    /// Cast Skill 
    /// </summary>
    /// <param name="skillName">Name of Skill to cast</param>
    protected abstract void ThrowSkill(string skillName);

    /// <summary>
    /// Will sum damage to boss
    /// </summary>
    /// <param name="damageReceived">Damage received from character (Unity Event)</param>
    public abstract void ReceiveDamage(int damageReceived);

    /// <summary>
    /// Coroutine To Throw skill
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator CastSkill();

    /// <summary>
    /// Change "armor"=> Phase of boss
    /// </summary>
    protected abstract void ChangeStates();

    /// <summary>
    /// Stops Movement and rotation
    /// </summary>
    protected abstract void StopAgent();

    public abstract void ChangeState(States stateToChange);
    protected abstract void ChangeNameSkill(string nameOfSkill);
}