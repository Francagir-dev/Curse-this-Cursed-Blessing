using System;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Skills")]
    [SerializeField] protected bool isCastingSkill;
    [SerializeField] protected string[] skillNames;
   
    [Header("Health")]
    [SerializeField][Range(0,100)] protected int scare;
    [SerializeField] protected bool isDeath;
    protected Color [] scareColors;
    [SerializeField] protected ProgressBar scareLife;
    
    [Header("Phases")]
    [SerializeField][Range(3,5)] protected int phases;
    [SerializeField] protected string [] phasesName;
   
    [Header("Other Stats")]
    [SerializeField][Range(0f,2000f)] protected float speedMovement;

    [Header("States")]
    [SerializeField] protected States state = States.Idle;

    [Header("Anims")] 
    [SerializeField] protected Animator _animator;

    private int skillsCasted;

    public int SkillCasted
    {
        get => skillsCasted;
        set => skillsCasted = value;

    }

    public string name;
  
    protected enum States
    {
        Idle,
        Moving,
        Attacking,
        Resting,
        Scared
    }


    public Enemy(bool isCastingSkill, string[] skillNames, int scare, bool isDeath, int phases, string[] phasesName, float speedMovement,Animator _animator)
    {
        this.isCastingSkill = isCastingSkill;
        this.skillNames = skillNames;
        this.scare = scare;
        this.isDeath = isDeath;
        this.phases = phases;
        this.phasesName = phasesName;
        this.speedMovement = speedMovement;
    }

 
}