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
        textGeneral.text = Mathf.Round(sliderGeneral.value * 100).ToString();
    }

    public void ChangeBackgroundText()
    {
        textBackground.text = Mathf.Round(sliderBackground.value * 100).ToString();
    }

    public void ChangeVFXText()
    {
        textVFX.text = Mathf.Round(sliderVFX.value * 100).ToString();
    }

    public void ChangeUIText()
    {
        textUI.text = Mathf.Round(sliderUI.value * 100).ToString();
    }

    public void SetGeneralVolume(float Gvolume)
    {
        audioMixer.SetFloat("GVolume", Mathf.Log10(Gvolume) * 20);
    }

    public void BGVolume(float BGvolume)
    {
        audioMixer.SetFloat("BGVolume", Mathf.Log10(BGvolume) * 20);
    }

    public void VFXVolume(float EVolume)
    {
        audioMixer.SetFloat("EVolume", Mathf.Log10(EVolume) * 20);
    }

    public void UIVolume(float UIvolume)
    {
        audioMixer.SetFloat("UIVolume", Mathf.Log10(UIvolume) * 20);
    }
}