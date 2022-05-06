using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{ 
    [SerializeField] Answers answ;
    [Header("Skills")] 
    [SerializeField] protected bool isCastingSkill;
    [SerializeField] protected string[] skillNames;
    [SerializeField] [Range(0, 30)] protected float coolDown;
    [SerializeField] [Range(0f, 50f)] protected float offsetDistanceSkill;

    [Header("Health")]
    [SerializeField] protected int actualScare;
    [SerializeField] protected float maxScare;
    public int ActualScare => actualScare;
   
    protected bool isDeath;
   
    [SerializeField] protected Sprite[] scareImages;
    [SerializeField] protected Image imageScare;
    [SerializeField] protected UnityEvent onDefeat;

    [Header("Other Stats")] 
    [SerializeField] [Range(400f, 1000f)] protected float speedMovement;
    [SerializeField] [Range(0f, 1f)] protected float rotationSpeed;


    [Header("States")] 
    [SerializeField] protected States state = States.Idle;

    protected NavMeshAgent navMeshAgent;
    protected ProgressBar scareLifeHUD;
    [SerializeField]
    protected Animator _animator;
    public Animator Animator
    {
        get => _animator;
    }

    [SerializeField] protected string skillName;
    public string SkillName
    {
        set => skillName = value;
    }

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

    public bool IsCastingSkill { set => isCastingSkill = value; }

    //Only For Debug, remove serialize later
    [SerializeField] bool canRotate = false;
    public bool CanRotate { set => canRotate = value; }

    public NavMeshAgent NavMeshAgent => navMeshAgent;

    protected Transform playerTransf;
    
    protected virtual void Awake()
    {
        playerTransf = FindObjectOfType<Movement>().transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        speedMovement = navMeshAgent.speed;
        OnDamageReceived.AddListener(ReceiveDamage);
        scareLifeHUD = transform.Find("--ScareLife--").GetChild(0).GetComponent<ProgressBar>();
    }

    protected virtual void Start()
    {
        StartCoroutine(WaitForSkillCast());
        ChoiceController contrl = FindObjectOfType<ChoiceController>();
        contrl.EnableChoices();
    }

    private void OnEnable()
    {
        ChoiceController contrl = FindObjectOfType<ChoiceController>();
        contrl.answerPool = answ;
        contrl.enableTime = true;
        contrl.enemy = this;
        scareLifeHUD.Maximum = Mathf.RoundToInt(maxScare);
    }


    /// <summary>
    /// Cast Skill 
    /// </summary>
    /// <param name="skillName">Name of Skill to cast</param>
    protected void ThrowSkill(string skillName)
    {
        SkillName = skillName;
        _animator.SetTrigger(skillName);
        ChangeState(States.Attacking);
    }

    public void SetRot(int enable)
    {
        CanRotate = enable != 0;
    }

    protected virtual void Update()
    {
        Movement(navMeshAgent, playerTransf, speedMovement);
        _animator.SetFloat("Speed", Mathf.Lerp(0, 1, navMeshAgent.velocity.magnitude/3.5f));

        if (state == States.Attacking && canRotate)
        {
            Vector3 position = new Vector3(playerTransf.position.x, transform.position.y, playerTransf.position.z);
            Quaternion finalRot = Quaternion.LookRotation(position - transform.position, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, finalRot, rotationSpeed);
        }
    }

    #region Movement

    /// <summary>
    /// Movement of enemy through the NavMesh
    /// </summary>
    /// <param name="navigation">Component indicates will move through mesh</param>
    /// <param name="playerTransform">Position where Enemy will move</param>
    /// <param name="speed">Movement Speed (Will be multiplied by Time.deltaTime)</param>
    protected virtual void Movement(NavMeshAgent navigation, Transform pistachitoTransform, float speed)
    {
        AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);

        if (isCastingSkill)
        {
            if (info.IsName("Idle"))
            {
                isCastingSkill = false;
                ChangeState(States.Moving);
            }
            return;
        }

        if (!info.IsName("Idle")) 
        {
            isCastingSkill = true;
            return; 
        }

        playerTransf.position = pistachitoTransform.position;
        navigation.SetDestination(playerTransf.position);
    }
    #endregion

    /// <summary>
    /// Calculate Skill to throw
    /// </summary>
    /// <returns>Name of Skill to Cast</returns>
    protected abstract string RandomizeSkill();

    /// <summary>
    /// Will sum damage to boss
    /// </summary>
    /// <param name="damageReceived">Damage received from character (Unity Event)</param>
    public void ReceiveDamage(int damageReceived)
    {
        actualScare += damageReceived;
        scareLifeHUD.Current = actualScare;
        if (actualScare >= maxScare)
            ChangeState(States.Scared);
    }

    /// <summary>
    /// Coroutine To Throw skill
    /// </summary>
    /// <returns></returns>
    protected IEnumerator WaitForSkillCast()
    {
        while (state != States.Scared)
        {
            while (state == States.Attacking)
                yield return null;
            yield return new WaitForSeconds(coolDown);
            ThrowSkill(RandomizeSkill());
            //Hace falta un tiempo minimo de espera,
            //no se me ocurre como hacerlo m�s limpito
            //yield return new WaitForSeconds(.5f);
        }
    }

    /// <summary>
    /// Change "armor"=> Phase of boss
    /// </summary>
    public abstract void ChangeStates();

    /// <summary>
    /// Stops Movement and rotation
    /// </summary>
    protected void StopAgent()
    {
        navMeshAgent.speed = 0;
        navMeshAgent.isStopped = true;
    }

    public void ChangeState(States stateToChange)
    {
        state = stateToChange;
        switch (state)
        {
            case States.Moving:
                navMeshAgent.speed = speedMovement;
                navMeshAgent.isStopped = false;
                break;
            case States.Attacking:
                StopAgent();
                break;
            case States.Scared:
                isDeath = true;
                _animator.SetTrigger("Scared");
                Transition.Instance.Do(() => onDefeat.Invoke());
                break;
        }
    }
}