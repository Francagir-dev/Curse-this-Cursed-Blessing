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
        menuActivate.SetActive(true);
        yield return new WaitForEndOfFrame();
        _eventSystem.SetSelectedGameObject(objectToSelect);
        optionMenu.SetActive(false);
    }

    public void MainMenuSelection()
    {
        StartCoroutine(ActivateItem(startBtn));
        
    }

    public void PauseMenuSelection()
    {
     StartCoroutine(ActivateItem(startBtn));
    }

}