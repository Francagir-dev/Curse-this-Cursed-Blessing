using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Transition : MonoBehaviour
{
    private delegate void Effect();
    private Effect effect;

    [SerializeField] private Image image;

    private float alpha = 255f;
    [SerializeField] private float cooldDown = 2f;

    public UnityEvent onOff;
    public UnityEvent onOn;

    private void Start()
    {
        image = GetComponent<Image>();
        effect = Off;
        effect();
    }

    private void Off()
    {
        StartCoroutine(TransitionToOff());
    }

    private void On()
    {
        StartCoroutine(TransitionToOn(false));
    }

    private void ToOnToOff()
    {
        StartCoroutine(TransitionToOn(true));
    }

    IEnumerator TransitionToOff()
    {
        yield return new WaitForSeconds(cooldDown);
        alpha = 0;
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        onOff.Invoke();
    }
    IEnumerator TransitionToOn(bool doble)
    {
        yield return new WaitForSeconds(cooldDown);
        alpha = 255;
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        if (doble)
        {
            effect = Off;
            effect();
        }
        onOn.Invoke();
    }
}
