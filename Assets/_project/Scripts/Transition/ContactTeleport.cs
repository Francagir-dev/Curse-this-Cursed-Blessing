using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactTeleport : ContactEvent
{
    [Header("Settings")]
    [SerializeField] Transform teleportTo;

    Transform playertransform;
    Vector3 lastPosition;
    bool isBack = false;
   
    private void Start()
    {
        playertransform = Movement.Instance.transform;
    }

    public void Teleport()
    {
        if (isBack)
            return;

        isBack = true;
        lastPosition = playertransform.position;
        playertransform.SetPositionAndRotation(teleportTo.position, teleportTo.rotation);
    }

    public void PermaTeleport(Transform to)
    {
        playertransform.SetPositionAndRotation(to.position, to.rotation);
    }

    public void GoBack()
    {
        if (!isBack)
            return;

        playertransform.position = lastPosition;
        isBack = false;
    }

}

