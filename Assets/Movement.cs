using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour, PlayerInput.IPlayerActions
{
    PlayerInput playerInput;
    Vector3 inputMove;
    Vector3 actualMove;

    public float speed = 1;
    public float acceleration = .5f;
    Rigidbody rig;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Player.SetCallbacks(this);
        playerInput.Enable();
        rig = GetComponent<Rigidbody>();
    }

    //Provisional, poner en el script principal del prota
    public void OnDash(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        inputMove = new Vector3(input.x, rig.velocity.y, input.y);
    }

    private void Update()
    {
        rig.velocity = Vector3.Lerp(actualMove, inputMove, acceleration);
    }

    #region NotImplemented
    public void OnCameraMov(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
    public void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
    public void OnOpenDialog(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnPause(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSelectOption(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
