using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Funciono");
        if(other.tag == "Player")
        {
            other.gameobject.GetComponent<ImprovedInteraction>().checker = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Funcionoxd");
        if (other.tag == "Player")
        {
            other.gameobject.GetComponent<ImprovedInteraction>().checker = 0;
        }
    }
}
