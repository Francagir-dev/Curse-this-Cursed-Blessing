using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [Range(0, 1)]
    public float forceMult = .7f;
    public float defaultStopDuration = 1;
    Coroutine shake;
    Vector3 origPos;
    Vector3 newPos;

    float forceValue;

    private void Awake()
    {
        origPos = transform.localPosition;
    }

    private void Start()
    {
        BeginShake(.5f, .5f);
    }

    public void BeginShake(float duration, float value)
    {
        if (shake != null) StopCoroutine(shake);

        forceValue = value;
        shake = StartCoroutine(Shake());
        IEnumerator Shake()
        {
            float time = duration;
            while (time > 0)
            {
                newPos = NewPosition();
                time -= Time.deltaTime;
                transform.localPosition = Vector3.Lerp(origPos, newPos, forceMult);
                yield return null;
            }

            Stop(defaultStopDuration);
        }
    }

    Vector3 NewPosition()
    {
        return origPos + new Vector3(Random.Range(forceValue, -forceValue),
            Random.Range(forceValue, -forceValue),
            Random.Range(forceValue, -forceValue));
    }

    public void Stop(float stopDur)
    {
        if (shake == null) return;

        StopCoroutine(shake);
        shake = StartCoroutine(ShakeSlow());

        IEnumerator ShakeSlow()
        {
            float time = stopDur;
            float actualForce;
            while (time > 0)
            {
                newPos = NewPosition();
                time -= Time.deltaTime;
                actualForce = Mathf.Lerp(0, forceMult, time / stopDur);
                transform.localPosition = Vector3.Lerp(origPos, newPos, actualForce);
                yield return null;
            }

            transform.localPosition = origPos;
        }
    }
}
