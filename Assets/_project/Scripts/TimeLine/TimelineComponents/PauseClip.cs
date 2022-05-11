using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PauseClip : PlayableAsset
{
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        // create a new TweenBehaviour
        ScriptPlayable<PauseBehaviour> playable = ScriptPlayable<PauseBehaviour>.Create(graph);
        PauseBehaviour tween = playable.GetBehaviour();

        tween.dialogueManag = FindObjectOfType<DialogueManager>();
        return playable;
    }
}