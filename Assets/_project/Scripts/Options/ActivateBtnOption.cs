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

    private void Awake()
    {
        _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    private void OnEnable()
    {
        StartCoroutine(ActivateItem(firstBtn));
    }

    public void ActivateGraphicPanel()
    {
        volumePanel.SetActive(false);
        graphicPanel.SetActive(true);
        StartCoroutine(ActivateItem(graphicBTN));
    }

    public void ActivateVolumePanel()
    {
        graphicPanel.SetActive(false);
        volumePanel.SetActive(true);
        StartCoroutine(ActivateItem(volumeBTN));
    }

    IEnumerator ActivateItem(GameObject objectToSelect)
    {
        _eventSystem.firstSelectedGameObject = null;
        yield return new WaitForEndOfFrame();
        _eventSystem.firstSelectedGameObject = objectToSelect;
        Debug.Log("eeo");
    }
}