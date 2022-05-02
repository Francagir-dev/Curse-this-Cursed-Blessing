using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicOptions : MonoBehaviour
{
    private int width,
        height,
        qualityIndex,
        defaultWidth,
        defaultHeight,
        defaultResolutionResolutionOptionIndex,
        _selectedResolutionResolutionOptionIndex;

    private bool fullScreen, vfx;
    private Resolution[] resolutions;

    [SerializeField] private Toggle vfxToggle, fullScreenToggle;

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;


    private void OnEnable()
    {
        PopulateDropDownResolutionOptions();
    }

    public void PopulateDropDownResolutionOptions()
    {
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        resolutionDropdown.ClearOptions();
        defaultResolutionResolutionOptionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                _selectedResolutionResolutionOptionIndex = i;
                defaultWidth = resolutions[i].width;
                defaultHeight = resolutions[i].height;
                defaultResolutionResolutionOptionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = _selectedResolutionResolutionOptionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetQuality(int qualitySettingsIndex)
    {
        qualityIndex = qualitySettingsIndex;
    }

    public void ChangeResolution(int index)
    {
        Resolution resolution = resolutions[index];
        width = resolution.width;
        height = resolution.height;
        _selectedResolutionResolutionOptionIndex = index;
        Screen.SetResolution(width,height, FullScreenMode.FullScreenWindow);
    }
}