using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DampFollow : MonoBehaviour
{
    float damp = .35f;
    Quaternion TargetRot => transform.parent.rotation * lockedRot;

    Quaternion lockedRot;
    Quaternion denyRot = Quaternion.identity;

    private void Awake()
    {
        lockedRot = transform.localRotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = denyRot;

        transform.rotation = Quaternion.Lerp(transform.rotation, TargetRot, damp);

        denyRot = transform.rotation;
    }

    public void NewLockedRot(Quaternion newRot) 
        => lockedRot = newRot;

    public void NewLockedRot() 
        => lockedRot = transform.localRotation;
}
