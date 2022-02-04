using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceController : MonoBehaviour
{
    struct TextBox
    {
        public Text text;
        public Rect rect;

        public TextBox(GameObject box)
        {
            rect = box.GetComponent<Rect>();
            text = box.transform.Find("--Text--").GetComponent<Text>();
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

    private void Awake()
    {
        SetUI(GameObject.Find("--PlayerWheel--"));
    }

    void SetUI(GameObject ui)
    {
        blackBackg = ui.transform.Find("--Back--").GetComponent<Image>();

        for (int i = 0; i < textBox.Length; i++)
        {
            textBox[i] = new TextBox(ui.transform.Find("--" + Translate(i) + "--").gameObject);
        }

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
}
