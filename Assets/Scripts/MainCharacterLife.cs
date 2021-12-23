using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterLife : MonoBehaviour
{
    LifeSystem life;
    private Material material;
    private Collider collider;

    private Vector3 newFirst = Vector3.zero;
    private Vector3 newSecond = Vector3.zero;

    public float degradeCooldown = 0.25f;

    private Vector3 middle = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 quarterTo = new Vector3(0.75f, 0.75f, 0.75f);
    private Vector3 quarterPast = new Vector3(0.25f, 0.25f, 0.25f);
    private Vector3 minZero = new Vector3(0.1f, 0.1f, 0.1f);

    private string first = "Vector3_406caae1e3a740299dba88c3ea433e62";
    private string second = "Vector3_c205aba480a54b729b57469bf6fa83ac";

    private void Awake()
    {
        life = GetComponent<LifeSystem>();
        material = GetComponent<MeshRenderer>().material;
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        changeColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            life.Damage(1);
            collider.enabled = false;
            if (life.life < 0)
            {
                Debug.Log("You Die");
            }
        }
    }

    public void changeColor()
    {
        switch (life.life)
        {
            case -1:
                StartCoroutine(ColorDegrade(minZero, minZero, Vector3.zero, Vector3.zero));
                break;
            case 0:
                StartCoroutine(ColorDegrade(minZero, quarterPast, minZero, minZero));
                break;
            case 1:
                StartCoroutine(ColorDegrade(quarterPast, middle, minZero, quarterPast));
                break;
            case 2:
                StartCoroutine(ColorDegrade(middle, quarterTo, quarterPast, middle));
                break;
            case 3:
                StartCoroutine(ColorDegrade(quarterTo, middle, middle, quarterTo));
                break;
            case 4:
                StartCoroutine(ColorDegrade(Vector3.one, Vector3.zero, quarterTo, middle));
                break;
            case 5:
                material.SetVector(first, Vector3.one);
                material.SetVector(second, Vector3.zero);
                break;
        }
        
    }

    IEnumerator ColorDegrade(Vector3 firstVector, Vector3 secondVector, Vector3 goToFirst, Vector3 goToSecond)
    {
        newFirst = firstVector - minZero;

        if (newFirst.x < goToFirst.x && newFirst.y < goToFirst.y && newFirst.z < goToFirst.z)
        {
            newFirst = goToFirst;
        }

        if (secondVector.x > goToSecond.x)
        {
            newSecond = secondVector - minZero;

            if (newSecond.x < goToSecond.x && newSecond.y < goToSecond.y && newSecond.z < goToSecond.z)
            {
                newSecond = goToSecond;
            }
        }

        if (secondVector.x < goToSecond.x)
        {
            newSecond = secondVector + minZero;

            if (newSecond.x > goToSecond.x && newSecond.y > goToSecond.y && newSecond.z > goToSecond.z)
            {
                newSecond = goToSecond;
            }
        }

        material.SetVector(first, newFirst);
        material.SetVector(second, newSecond);

        yield return new WaitForSeconds(degradeCooldown);

        if(newFirst.x == goToFirst.x && newFirst.y == goToFirst.y && newFirst.z == goToFirst.z && secondVector.x == goToSecond.x && secondVector.y == goToSecond.y && secondVector.z == goToSecond.z)
        {
            collider.enabled = true;
            StopCoroutine(ColorDegrade(newFirst, newSecond, goToFirst, goToSecond));
        }

        else
        {
            StartCoroutine(ColorDegrade(newFirst, newSecond, goToFirst, goToSecond));
        }
    }
}