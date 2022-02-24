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
    [SerializeField] [Range(0, 100)] protected int scare;
    public int Scare => scare;

    [SerializeField] protected bool isDeath;
    [SerializeField] protected Sprite[] scareImages;
    [SerializeField] protected Image imageScare;
    [SerializeField] protected ProgressBar scareLife;
    [SerializeField] protected UnityEvent onDefeat;

    [Header("Other Stats")] 
    [SerializeField] [Range(400f, 1000f)] protected float speedMovement;
    protected NavMeshAgent navMeshAgent;

    [Header("States")] 
    [SerializeField] protected States state = States.Idle;

    //TEMP: EL BOSS APRENDIZ TIENE EL ANIMATOR EN EL HIJO (SE CAMBIARA EN EL FUTURO) TEMP: NO HABIA MODELO
    [Header("Anims")]
    [SerializeField] protected Animator _animator;
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

    [SerializeField] bool canRotate = false;
    public bool CanRotate { set => canRotate = value; }

    public NavMeshAgent NavMeshAgent => navMeshAgent;

    protected Transform playerTransf;


    protected virtual void Awake()
    {
        playerTransf = FindObjectOfType<Movement>().transform;
        //COMPLETAMENTE TEMPORAL, EN EL FINAL TENDRIA QUE ESTAR EN EL MISMO SITIO QUE EL SCRIPT
        //_animator = transform.GetChild(0).GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        speedMovement = navMeshAgent.speed;
        OnDamageReceived.AddListener(ReceiveDamage);
    }

    protected virtual void Start()
    {
        StartCoroutine(WaitForSkillCast());
    }

    private void OnEnable()
    {
        ChoiceController contrl = FindObjectOfType<ChoiceController>();
        contrl.answerPool = answ;
        contrl.enableTime = true;
        contrl.enemy = this;
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

        if (state == States.Attacking && canRotate)
        {
            Vector3 position = new Vector3(playerTransf.position.x, transform.position.y, playerTransf.position.z);
            transform.LookAt(position);
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
        scare += damageReceived;
        scareLife.Current = scare;
        if (scare >= 100)
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
                onDefeat?.Invoke();
                break;
        }
    }
}