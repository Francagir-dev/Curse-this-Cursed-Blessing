using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetGeneralVolume(float Gvolume)
    {
        audioMixer.SetFloat("GVolume", Gvolume);
    }

    public void BGVolume(float BGvolume)
    {
        audioMixer.SetFloat("BGVolume", BGvolume);
    }

    public void VFXVolume(float EVolume)
    {
        audioMixer.SetFloat("EVolume", EVolume);
    }
    public void UIVolume(float UIvolume)
    {
        audioMixer.SetFloat("UIVolume", UIvolume);
    }

}
