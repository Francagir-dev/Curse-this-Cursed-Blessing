using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedallionCounter : MonoBehaviour
{
    public static MedallionCounter counter;
    public GameObject interaction;
    public int medall;
    private void Awake()
    {
        medall = 0;
        counter = this;
    }

    public void Add()
    {
        medall++;
    }

    private void OnEnable()
    {
        if (medall == 2)
            interaction.SetActive(true);
    }
}
