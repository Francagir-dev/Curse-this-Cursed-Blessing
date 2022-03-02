using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightThrowable : MonoBehaviour
{
    public float speed;
    bool stop = false;
    public bool Stop { set => stop = value; }

    private void Awake()
    {
        transform.parent = null;
    }

    private void OnEnable()
    {
        stop = false;
    }

    void Update()
    {
        if (!stop)
            transform.position += transform.forward * speed;
    }
}
