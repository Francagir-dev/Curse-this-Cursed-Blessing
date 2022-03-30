using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipDialogueBehaviour : PlayableBehaviour
{
    public int skipToKey;
    bool doOnce = false;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!Application.isPlaying) return;

        if (doOnce) return;
        DialogueManager manag = playerData as DialogueManager;
        manag.Skip(skipToKey);
        doOnce = true;
    }
}