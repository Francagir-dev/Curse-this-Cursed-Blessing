using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueClip : PlayableAsset
{
    public int keysToAdd;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        // create a new TweenBehaviour
        ScriptPlayable<DialogueBehaviour> playable = ScriptPlayable<DialogueBehaviour>.Create(graph);
        DialogueBehaviour tween = playable.GetBehaviour();

        // set the behaviour's data
        tween.keysToAdd = keysToAdd;
        return playable;
    }
}