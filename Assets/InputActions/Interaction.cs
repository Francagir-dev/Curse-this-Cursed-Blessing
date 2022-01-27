using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private GameObject[] multipleInteractables;
    public Transform closestInteractable;
    public bool Contact;

    private void Start()
    {
        closestInteractable = null;
        Contact = false;
    }

    private void Update()
    {
        closestInteractable = getClosestInteractable();
    }

    public Transform getClosestInteractable()
    {
        multipleInteractables = GameObject.FindGameObjectsWithTag("Interactable");
        float closestDistance = Mathf.Infinity;
        Transform trans = null;

        foreach(GameObject go in multipleInteractables)
        {
            float CurrentDistance;
            CurrentDistance = Vector3.Distance(transform.position, go.transform.position);
            if(CurrentDistance < closestDistance)
            {
                closestDistance = CurrentDistance;
                trans = go.transform;
                if(closestDistance <= 1.5f)
                {
                    Contact = true;
                }
            }
        }
        return trans;
    }
    public void Interact()
    {
        
        if (Contact == true)
        {
            Contact = false;
            Debug.Log("Interacting!");
        }
        else
        {
            Debug.Log("Nothing Found!");
        }   
    }
}
