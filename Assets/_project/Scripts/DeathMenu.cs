using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeathMenu : MonoBehaviour
{
    private EventSystem eventSystem;
    public GameObject defaultEventSystem;
    public void SetEventSystem()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        StartCoroutine(ChangeEventSystem(defaultEventSystem));
        Time.timeScale = 0;
    }
    
    IEnumerator ChangeEventSystem(GameObject itemSelected)
    {
        eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        eventSystem.SetSelectedGameObject(itemSelected);
    }

    public void BackMenu()
    {
        Time.timeScale = 1;
        Transition.Instance.Do("MainMenu");
    }
    
    public void ReStart()
    {
        Time.timeScale = 1;
        Transition.Instance.Do("Exploracion1");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
