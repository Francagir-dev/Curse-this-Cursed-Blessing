using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour
{
    float damp = .05f;
    Vector3 TargetPos => transform.parent.position + lockedPos;

    Vector3 lockedPos;
    Vector3 denyPos;

    Quaternion TargetRot => transform.parent.rotation * lockedRot;
    Quaternion lockedRot;
    Quaternion denyRot;

    private void Awake()
    {
        lockedPos = transform.localPosition;
        denyPos = transform.position;
        lockedRot = transform.localRotation;
        denyRot = transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = denyPos;
        transform.position = Vector3.Lerp(transform.position, TargetPos, damp);
        denyPos = transform.position;

        transform.rotation = denyRot;
        transform.rotation = Quaternion.Lerp(transform.rotation, TargetRot, damp);
        denyRot = transform.rotation;
    }

    public void NewLockedRot(Vector3 newPos)
        => lockedPos = newPos;

    public void NewLockedRot()
        => lockedPos = transform.localPosition;
}
