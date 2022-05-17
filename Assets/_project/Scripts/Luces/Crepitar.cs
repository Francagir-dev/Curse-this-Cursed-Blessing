using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Crepitar : MonoBehaviour
{
    public float intensity;
    HDAdditionalLightData myLight;
    public float velocity;
    public float Maxintensity;
    public float MaxRadius;
    public float originarange;
    public float duration;

    void Start()
    {
        myLight = GetComponent<HDAdditionalLightData>();
        originarange = myLight.range;
       

    }

    void Update()
    {
        myLight.intensity = Mathf.PingPong(Time.time * velocity, Maxintensity) + intensity;
        Debug.Log(intensity);
        var amplitude = Mathf.PingPong(Time.time, duration);

        //Transform from 0..duration to 0.5..1 range.
        amplitude = amplitude / duration * 0.5f + 0.5f;

        // Set light range.
        myLight.range= originarange * amplitude;
    }
}
