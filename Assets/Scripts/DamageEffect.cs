using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    public GameObject rend;
    public float cooldownTrans = 0.5f;
    [SerializeField] private int countTrans = 3;
    private int count = 0;

    public void DEffect()
    {
        //rend = GetComponent<GameObject>();
        while (countTrans > count)
        {
            rend.SetActive(false);
            new WaitForSeconds(cooldownTrans);
            rend.SetActive(true);
            new WaitForSeconds(cooldownTrans);
            count++;
        }
        count = 0;
    }
}
