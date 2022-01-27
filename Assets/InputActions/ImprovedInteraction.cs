using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedInteraction : MonoBehaviour
{
    int checker = 0;
    public void Interact()
    {
        checker = PlayerPrefs.GetInt("Checkeador");
        if (checker == 1)
        {
            Debug.Log("Interactuaste!");
        }
        else {
            Debug.Log("Nada manin");
        
        }
    }
}
