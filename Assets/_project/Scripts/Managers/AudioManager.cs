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
        audioSource.Stop();
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    public void TravelPuerta()
    {
        audioSource.Stop();
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    public void TravelBolaMundo()
    {
        audioSource.Stop();
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }

    public void Combat()
    {
        audioSource.Stop();
        audioSource.clip = audioClips[3];
        audioSource.Play();
    }

    public void ExploracionSotano()
    {
        audioSource.Stop();
        audioSource.clip = audioClips[4];
        audioSource.Play();
    }

    public void BeforeCombat()
    {
        audioSource.Stop();
        audioSource.clip = audioClips[5];
        audioSource.Play();
    }

    public void StatueSpeak()
    {
        audioSource.Stop();
        audioSource.clip = audioClips[6];
        audioSource.Play();
    }

    public void OnDamage()
    {
        audioSource.Stop();
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }
}