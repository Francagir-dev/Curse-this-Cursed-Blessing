using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();

    public void ExploracionTresSalas()
    {
        audioSource.clip = audioClips[0];
    }

    public void ExploracionSotano()
    {
        audioSource.clip = audioClips[4];
    }

    public void BeforeCombat()
    {
        audioSource.clip = audioClips[5];
    }

    public void StatueSpeak()
    {
        audioSource.clip = audioClips[6];
    }

    public void TravelPuerta()
    {
        audioSource.clip = audioClips[1];
    }

    public void TravelBolaMundo()
    {
        audioSource.clip = audioClips[2];
    }

    public void Combat()
    {
        audioSource.clip = audioClips[3];
    }
}
