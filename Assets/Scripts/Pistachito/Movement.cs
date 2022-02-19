using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour, PlayerInput.IPlayerActions
{
    //Player Input Related
    PlayerInput playerInput;
    Vector3 inputMove;
    Vector3 dashDir;
    Quaternion moveDirection;
    
    [Header("Movement")]
    public float speed = 1;
    public float acceleration = .5f;
    public float rotTime = .2f;

    [Header("Dash")]
    public float dashSpeedMult = 1.5f;
    public float dashDurat = 1;
    public float dashCooldown = .3f;

    //Private Variables
    bool dashing = false;
    float originalSpeed;
    float origDashCooldown;
    Rigidbody rig;
    float dashActualDuration;

    //TEMPORAL
    ChoiceController choice;

    private void Awake()
    {
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

    public void OnDash(InputAction.CallbackContext context)
    {
        if (dashing || dashCooldown > 0 || context.phase != InputActionPhase.Started) return;

        dashDir = transform.forward;

        moveDirection = Quaternion.LookRotation(inputMove);
        speed *= dashSpeedMult;

        dashActualDuration = dashDurat;
        dashing = true;
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
    }

    private void Update()
    {
        if (dashActualDuration > 0)
        {
            dashActualDuration -= Time.deltaTime;
        }
        else if (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }

        rig.velocity = Vector3.Lerp(rig.velocity, (dashing ? dashDir : inputMove) * speed, acceleration);
        if (!dashing && inputMove.sqrMagnitude > .03f)
            transform.rotation = Quaternion.Slerp(transform.rotation, moveDirection, rotTime);

        if (dashing && dashActualDuration <= 0)
        {
            speed = originalSpeed;
            dashing = false;
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

    #region NotImplemented
    public void OnCameraMov(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }
    public void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }
    public void OnOpenDialog(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
    #endregion
}