using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Skills")]
    [SerializeField] protected bool isCastingSkill;
    [SerializeField] protected string[] skillNames;
    [SerializeField] [Range(0, 30)]protected float coolDown;
    [SerializeField] [Range(0f, 50f)] protected float offsetDistanceSkill;

    [Header("Health")]
    [SerializeField][Range(0,100)] protected int scare;
    [SerializeField] protected bool isDeath;
    [SerializeField] protected Sprite[] scareImages;
    [SerializeField] protected Image imageScare; 
    [SerializeField] protected ProgressBar scareLife;
    
    [Header("Phases")]
    [SerializeField] protected string [] phasesName;
    
    [Header("Other Stats")]
    [SerializeField][Range(0f,2000f)] protected float speedMovement;
    [SerializeField]  protected Vector3 offsetPlayer;
  
    [Header("States")]
    [SerializeField] protected States state = States.Idle;

    [Header("Anims")] 
    [SerializeField] protected Animator animatorCharacter;
    [SerializeField] protected Animator animatorSkill;

    public enum States
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

    public States State
    {
        get => state;
        set => state = value;
    }

    public Enemy(bool isCastingSkill, string[] skillNames, int scare, bool isDeath, string[] phasesName, float speedMovement,Animator animatorCharacter, Animator skillaAim)
    {
        this.isCastingSkill = isCastingSkill;
        this.skillNames = skillNames;
        this.scare = scare;
        this.isDeath = isDeath;
        this.phasesName = phasesName;
        this.speedMovement = speedMovement;
    }
}