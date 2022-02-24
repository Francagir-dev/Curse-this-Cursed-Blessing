using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ImprovedInteraction : MonoBehaviour
{
    public int checker = 0;
    public string OAI;
    public void Interact()
    {
        //checker = PlayerPrefs.GetInt("Checkeador");
        if (checker == 1)
        {
            Debug.Log("Interactuaste!");
            //Sustituir futuros métodos de los Interactuables por los Debugs.
            switch (OAI)
            {
                case "Objeto1":
                    Debug.Log("Interactuaste con Objeto1!");
                    break;
                case "Objeto2":
                    Debug.Log("Interactuaste con Objeto2!");
                    break;
                case "Objeto3":
                    Debug.Log("Interactuaste con Objeto3!");
                    break;
                case "Objeto4":
                    Debug.Log("Interactuaste con Objeto4!");
                    break;
                case "Objeto5":
                    Debug.Log("Interactuaste con Objeto5!");
                    break;
            }
        }
        else 
        {
            Debug.Log("Nada manin");
        }
    }
    void OnInteract(InputAction.CallbackContext context)
    {
        if(context.started)
            Interact();
    }
}
