using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MovementBehaviour : PlayableBehaviour
{
    public Transform goTo;
    public Transform origPos;
    //Vector3 origPos;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Transform orig = playerData as Transform;

        orig.position = Vector3.Lerp(origPos.position, goTo.position, info.weight);
    }
}