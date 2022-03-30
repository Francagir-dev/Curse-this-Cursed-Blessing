using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipDialogueClip : PlayableAsset
{
    public int skipToKey;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        // create a new TweenBehaviour
        ScriptPlayable<SkipDialogueBehaviour> playable = ScriptPlayable<SkipDialogueBehaviour>.Create(graph);
        SkipDialogueBehaviour tween = playable.GetBehaviour();

        // set the behaviour's data
        tween.skipToKey = skipToKey;
        return playable;
    }
}