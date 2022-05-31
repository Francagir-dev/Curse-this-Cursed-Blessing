using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipDialogueClip : PlayableAsset
{
    public string conditional;
    [System.Serializable]
    class SkippableKeys
    {
        public int conditionValue;
        public int skipTo;
    }

    [SerializeField] SkippableKeys[] keys;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        // create a new TweenBehaviour
        ScriptPlayable<SkipDialogueBehaviour> playable = ScriptPlayable<SkipDialogueBehaviour>.Create(graph);
        SkipDialogueBehaviour tween = playable.GetBehaviour();

        var pairs = new List<KeyValuePair<int, int>>();
        for (int i = 0; i < keys.Length; i++)
            pairs.Add(new KeyValuePair<int, int>(keys[i].conditionValue, keys[i].skipTo));

        // set the behaviour's data
        tween.skipToKey = pairs.ToArray();
        tween.condition = conditional;
        return playable;
    }
}