using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CutsceneManager : MonoBehaviour, PlayerInput.IUIControlsActions
{
    public PlayerInput playerInput;
    [SerializeField] string dialogueTableName;

    [Header("Only if Dialogue Only")]
    [SerializeField] bool dialogueOnly = false;
    [SerializeField] int beginAtKey = 0;
    [SerializeField] int endAtKey = 99;

    PlayableDirector direct;
    public UnityEvent onCutsceneEnd;
    public UnityEvent onCutsceneStart;

    private DialogueManager manag;
   
    private void Awake()
    {
        manag = FindObjectOfType<DialogueManager>();
        direct = GetComponent<PlayableDirector>();
        Debug.Log(gameObject.name);
    }

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.UIControls.SetCallbacks(this);
        playerInput.Enable();

        onCutsceneStart.Invoke();
        manag.TableName = dialogueTableName;

        if (!manag.AutomaticText)
        {
            Movement.Instance.playerInput.Player.Disable();
            Movement.Instance.playerInput.UIControls.Enable();
            Movement.Instance.playerInput.UIControls.Skip.performed += ctx => manag.SkipText();
        }

        if (dialogueOnly)
        {
            manag.Skip(beginAtKey);
            manag.showToKey = endAtKey;
            manag.Open();

            manag.onDialogueEnd += delegate
            {
                Movement.Instance.playerInput.Player.Enable();
                Movement.Instance.playerInput.UIControls.Disable();
                Transition.Instance.Do(onCutsceneEnd.Invoke);
            };

            return;
        }


        direct.stopped += delegate
        {
            if (!manag.AutomaticText)
            {
                Movement.Instance.playerInput.Player.Enable();
                Movement.Instance.playerInput.UIControls.Disable();
            }
            Transition.Instance.Do(onCutsceneEnd.Invoke);
        };
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    public void Skip()
    {
        Stop();

        manag.Close();

        if (!manag.AutomaticText)
        {
            Movement.Instance.playerInput.Player.Enable();
            Movement.Instance.playerInput.UIControls.Disable();
        }

        Transition.Instance.Do(onCutsceneEnd.Invoke);
    }

    public void Stop()
    {
        if (direct == null || direct.state == PlayState.Paused) return;
        direct.Pause();
    }

    public void Continue()
    {
        if (direct == null || direct.state != PlayState.Paused) return;
        direct.Resume();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Skip();
    }

    #region MUDAMUDAMUDA
    public void OnNavigate(InputAction.CallbackContext context)
    {
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
    }

    public void OnPressAnyKey(InputAction.CallbackContext context)
    {
    }

    public void OnSkip(InputAction.CallbackContext context)
    {
    }
    #endregion
}