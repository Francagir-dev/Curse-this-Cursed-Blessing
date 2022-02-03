using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Skills")] [SerializeField] protected bool isCastingSkill;

    [SerializeField] protected string[] skillNames;
    [SerializeField] [Range(0, 30)] protected float coolDown;

    [Header("Health")] [SerializeField] [Range(0, 100)]
    protected int scare;

    [SerializeField] protected bool isDeath;
    [SerializeField] protected Sprite[] scareImages;
    [SerializeField] protected Image imageScare;
    protected Color[] scareColors;
    [SerializeField] protected ProgressBar scareLife;

    [Header("Phases")] [SerializeField] [Range(3, 5)]
    protected int phases;

    [SerializeField] protected string[] phasesName;

    [Header("Other Stats")] [SerializeField] [Range(0f, 2000f)]
    protected float speedMovement;


    [SerializeField] protected Vector3 offsetPlayer;
    [Header("States")] [SerializeField] protected States state = States.Idle;

    [Header("Anims")] [SerializeField] protected Animator _animator;

    protected enum States
    {
        Idle,
        Moving,
        Attacking,
        Resting,
        Scared
    }

    public float SpeedMovement
    {
        get => speedMovement;
        set => speedMovement = value;
    }

    public Enemy(bool isCastingSkill, string[] skillNames, int scare, bool isDeath, int phases, string[] phasesName,
        float speedMovement, Animator animator)
    {
        this.isCastingSkill = isCastingSkill;
        this.skillNames = skillNames;
        this.scare = scare;
        this.isDeath = isDeath;
        this.phases = phases;
        this.phasesName = phasesName;
        this.speedMovement = speedMovement;
    }

    #region Getters

    public bool IsCastingSkill
    {
        get => isCastingSkill;
        set => isCastingSkill = value;
    }

    #endregion
}