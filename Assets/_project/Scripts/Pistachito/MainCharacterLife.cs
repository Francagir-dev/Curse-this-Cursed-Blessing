using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacterLife : LifeSystem
{
   public GameObject dead;
    DamageEffect damageEffect;
    [HideInInspector] public Material material;

    private Vector3 newFirst = Vector3.zero;
    private Vector3 newSecond = Vector3.zero;

    public float degradeCooldown = 0.25f;
    public float invencibleCooldown = 0.8f;

    private Vector3 middle = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 quarterTo = new Vector3(0.75f, 0.75f, 0.75f);
    private Vector3 quarterPast = new Vector3(0.25f, 0.25f, 0.25f);
    private Vector3 minZero = new Vector3(0.1f, 0.1f, 0.1f);

    private string first = "Vector3_406caae1e3a740299dba88c3ea433e62";
    private string second = "Vector3_c205aba480a54b729b57469bf6fa83ac";

    public Image lifeImage;
    public Sprite[] lifeUI;

    Shaker camShake;

    private void Awake()
    {
        dead = GameObject.Find("--Death--");
        dead.SetActive(false);
        onDamage.AddListener(CheckDeath);
        damageEffect = GetComponent<DamageEffect>();
        material = GetComponent<MeshRenderer>().material;

        camShake = FindObjectOfType<Shaker>();
    }

    private void Start()
    {
        onDamage.AddListener(ChangeImage);
        onDamage.AddListener(ChangeColorDegrade);
        onDamage.AddListener(() => camShake.BeginShake(.5f, .5f));
    }

    private void ChangeImage()
    {
        lifeImage.sprite = lifeUI[Mathf.Clamp(Life - 1, 0, lifeUI.Length)];
    }

    //Colision de pureba mas cutre que mi existencia

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Heal"))
        {
            Heal(1);
            ChangeColorDegrade();
            if (Life > 5)
            {
                Life = 5;
            }
        }
    }

    void CheckDeath()
    {
        if (Life <= 0)
        {
            dead.SetActive(true);
            dead.GetComponent<DeathMenu>().SetEventSystem();
            lifeImage.enabled = false;
        }
    }

    //Sistema para llamar a cada caso de cantidad de vida y asi modificarla

    public void ChangeColorDegrade()
    {
        switch (Life)
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
        StartCoroutine(CoolDown());
    }
    public void ChangeColorUpgrade()
    {
        switch (Life)
        {
            case -1:
                StartCoroutine(ColorUpgrade(minZero, minZero, Vector3.zero, Vector3.zero));
                break;
            case 0:
                StartCoroutine(ColorUpgrade(minZero, quarterPast, minZero, minZero));
                break;
            case 1:
                StartCoroutine(ColorUpgrade(quarterPast, middle, minZero, quarterPast));
                break;
            case 2:
                StartCoroutine(ColorUpgrade(middle, quarterTo, quarterPast, middle));
                break;
            case 3:
                StartCoroutine(ColorUpgrade(quarterTo, middle, middle, quarterTo));
                break;
            case 4:
                StartCoroutine(ColorUpgrade(Vector3.one, Vector3.zero, quarterTo, middle));
                break;
            case 5:
                material.SetVector(first, Vector3.one);
                material.SetVector(second, Vector3.zero);
                break;
        }
    }

    //Por temas de velocidad del degradado prefiero pasarlo por su cuenta su invencibilidad

    IEnumerator CoolDown()
    {
        Inv = true;
        damageEffect.Activate = true;
        yield return new WaitForSeconds(invencibleCooldown);
        Inv = false;
        damageEffect.Activate = false;
    }

    IEnumerator ColorDegrade(Vector3 firstVector, Vector3 secondVector, Vector3 goToFirst, Vector3 goToSecond)
    {
        newFirst = firstVector - minZero;

        if (newFirst.x < goToFirst.x && newFirst.y < goToFirst.y && newFirst.z < goToFirst.z)
            newFirst = goToFirst;
        

        if (secondVector.x > goToSecond.x)
        {
            newSecond = secondVector - minZero;
            if (newSecond.x < goToSecond.x && newSecond.y < goToSecond.y && newSecond.z < goToSecond.z)
                newSecond = goToSecond;
        }

        if (secondVector.x < goToSecond.x)
        {
            newSecond = secondVector + minZero;
            if (newSecond.x > goToSecond.x && newSecond.y > goToSecond.y && newSecond.z > goToSecond.z)
                newSecond = goToSecond;
        }

        material.SetVector(first, newFirst);
        material.SetVector(second, newSecond);

        yield return new WaitForSeconds(degradeCooldown);

        if(newFirst.x == goToFirst.x && newFirst.y == goToFirst.y && newFirst.z == goToFirst.z && secondVector.x == goToSecond.x && secondVector.y == goToSecond.y && secondVector.z == goToSecond.z)
            StopCoroutine(ColorDegrade(newFirst, newSecond, goToFirst, goToSecond));
        else StartCoroutine(ColorDegrade(newFirst, newSecond, goToFirst, goToSecond));
    }

    IEnumerator ColorUpgrade(Vector3 firstVector, Vector3 secondVector, Vector3 goToFirst, Vector3 goToSecond)
    {
        newFirst = firstVector + minZero;

        if (newFirst.x > goToFirst.x && newFirst.y > goToFirst.y && newFirst.z > goToFirst.z)
            newFirst = goToFirst;
        

        if (secondVector.x < goToSecond.x)
        {
            newSecond = secondVector - minZero;
            if (newSecond.x > goToSecond.x && newSecond.y > goToSecond.y && newSecond.z > goToSecond.z) 
                newSecond = goToSecond;
        }

        if (secondVector.x > goToSecond.x)
        {
            newSecond = secondVector + minZero;
            if (newSecond.x < goToSecond.x && newSecond.y < goToSecond.y && newSecond.z < goToSecond.z)
                newSecond = goToSecond;
        }

        material.SetVector(first, newFirst);
        material.SetVector(second, newSecond);

        yield return new WaitForSeconds(degradeCooldown);

        if (newFirst.x == goToFirst.x && newFirst.y == goToFirst.y && newFirst.z == goToFirst.z && secondVector.x == goToSecond.x && secondVector.y == goToSecond.y && secondVector.z == goToSecond.z)
            StopCoroutine(ColorDegrade(newFirst, newSecond, goToFirst, goToSecond));
        else StartCoroutine(ColorDegrade(newFirst, newSecond, goToFirst, goToSecond));
    }
}