using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    private string IG;

    void Start()
    {
        IG = this.gameObject.name;

        switch (IG)
        {
            case "Objeto1":
                Debug.Log("Identificado1");
                break;
            case "Objeto2":
                Debug.Log("Identificado2");
                break;
            case "Objeto3":
                Debug.Log("Identificado3");
                break;
            case "Objeto4":
                Debug.Log("Identificado4");
                break;
            case "Objeto5":
                Debug.Log("Identificado5");
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Funciono");
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<ImprovedInteraction>().checker = 1;
            other.gameObject.GetComponent<ImprovedInteraction>().OAI = IG;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Funcionoxd");
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<ImprovedInteraction>().checker = 0;
        }
    }
}
