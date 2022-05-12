using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstButton : MonoBehaviour
{
    public GameObject itemToSelect;
    public EventSystem _eventSystem;

   
    void OnEnable()
    {
        StartCoroutine(ActivateItem());
    }

    IEnumerator ActivateItem()
    {

        _eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        _eventSystem.SetSelectedGameObject(itemToSelect);
    }
}