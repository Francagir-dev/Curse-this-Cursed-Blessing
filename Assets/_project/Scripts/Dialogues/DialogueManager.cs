using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;


public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDialogue;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] LocalizeStringEvent _stringEvent;
    public LocalizedString myString;

    private List<string> keys = new List<string>();
    public int showToKey = 0;
    int currKey = 0;

    [SerializeField] private string tableName;
    public string TableName { set { tableName = value; GetAllKeys(tableName); } }

    [SerializeField] [Range(0f, 20f)] private float timeChangingText = 5f;
    [SerializeField] private float timeBetweenChar = .1f;
    //private StringTableCollection collection;
    private StringTable stringTable;
    [SerializeField] private bool automaticText;

    public Action onDialogueEnd;

    Animator anim;
    bool isOpen = false;
    public bool IsOpen => isOpen;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("es");
    }

    private void Start()
    {
        Open();
    }

    void OnEnable()
    {
        myString.StringChanged += UpdateString;
        //StartCoroutine(PrologueText(tableName, timeChangingText));
    }

    public void Open()
    {
        if (isOpen || currKey >= showToKey) return;
        isOpen = true;
        anim.Play("Open");
        NextDialogue();
    }

    public void Close()
    {
        if (!isOpen) return;
        isOpen = false;
        anim.Play("Close");
        onDialogueEnd?.Invoke();
        onDialogueEnd = null;
    }

    void OnDisable()
    {
        myString.StringChanged -= UpdateString;
    }

    void UpdateString(string s)
    {

    }

    public void Skip(int keyToSkip)
    {
        showToKey = keyToSkip;
        currKey = keyToSkip;
    }

    void GetAllKeys(string tableName)
    {
        //collection = LocalizationEditorSettings.GetStringTableCollection(tableName);
        //stringTable = collection.GetTable("en") as StringTable;
        stringTable = LocalizationSettings.StringDatabase.GetTable(tableName);
        keys = new List<string>();

        foreach (var v in stringTable)
        {
            keys.Add(stringTable.SharedData.GetEntry(v.Key).Key);
        }
        Debug.Log(keys.Count);
    }

    IEnumerator PrologueText(string table, float timeBetweenSentences)
    {
        GetAllKeys(table);

        for (int i = 0; i < keys.Count; i++)
        {
            string translatedText = LocalizationSettings.StringDatabase.GetLocalizedString(table, keys[i]);

            //Letras graduales, con input para skipear
            string displayText = "";
            float timer = timeBetweenChar;
            for (int j = 0; j < translatedText.Length; j++)
            {
                displayText += translatedText[j];
                //While en vez de WaitForSeconds para poder detectar input
                while (timer > 0)
                {
                    timer -= Time.deltaTime;
                    //TODO: Poner input de verdad, esto es temporal y para testear
                    if (Keyboard.current.anyKey.wasReleasedThisFrame)
                    {
                        displayText = translatedText;
                        j = translatedText.Length;
                        break;
                    }
                    yield return null;
                }
                timer = timeBetweenChar;
                textDialogue.text = displayText;
            }

            yield return new WaitForSeconds(0.5f);

            if (automaticText)
                yield return new WaitForSeconds(timeBetweenSentences);
            else
                yield return new WaitUntil(() => Keyboard.current.anyKey.wasReleasedThisFrame);
        }
    }

    void NextDialogue()
    {
        if (currKey >= showToKey || keys.Count <= currKey)
        {
            Close();
            return;
        }
        string name = keys[currKey].Split('_')[0];
        
        switch (name)
        {
            case "Hero":
                name = "Heroé";
                break;
            case "Pistachin":
                name = "Pistachin";
                break;
            case "Secuaz":
                name = "Secuaz";
                break;
            case "Juan":
                name = "Juan";
                break;
            default:
                name = "InvalidName";
                break;
        }

        textName.text = name;

        string translatedText = LocalizationSettings.StringDatabase.GetLocalizedString(tableName, keys[currKey]);
        StartCoroutine(WriteText(translatedText, timeChangingText));
        currKey++;
    }

    IEnumerator WriteText(string translatedText, float timeBetweenSentences)
    {
        float timer = timeBetweenChar;
        string displayText = "";
        for (int j = 0; j < translatedText.Length; j++)
        {
            displayText += translatedText[j];
            //While en vez de WaitForSeconds para poder detectar input
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                //TODO: Poner input de verdad, esto es temporal y para testear
                if (Keyboard.current.anyKey.wasReleasedThisFrame)
                {
                    displayText = translatedText;
                    j = translatedText.Length;
                    break;
                }
                yield return null;
            }
            timer = timeBetweenChar;
            textDialogue.text = displayText;
        }

        yield return new WaitForSeconds(0.5f);

        if (automaticText)
            yield return new WaitForSeconds(timeBetweenSentences);
        else
            yield return new WaitUntil(() => Keyboard.current.anyKey.wasReleasedThisFrame);

        yield return null;

        NextDialogue();
    }
}