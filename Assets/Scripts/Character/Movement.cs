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
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (dashing || dashCooldown > 0 || context.phase != InputActionPhase.Started) return;

        inputMove = transform.forward;

        moveDirection = Quaternion.LookRotation(inputMove);
        speed *= dashSpeedMult;

        dashActualDuration = dashDurat;
        dashing = true;

    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (dashing) return;

        var input = context.ReadValue<Vector2>();

        if (input.sqrMagnitude < .025f) input = Vector2.zero;

        inputMove = new Vector3(input.x, rig.velocity.y, input.y);

        //No puede sacar una rotación si el movimiento es zero
        if (inputMove != Vector3.zero) 
            moveDirection = Quaternion.LookRotation(inputMove);
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

        rig.velocity = Vector3.Lerp(rig.velocity, inputMove * speed, acceleration);
        if (inputMove.sqrMagnitude > .03f)
            transform.rotation = Quaternion.Slerp(transform.rotation, moveDirection, rotTime);

        if (dashing && dashActualDuration <= 0)
        {
            speed = originalSpeed;
            dashing = false;
            dashCooldown = origDashCooldown;
            //Para evitar un deslize si esta quieto
            inputMove = Vector2.zero;
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

    public void OnPause(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnSelectOption(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }
    #endregion
}
