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
    public EventSystem _eventSystem;

  
    IEnumerator ActivateItem(GameObject objectToSelect)
    {
        _eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        _eventSystem.SetSelectedGameObject(objectToSelect);
        
    }

    public void MainMenuSelection()
    {
        menuActivate.SetActive(true);
        StartCoroutine(ActivateItem(startBtn));
        optionMenu.SetActive(false);
    }

    public void PauseMenuSelection()
    {
        menuActivate.SetActive(true);
        StartCoroutine(ActivateItem(startBtn));
        optionMenu.SetActive(false);
    }

}