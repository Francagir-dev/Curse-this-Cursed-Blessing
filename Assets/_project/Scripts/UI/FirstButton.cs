using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class FirstButton : MonoBehaviour
{
    public GameObject itemToSelect;
    public EventSystem eventSystem;
   
    void OnEnable()
    {
        StartCoroutine(ActivateItem());
    }

    IEnumerator ActivateItem()
    {
        eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        eventSystem.SetSelectedGameObject(itemToSelect);
    }
}