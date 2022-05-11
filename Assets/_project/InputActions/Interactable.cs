using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onTrigger;
    bool playerDetected = false;
    public bool needTransition = false;
    public bool PlayerDetected { get => playerDetected; set => playerDetected = value; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerDetected = true;
            other.GetComponent<Movement>().DisplayInteraction(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
            other.GetComponent<Movement>().DisplayInteraction(false);
        }
    }
}
