using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour, PlayerInput.IPlayerActions
{
    public GameObject pauseMenu;
    static Movement instance;

    public static Movement Instance
    {
        get => instance;
        private set => instance = value;
    }

    MainCharacterLife lifeSystem;

    public MainCharacterLife LifeSystem
    {
        get => lifeSystem;
        private set => lifeSystem = value;
    }

    public bool isPaused;

    //Player Input Related
  public PlayerInput playerInput;
    Vector3 inputMove;
    Vector3 dashDir;
    Quaternion moveDirection;

    [Header("Movement")] public float speed = 1;
    public float acceleration = .5f;
    public float rotTime = .2f;

    [Header("Dash")] public float dashSpeedMult = 1.5f;
    public float dashDurat = 1;
    public float dashCooldown = .3f;

    //Private Variables
    bool dashing = false;
    float originalSpeed;
    float origDashCooldown;
    Rigidbody rig;
    float dashActualDuration;
    Transform interactionDisplayer;
    Coroutine interactionCoroutine;

    //TEMPORAL
    ChoiceController choice;
    [HideInInspector] public ChoiceController_Riddle riddle;
    Interactable[] interactables;

    private void Awake()
    {
        instance = this;

        interactionDisplayer = transform.Find("--Interactable--");
        DisplayInteraction(false);

        LifeSystem = GetComponent<MainCharacterLife>();
        originalSpeed = speed;
        origDashCooldown = dashCooldown;
        dashCooldown = 0;

        Application.targetFrameRate = 60;
        playerInput = new PlayerInput();
        playerInput.Player.SetCallbacks(this);
        playerInput.Enable();
        rig = GetComponent<Rigidbody>();

        //TEMPORAL
        choice = FindObjectOfType<ChoiceController>();
    }

    private void Start()
    {
        interactables = FindObjectsOfType<Interactable>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();

        if (input.sqrMagnitude < .025f) input = Vector2.zero;

        inputMove = new Vector3(input.x, rig.velocity.y, input.y);

        //No puede sacar una rotación si el movimiento es zero
        if (inputMove != Vector3.zero)
            moveDirection = Quaternion.LookRotation(inputMove);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (dashing || dashCooldown > 0 || context.phase != InputActionPhase.Started) return;

        LifeSystem.Inv = true;
        transform.rotation = moveDirection;
        dashDir = transform.forward;

        moveDirection = Quaternion.LookRotation(inputMove);
        speed *= dashSpeedMult;

        dashActualDuration = dashDurat;
        dashing = true;
    }

    public void OnSelectOption(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        int trueAnsw = -1;
        Vector2 answ = context.ReadValue<Vector2>();

        if (answ.x != 0)
            trueAnsw = answ.x == 1 ? 1 : 3;
        else if (answ.y != 0)
            trueAnsw = answ.y == 1 ? 0 : 2;

        if (trueAnsw == -1) return;

        choice.ChooseOption(trueAnsw);
        if (riddle != null)
            riddle.ChooseOption(trueAnsw);
    }

    public void DisplayInteraction(bool display)
    {
        float maxTime = .3f;
        int multiplier = display ? 1 : -1;
        float time = display ? 0 : maxTime;
        if (interactionCoroutine != null)
            StopCoroutine(interactionCoroutine);
        interactionCoroutine = StartCoroutine(Displayer());

        IEnumerator Displayer()
        {
            while(display ? time < maxTime : time > 0)
            {
                time += Time.deltaTime * multiplier;
                interactionDisplayer.localScale = Vector3.one *
                    Mathf.Lerp(0 , 1, time/maxTime);
                yield return null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (dashActualDuration > 0)
            dashActualDuration -= Time.deltaTime;
        else if (dashCooldown > 0)
            dashCooldown -= Time.deltaTime;

        rig.velocity = Vector3.Lerp(rig.velocity, (dashing ? dashDir : inputMove) * speed, acceleration);
        if (!dashing && inputMove.sqrMagnitude > .03f)
            transform.rotation = Quaternion.Slerp(transform.rotation, moveDirection, rotTime);

        if (dashing && dashActualDuration <= 0)
        {
            speed = originalSpeed;
            dashing = false;
            LifeSystem.Inv = false;
            dashCooldown = origDashCooldown;
            //Para evitar un deslize si esta quieto
            rig.velocity = Vector3.zero;
        }
    }

    //Salen mensajes de error si no haces esto
    //Al parecer el input sigue accediendo al script aunque termine el juego
    private void OnDestroy()
    {
        playerInput.Disable();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        foreach (var item in interactables)
        {
            if (item.PlayerDetected && item.isActiveAndEnabled)
            {
                item.onTrigger.Invoke();
                if (!item.isActiveAndEnabled)
                {
                    DisplayInteraction(false);
                    item.PlayerDetected = false;
                }
                break;
            }
        }
    }

    #region NotImplemented

    public void OnCameraMov(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnOpenDialog(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started && !isPaused)
        {
            Instantiate(pauseMenu, pauseMenu.transform.position, pauseMenu.transform.rotation);
            isPaused = true;
        }
/*
        if (isPaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }else{
        pauseMenu.SetActive(true);
        isPaused = true;
        }
            */
    }

    #endregion
}