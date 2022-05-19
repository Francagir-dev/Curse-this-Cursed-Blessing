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