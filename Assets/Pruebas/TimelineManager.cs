using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    PlayableDirector direct;

    private void Awake()
    {
        direct = GetComponent<PlayableDirector>();
    }

    public void Stop()
    {
        if (direct.state == PlayState.Paused) return;
        direct.Pause();
    }

    public void Continue()
    {
        if (direct.state != PlayState.Paused) return;
        direct.Resume();
    } 
}