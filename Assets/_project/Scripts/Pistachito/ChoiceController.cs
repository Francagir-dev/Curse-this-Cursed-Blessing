using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceController : MonoBehaviour
{
    struct TextBox
    {
        public TextMeshProUGUI text;
        public Transform trf;
        public int power;
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

            power = 0;
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

    public bool enableTime;

    [Header("Stats")]
    [SerializeField] float intervals = 10; 
    [SerializeField] float choiceDuration = 10; 
    [Range(0,1)]
    [SerializeField] float timeSlow = .5f;
    [SerializeField] float timeSlowDuration = 3;

    public Enemy enemy;
    private Movement player;
    ProgressBar bar;

    //Temporal
    public Answers answerPool;

    float timeLeft = 0;
    float timeSlowTimeLeft = 0;

    [Header("UI")]
    public float textBoxReducedSizeMult = .8f;
    TextBox[] textBox = new TextBox[4];
    Image blackBackg;
    Transform choicesUI;

    int choosen = -1;
    bool canChoose = false;
    bool canTimeSlow = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        //enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Movement>();
        //enemy.OnDamageReceived.AddListener((enemy as Aprendiz).ReceiveDamage);
        SetUI(GameObject.Find("--PlayerWheel--"));
        ResetBoxes();
        EnableUI(false);
    }

    private void Update()
    {
        CheckTime();
        CheckTimeSlow();
    }

    void CheckTime()
    {
        if (timeLeft <= 0 || !enableTime) return;

        float distance = 1;
        float modyfier = 1;
        if (!canChoose)
        {
            distance = Vector3.Distance(enemy.transform.position, player.transform.position);
            modyfier = 10;
        }
        timeLeft -= Time.deltaTime * modyfier / distance;

        bar.Current = Mathf.FloorToInt((timeLeft / choiceDuration) * 100);

        if (timeLeft > 0) return;

        timeSlowTimeLeft = .1f;
        if (canChoose) EnableUI(false);
        else EnableChoices();
    }

    void CheckTimeSlow()
    {
        if (timeSlowTimeLeft <= 0) return;

        timeSlowTimeLeft -= Time.unscaledDeltaTime;

        if (timeSlowTimeLeft > 0) return;

        Time.timeScale = 1;
        SetBackAlpha(0);
    }

    void SlowTime()
    {
        if (!canTimeSlow) return;

        canTimeSlow = false;
        timeSlowTimeLeft = timeSlowDuration;
        SetBackAlpha(.4f);
        Time.timeScale = timeSlow;
    }

    public void EnableChoices()
    {
        if (canChoose == true) return;
        
        choosen = -1;
        ResetBoxes();
        answerPool.RandomPhrases(this);
        EnableUI(true);
        canChoose = true;
        timeLeft = choiceDuration;
        canTimeSlow = true;
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
        else SlowTime();

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
    public void SetAnswer(int choice, string text, int power)
    {
        textBox[choice].text.text = text;
        textBox[choice].power = power;
    }

    public void ConfirmChoice()
    {
        canChoose = false;

        enemy.OnDamageReceived.Invoke(textBox[choosen].power * 10);

        if (enemy.ActualScare >= 100) enableTime = false;

        timeSlowTimeLeft = .1f;

        for (int i = 0; i < textBox.Length; i++)
        {
            if (choosen == i) continue;
            if (textBox[i].sizeRoutine != null)
                StopCoroutine(textBox[i].sizeRoutine);
            textBox[i].sizeRoutine = StartCoroutine(ChangeSize(textBox[i].trf, Vector3.zero, 1));
        }
        timeLeft += 100;
        StartCoroutine(WaitForClose());

        IEnumerator WaitForClose()
        {
            yield return new WaitForSeconds(5f);
            EnableUI(false);
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

        blackBackg = ui.transform.Find("--Back--").GetComponent<Image>();
        SetBackAlpha(0, 0);

        choicesUI = ui.transform.Find("--Choices--");
        bar = choicesUI.Find("--Bar--").gameObject.GetComponent<ProgressBar>();

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

    void SetBackAlpha(float alpha, float time = 1)
    {
        if (time <= 0)
        {
            blackBackg.color = new Color(blackBackg.color.r, blackBackg.color.g, blackBackg.color.b, alpha);
            return;
        }

        StartCoroutine(SetAlpha());
        IEnumerator SetAlpha()
        {
            float timeLeft = time;
            float newAlpha;
            while (timeLeft > 0)
            {
                timeLeft -= Time.unscaledDeltaTime;
                newAlpha = Mathf.Lerp(alpha, blackBackg.color.a, timeLeft / time);
                blackBackg.color = new Color(blackBackg.color.r, blackBackg.color.g, blackBackg.color.b, newAlpha);
                yield return null;
            }

            blackBackg.color = new Color(blackBackg.color.r, blackBackg.color.g, blackBackg.color.b, alpha);
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
        if (!enable)
        {
            canChoose = false;
            timeLeft = intervals;
        }
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