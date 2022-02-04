using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    public MeshRenderer rend;
    public float cooldownTrans = 0.1f;
    [SerializeField] private int countTrans = 3;
    private int count = 0;

    private void Start()
    {
        //rend = GetComponent<GameObject>();
    }
    public void DEffect()
    {
        StartCoroutine(DamEffect());
    }

    //Efecto de parpadeo simplon para comprobar si molesta o beneficia al jugador

    IEnumerator DamEffect()
    {
        if (countTrans > count)
        {
            rend.enabled = false;
            yield return new WaitForSeconds(cooldownTrans);
            rend.enabled = true;
            yield return new WaitForSeconds(cooldownTrans);
            count++;
            StartCoroutine(DamEffect());
        }

        else
        {
            count = 0;
        }
    }
}
