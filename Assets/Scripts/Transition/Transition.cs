using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Transition : MonoBehaviour
{
    Image image;

    private float alpha = 255f;
    [SerializeField] private float fadeDown = 1f;

    public UnityEvent onTransition;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        Off();
    }

    public void Off()
    {
        StartCoroutine(TransitionToOff());
    }

    public void On()
    {
        StartCoroutine(TransitionToOn(false));
    }

    public void ToOnToOff()
    {
        StartCoroutine(TransitionToOn(true));
    }

    IEnumerator TransitionToOff()
    {
        float time = fadeDown;

        while (time > 0)
        {
            time -= Time.deltaTime;
            alpha = Mathf.Lerp(0, 1, time / fadeDown);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null;
        }

        onTransition.RemoveAllListeners();
    }

    IEnumerator TransitionToOn(bool doble)
    {
        alpha = 255;
        float time = fadeDown;
        while (time > 0)
        {
            time -= Time.deltaTime;
            alpha = Mathf.Lerp(1, 0, time / fadeDown);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null;
        }

        onTransition?.Invoke();
        Off();
    }
}
