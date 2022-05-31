using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LookAtBehaviour : PlayableBehaviour
{
    public Transform target;
    //Vector3 origPos;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Transform orig = playerData as Transform;

        orig.rotation = Quaternion.Slerp(orig.rotation, Quaternion.LookRotation(target.position - orig.position, Vector3.up), info.weight);
    }
}