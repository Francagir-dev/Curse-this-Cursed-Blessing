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

    void Start()
    {
        myLight = GetComponent<HDAdditionalLightData>();
    }

    void Update()
    {
        myLight.intensity = Mathf.PingPong(Time.time * velocity, Maxintensity) + intensity;
        Debug.Log(intensity);
    }
}
