using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LookAtClip : PlayableAsset
{
    public ExposedReference<Transform> lookAt;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        // create a new TweenBehaviour
        ScriptPlayable<LookAtBehaviour> playable = ScriptPlayable<LookAtBehaviour>.Create(graph);
        LookAtBehaviour tween = playable.GetBehaviour();

        // set the behaviour's data
        tween.target = lookAt.Resolve(graph.GetResolver());
        return playable;
    }
}