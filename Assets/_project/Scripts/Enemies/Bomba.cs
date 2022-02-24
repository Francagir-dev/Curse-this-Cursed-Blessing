using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    Rigidbody inplaceBomb;
    public float time = 5;
    public float radiusExpl = 10;
    public Transform warn;
    float detonationTime;
    Vector3 posit;


    float DTI;
    bool detonate = false;

    private void Awake()
    {
        inplaceBomb = GetComponent<Rigidbody>();
        transform.parent = null;
        transform.localScale = Vector3.one;
    }

    void OnEnable()
    {
        StopAllCoroutines();
        posit = Movement.instance.transform.position;
        warn.localScale = Vector3.zero;
        detonationTime = -1;
        detonate = false;
        inplaceBomb.isKinematic = false;
        inplaceBomb.velocity = CalculateSpeed(posit, transform.position, time * .66f);
    }

    private void Update()
    {
        if (detonationTime > 0)
        {
            warn.localScale = Vector3.Lerp(Vector3.one * 19, Vector3.zero, detonationTime/(time * .33f));
            detonationTime -= Time.deltaTime;

            if (detonationTime <= 0)
            {
                Detonate();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radiusExpl);
    }

    void Detonate()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, radiusExpl);
        foreach (var item in colls)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.GetComponent<LifeSystem>().Damage(1);
                break;
            }
        }
        warn.localScale = Vector3.zero;
        StartCoroutine(Explode());

        IEnumerator Explode()
        {
            float time = .2f;
            while (time > 0)
            {
                yield return null;
                time -= Time.deltaTime;
                transform.localScale = Vector3.Lerp(Vector3.one * radiusExpl/1.5f, Vector3.one, time/.2f);
            }

            transform.localScale = Vector3.one;
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (detonate) return;
        DTI = Vector3.Distance(posit, inplaceBomb.transform.position);
        if(DTI <= 1f)
        {
            //Destroy(InplaceBomb.GetComponent<Rigidbody>());
            inplaceBomb.isKinematic = true;
            detonationTime = time * .33f;
            detonate = true;
        }
    }

    Vector3 CalculateSpeed(Vector3 target, Vector3 origin, float time)
    {
        //Definimos las distancias en eje X e Y
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;
        
        //Definimos floats para las distancias del vector
        float Dy = distance.y;
        float Sxz = distanceXZ.magnitude;

        //Calculo del efecto de parabola hasta el objetivo
        float Vx = Sxz / time;
        float Vy = Dy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vx;
        result.y = Vy;

        return result;
    }
}

