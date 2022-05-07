using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivateBtnOption : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject firstBtn;
    [SerializeField] private GameObject graphicPanel;
    [SerializeField] private GameObject graphicBTN;
    [SerializeField] private GameObject volumePanel;
    [SerializeField] private GameObject volumeBTN;
    [SerializeField] private GameObject controlPanel;
    [SerializeField] private GameObject controlBTN;
    [SerializeField] private GameObject previousSelected;

    private void Awake()
    {
        _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    private void OnEnable()
    {
        graphicPanel.SetActive(true);
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
        previousSelected = graphicPanel;
    }
}