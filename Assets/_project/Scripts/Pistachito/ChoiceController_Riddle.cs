using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Settings;
using UnityEngine.Events;

public class ChoiceController_Riddle : MonoBehaviour
{
    struct TextBox
    {
        public TextMeshProUGUI text;
        public Transform trf;
        public bool correct;
        public Coroutine sizeRoutine;

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

            correct = false;
            sizeRoutine = null;

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

    public string tableName;
    StringTable riddles;

    List<string> keys;
    int currentKey;

    [Header("UI")]
    public float textBoxReducedSizeMult = .8f;
    public TextMeshProUGUI riddleText;

    public UnityEvent onWin;
    public UnityEvent onLose;

    int tries = 0;
    int correct = 0;

    TextBox[] textBox = new TextBox[4];
    Image blackBackg;
    Transform choicesUI;

    int choosen = -1;
    bool canChoose = false;

    float timer;
    float timeInterval = 5f;
    Animator anim;

    private void Awake()
    {
        FindObjectOfType<Movement>().riddle = this;
        anim = GetComponent<Animator>();
        keys = new List<string>();
        riddles = LocalizationSettings.StringDatabase.GetTable(tableName);

        Debug.Log("he");
        foreach (var v in riddles)
            keys.Add(riddles.SharedData.GetEntry(v.Key).Key);

        SetUI(gameObject);
        ResetBoxes();
        EnableUI(false);
    }

    private void OnEnable()
    {
        tries = 0;
        correct = 0;
        currentKey = 0;
        SetNextText();
    }

    void SetNextText()
    {
        if (currentKey >= keys.Count)
        {
            if (correct == 3)
                onWin.Invoke();
            else onLose.Invoke();

            return;
        }

        riddleText.text = GetText(currentKey);
        anim.SetBool("Down", true);

        if (keys[currentKey].StartsWith("?"))
        {
            SetAnswer(0, GetText(currentKey + 1), keys[currentKey + 1].StartsWith("$"));
            SetAnswer(1, GetText(currentKey + 2), keys[currentKey + 2].StartsWith("$"));
            SetAnswer(2, GetText(currentKey + 3), keys[currentKey + 3].StartsWith("$"));
            SetAnswer(3, GetText(currentKey + 4), keys[currentKey + 4].StartsWith("$"));

            currentKey += 5;

            EnableChoices();
        }
        else StartCoroutine(WaitText());

        IEnumerator WaitText()
        {
            currentKey++;
            yield return new WaitForSeconds(5);
            anim.SetBool("Down", false);
            yield return new WaitForSeconds(1);
            SetNextText();
        }

        string GetText(int key)
        {
            string answer = LocalizationSettings.StringDatabase.GetLocalizedString(tableName, keys[key]);
            Debug.Log(answer);
            return answer;
        }
    }

    public void EnableChoices()
    {
        if (canChoose) return;
        
        choosen = -1;
        ResetBoxes();
        EnableUI(true);
        canChoose = true;
    }

    public void ChooseOption(int option)
    {
        if (!canChoose) return;

        if (choosen == option)
        {
            ConfirmChoice();
            return;
        }

        if (textBox[option].sizeRoutine != null)
            StopCoroutine(textBox[option].sizeRoutine);
        textBox[option].sizeRoutine = StartCoroutine(ChangeSize(textBox[option].trf, Vector3.one, 1));

        if (choosen != -1)
        {
            if (textBox[choosen].sizeRoutine != null)
                StopCoroutine(textBox[choosen].sizeRoutine);
            textBox[choosen].sizeRoutine = StartCoroutine(ChangeSize(textBox[choosen].trf, Vector3.one * textBoxReducedSizeMult, 1));
        }

        choosen = option;
    }

    /// <summary>
    /// Saca rapidamente una sequencia de 4 numeros, del 0 al 3, de forma desordenada
    /// </summary>
    /// <returns>La lista de numeros desordenada</returns>
    public int[] Sequence()
    {
        List<int> seq = new List<int> { 0, 1, 2, 3 };
        int[] realSeq = new int[seq.Count];

        for (int i = 0; i < 4; i++)
        {
            int a = Random.Range(0, seq.Count);
            realSeq[i] = seq[a];
            seq.RemoveAt(a);
        }

        return realSeq;
    }

    /// <summary>
    /// Setear una respuesta de las elecciones
    /// </summary>
    /// <param name="choice">Indice de la elecci�n</param>
    /// <param name="text">Texto de la elecci�n</param>
    /// <param name="power">Poder de la elecci�n</param>
    public void SetAnswer(int choice, string text, bool isCorrect)
    {
        textBox[choice].text.text = text;
        textBox[choice].correct = isCorrect;
    }

    public void ConfirmChoice()
    {
        tries++;
        canChoose = false;

        if (textBox[choosen].correct)
            correct++;

        for (int i = 0; i < textBox.Length; i++)
        {
            if (choosen == i) continue;
            if (textBox[i].sizeRoutine != null)
                StopCoroutine(textBox[i].sizeRoutine);
            textBox[i].sizeRoutine = StartCoroutine(ChangeSize(textBox[i].trf, Vector3.zero, 1));
        }


        StartCoroutine(WaitForClose());

        IEnumerator WaitForClose()
        {
            yield return new WaitForSeconds(3f);
            EnableUI(false);
            yield return new WaitForSeconds(1f);
            SetNextText();
        }
    }

    #region UIManagment
    /// <summary>
    /// Set the ui for the choices
    /// </summary>
    /// <param name="ui">Prefab of the ui</param>
    void SetUI(GameObject ui)
    {
        if (ui == null)
        {
            Debug.Log("UI Object not found! Spawn a prefab of '--PlayerWheel--' in the scene!");
            return;
        }

        choicesUI = ui.transform.Find("--Choices--");

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

        //StartCoroutine(ChangeSize(obj, obj.localScale == Vector3.zero ? Vector3.one * .8f : Vector3.zero, 2));
    }

    public void EnableUI(bool enable)
    {
        anim.SetBool("Down", enable);
        canChoose = enable;
        Vector3 activate = enable ? Vector3.one : Vector3.zero;
        StartCoroutine(ChangeSize(choicesUI, activate, 2));
    }

    void ResetBoxes()
    {
        for (int i = 0; i < textBox.Length; i++)
            textBox[i].trf.localScale = Vector3.one * textBoxReducedSizeMult;
    }
    #endregion
}