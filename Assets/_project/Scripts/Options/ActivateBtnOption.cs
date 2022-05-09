using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;

public class ActivateBtnOption : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject firstBtn;
    [SerializeField] private GameObject generalBTN;
    [SerializeField] private GameObject generalPanel;
    [SerializeField] private GameObject graphicPanel;
    [SerializeField] private GameObject graphicBTN;
    [SerializeField] private GameObject volumePanel;
    [SerializeField] private GameObject volumeBTN;
    [SerializeField] private GameObject controlPanel;
    [SerializeField] private GameObject controlBTN;
    [SerializeField] private GameObject previousSelected;

    private void Awake()
    {
        Debug.Log(LocalizationSettings.SelectedLocale);
        _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    private void OnEnable()
    {
        generalPanel.SetActive(true);
    }
    public void ActivateGeneralPanel()
    {
        if (previousSelected != null) previousSelected.SetActive(false);
        generalPanel.SetActive(true);
        previousSelected = generalPanel;
        StartCoroutine(ActivateItem(generalBTN));
    }
    public void ActivateGraphicPanel()
    {
        if (previousSelected != null) previousSelected.SetActive(false);
        graphicPanel.SetActive(true);
        previousSelected = graphicPanel;
        StartCoroutine(ActivateItem(graphicBTN));
    }

    public void ActivateVolumePanel()
    {
        if (previousSelected != null) previousSelected.SetActive(false);
        volumePanel.SetActive(true);
        previousSelected = volumePanel;
        StartCoroutine(ActivateItem(volumeBTN));
    }

    public void ActivateControlPanel()
    {
        if (previousSelected != null) previousSelected.SetActive(false);
        previousSelected = controlPanel;
        controlPanel.SetActive(true);
        StartCoroutine(ActivateItem(controlBTN));
    }

    IEnumerator ActivateItem(GameObject objectToSelect)
    {
        _eventSystem.firstSelectedGameObject = null;
        yield return new WaitForEndOfFrame();
        _eventSystem.firstSelectedGameObject = objectToSelect;
    }

    private void OnDisable()
    {
        previousSelected.SetActive(false);
        previousSelected = generalPanel;
    }
}