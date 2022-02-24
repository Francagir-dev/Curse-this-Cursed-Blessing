using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightThrowable : MonoBehaviour
{
    public float speed;
    bool stop = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (stop) return;

        if (other.CompareTag("Player"))
            other.GetComponent<LifeSystem>().Damage(1);

        gameObject.SetActive(false);
    }
}
