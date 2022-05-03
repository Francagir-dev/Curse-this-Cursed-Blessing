using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    [Header("UI")] public TextMeshProUGUI textGeneral;
    public TextMeshProUGUI textBackground;
    public TextMeshProUGUI textVFX;
    public TextMeshProUGUI textUI;
    public Slider sliderGeneral;
    public Slider sliderBackground;
    public Slider sliderVFX;
    public Slider sliderUI;


    public void ChangeGeneralText()
    {
        textGeneral.text = sliderGeneral.value.ToString();
    }

    public void ChangeBackgroundText()
    {
        textBackground.text = sliderBackground.value.ToString();
    }

    public void ChangeVFXText()
    {
        textVFX.text = sliderVFX.value.ToString();
    }

    public void ChangeUIText()
    {
        textUI.text = sliderUI.value.ToString();
    }

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