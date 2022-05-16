using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour
{
    float damp = .05f;
    Vector3 TargetPos => transform.parent.position + lockedPos;

    Vector3 lockedPos;
    Vector3 denyPos;

    private void Awake()
    {
        lockedPos = transform.localPosition;
        denyPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = denyPos;

        transform.position = Vector3.Lerp(transform.position, TargetPos, damp);

        denyPos = transform.position;
    }

    public void NewLockedRot(Vector3 newPos)
        => lockedPos = newPos;

    public void NewLockedRot()
        => lockedPos = transform.localPosition;
}
