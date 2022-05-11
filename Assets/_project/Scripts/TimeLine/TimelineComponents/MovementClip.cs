using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MovementClip : PlayableAsset
{
    public ExposedReference<Transform> goTo;
    public ExposedReference<Transform> orig;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        // create a new TweenBehaviour
        ScriptPlayable<MovementBehaviour> playable = ScriptPlayable<MovementBehaviour>.Create(graph);
        MovementBehaviour tween = playable.GetBehaviour();

        // set the behaviour's data
        tween.goTo = goTo.Resolve(graph.GetResolver());
        tween.origPos = orig.Resolve(graph.GetResolver());

        return playable;
    }
}