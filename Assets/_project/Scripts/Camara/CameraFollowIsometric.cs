using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowIsometric : MonoBehaviour
{
  [SerializeField]  protected Transform target;
  [SerializeField]  protected float smoothing = 5f;
  [SerializeField]  protected Vector3 offset;

    // Update is called once per frame
    void LateUpdate () {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
