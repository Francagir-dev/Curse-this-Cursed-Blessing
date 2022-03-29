using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PauseBehaviour : PlayableBehaviour
{
    public DialogueManager dialogueManag;
    bool doOnce = false;
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (doOnce) return;

        TimelineManager manag = playerData as TimelineManager;

        manag.Stop();
        dialogueManag.onDialogueEnd += manag.Continue;
        doOnce = true;
    }
}