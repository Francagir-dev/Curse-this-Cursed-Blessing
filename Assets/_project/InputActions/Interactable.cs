using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onTrigger;
    bool playerDetected = false;
    public bool PlayerDetected => playerDetected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            playerDetected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerDetected = false;
    }
}
