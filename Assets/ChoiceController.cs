using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceController : MonoBehaviour
{
    // Ampliar al elegir
    // Dejar flotando elección despues de confirmar
    // Ralentizar tiempo

    struct TextBox
    {
        public TextMeshProUGUI text;
        public Transform trf;

        public TextBox(GameObject box)
        {
            trf = box.transform;

            Transform theText = box.transform;

            for (int i = 0; i < box.transform.childCount; i++)
            {
                theText = SearchChildren(box.transform.GetChild(i));
                if (theText.name == "--Text--") break;
            }

            text = theText.GetComponent<TextMeshProUGUI>();

            Transform SearchChildren(Transform parent)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    if (parent.GetChild(i).name == "--Text--")
                        return parent.GetChild(i);
                    else return SearchChildren(parent.GetChild(i));
                }

                return null;
            }
        }
    }

    public enum DamageType
    {
        None,
        Low,
        High
    }

    [Header("Stats")]
    [SerializeField] float timeSlow;
    [Range(0,1)]
    [SerializeField] float timeSlowDuration;


    //UI
    public DamageType[] avalibleDamage = { DamageType.None, DamageType.None, DamageType.Low, DamageType.High };
    TextBox[] textBox = new TextBox[4];
    Image blackBackg;
    Transform choicesUI;

    bool canAnswer = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        SetUI(GameObject.Find("--PlayerWheel--"));
        //StartCoroutine(ChangeSize(choicesUI, Vector3.zero, 2));
    }

    /// <summary>
    /// Set the ui for the choices
    /// </summary>
    /// <param name="ui">Prefab of the ui</param>
    void SetUI(GameObject ui)
    {
        blackBackg = ui.transform.Find("--Back--").GetComponent<Image>();
        choicesUI = ui.transform.Find("--Choices--");

        Debug.Log("--" + Translate(0) + "--");

        for (int i = 0; i < textBox.Length; i++)
            textBox[i] = new TextBox(choicesUI.transform.Find("--" + Translate(i) + "--").gameObject);
        

        string Translate(int num)
        {
            switch (num)
            {
                case 0:
                    return "Up";
                case 1:
                    return "Right";
                case 2:
                    return "Down";
                case 3:
                    return "Left";
                default:
                    return "";
            }
        }
    }

    IEnumerator ChangeSize(Transform obj, Vector3 newSize, float time)
    {
        float timeLeft = time;

        while(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            obj.localScale = Vector3.Lerp(newSize, obj.localScale, timeLeft/time);
            yield return null;
        }

        obj.localScale = newSize;

        StartCoroutine(ChangeSize(obj, obj.localScale == Vector3.zero ? Vector3.one * .8f : Vector3.zero, 2));
    }
}