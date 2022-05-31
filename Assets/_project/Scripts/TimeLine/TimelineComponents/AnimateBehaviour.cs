using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimateBehaviour : PlayableBehaviour
{
    public string animName;
    bool doOnce = false;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!Application.isPlaying) return;

        if (doOnce) return;
        Animator manag = playerData as Animator;

        manag.Play(animName);

        doOnce = true;
    }
}