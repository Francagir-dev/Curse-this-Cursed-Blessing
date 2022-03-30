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

    private void Awake()
    {
        DialogueManager manag = FindObjectOfType<DialogueManager>();
        manag.TableName = dialogueTableName;

        if (dialogueOnly) 
        {
            manag.onDialogueEnd += () => Transition.Instance.Do(onCutsceneEnd.Invoke);
            return;
        }

        direct = GetComponent<PlayableDirector>();
        direct.stopped += (PlayableDirector a) => Transition.Instance.Do(onCutsceneEnd.Invoke);
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