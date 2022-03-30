using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueBehaviour : PlayableBehaviour
{
    public int keysToAdd;
    bool doOnce = false;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!Application.isPlaying) return;

        if (doOnce) return;
        DialogueManager manag = playerData as DialogueManager;
        manag.showToKey += keysToAdd;
        manag.Open();
        doOnce = true;
    }
}