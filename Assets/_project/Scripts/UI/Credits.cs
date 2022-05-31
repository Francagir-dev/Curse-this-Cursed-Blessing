using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
   [SerializeField] private Animator anim;
   [SerializeField] private GameObject mainMenuCanvas;
   [SerializeField] private GameObject creditCanvas;

   private void OnEnable()
   {
       StartCoroutine(DisableCredits());
   }

   IEnumerator DisableCredits()
    {
        float length = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length);
        mainMenuCanvas.SetActive(true);
        creditCanvas.SetActive(false);

    }
}
