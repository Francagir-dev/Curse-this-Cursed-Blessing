using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] string dialogueTableName;

    [Header("Only if Dialogue Only")]
    [SerializeField] bool dialogueOnly = false;
    [SerializeField] int beginAtKey = 0;
    [SerializeField] int endAtKey = 99;

    PlayableDirector direct;
    public UnityEvent onCutsceneEnd;

    private PlayerInput _player;
   
    private void Awake()
    {
        DialogueManager manag = FindObjectOfType<DialogueManager>();
      //  manag.TableName = dialogueTableName;

        if (dialogueOnly) 
        {
            manag.onDialogueEnd += () => Transition.Instance.Do(onCutsceneEnd.Invoke);
            manag.Skip(beginAtKey);
            manag.showToKey = endAtKey;
            manag.Open();
            return;
        }

        direct = GetComponent<PlayableDirector>();
        
        Movement.Instance.playerInput.Player.Disable();
        Movement.Instance.playerInput.UIControls.Enable();
        Movement.Instance.playerInput.UIControls.Skip.performed += ctx => manag.SkipText();
        _player = new PlayerInput();

        direct.stopped += delegate
        {
            Movement.Instance.playerInput.Player.Enable();
            Movement.Instance.playerInput.UIControls.Disable();
            Transition.Instance.Do(onCutsceneEnd.Invoke);
        };
    }

    public void Stop()
    {
        if (direct.state == PlayState.Paused) return;
        direct.Pause();
    }

    public void Continue()
    {
        if (direct.state != PlayState.Paused) return;
        direct.Resume();
    } 
}