using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipDialogueBehaviour : PlayableBehaviour
{
    public string condition;
    public KeyValuePair<int, int>[] skipToKey;

    bool doOnce = false;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!Application.isPlaying || doOnce) return;

        DialogueManager manag = playerData as DialogueManager;
        int skip;
        try
        {
            skip = skipToKey[0].Value;
        }
        catch (Exception e)
        {
            skip = 0;
        }

        if (condition != "")
        {
            int value = GameObject.Find(condition).GetComponent<Conditional>().Value;
            for (int i = 0; i < skipToKey.Length; i++)
            {
                if (skipToKey[i].Key == value)
                    skip = skipToKey[i].Value;
            }
        }

        manag.Skip(skip);
        doOnce = true;
    }
}