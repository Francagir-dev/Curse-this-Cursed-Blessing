using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimateClip : PlayableAsset
{
    public string animName;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        // create a new TweenBehaviour
        ScriptPlayable<AnimateBehaviour> playable = ScriptPlayable<AnimateBehaviour>.Create(graph);
        AnimateBehaviour tween = playable.GetBehaviour();

        // set the behaviour's data
        tween.animName = animName;
        return playable;
    }
}