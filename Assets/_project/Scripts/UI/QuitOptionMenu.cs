using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuitOptionMenu : MonoBehaviour
{
    public GameObject optionMenu;
    public GameObject menuActivate;
    public GameObject startBtn;
    private EventSystem _eventSystem;

    private void Awake()
    {
        _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }
  

    IEnumerator ActivateItem(GameObject objectToSelect)
    {
        _eventSystem.firstSelectedGameObject = null;
        yield return new WaitForEndOfFrame();
        _eventSystem.firstSelectedGameObject = objectToSelect;
        Debug.Log("eeo");
    }

    public void MainMenuSelection()
    {
        menuActivate.SetActive(true);
        startBtn = GameObject.Find("StartBtn");
        Debug.Log(startBtn.name);
        StartCoroutine(ActivateItem(startBtn));
        optionMenu.SetActive(false);
    }

    public void PauseMenuSelection()
    {
        menuActivate.SetActive(true);
        startBtn = GameObject.Find("ResumeBtn");
        Debug.Log(startBtn.name);
        StartCoroutine(ActivateItem(startBtn));
        optionMenu.SetActive(false);
    }

}