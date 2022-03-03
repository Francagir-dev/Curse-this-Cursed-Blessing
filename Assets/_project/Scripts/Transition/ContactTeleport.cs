using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactTeleport : ContactEvent
{
    [Header("Settings")]
    [SerializeField] Transform teleportTo;

    Transform playertransform;
    Vector3 lastPosition;
   
    private void Start()
    {
        playertransform = Movement.Instance.transform;
    }

    public void Teleport()
    {
        playertransform.SetPositionAndRotation(teleportTo.position, teleportTo.rotation);
        lastPosition = playertransform.position;
    }

    public void GoBack() 
        => playertransform.position = lastPosition;

}

