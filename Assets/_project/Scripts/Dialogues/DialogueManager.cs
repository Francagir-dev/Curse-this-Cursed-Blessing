using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private bool skipText;

    //private List<string> keys = new List<string>();
    public int showToKey = 0;
    int currKey = 0;

    [SerializeField] private string tableName;

    public string TableName
    {
        set { tableName = value; /*GetAllKeys(tableName);*/ }
    }

    //private PlayerInput _player;

    [SerializeField] [Range(0f, 20f)] private float timeChangingText = 5f;

    [SerializeField] private float timeBetweenChar = .1f;

    private StringTable stringTable;
    [SerializeField] private bool automaticText;

    public Action onDialogueEnd;

    Animator anim;
    bool isOpen = false;
    public bool IsOpen => isOpen;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void Start()
    {
        Open();
    }

    void OnEnable()
    {
        myString.StringChanged += UpdateString;
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

    public List<string> GetAllKeys(string tableName)
    {
        stringTable = LocalizationSettings.StringDatabase.GetTable(tableName);
        List<string> keys = new List<string>();

        foreach (var v in stringTable)
        {
            keys.Add(stringTable.SharedData.GetEntry(v.Key).Key);
        }

        return keys;
    }

    IEnumerator PrologueText(string table, float timeBetweenSentences)
    {
        GetAllKeys(table);

        for (int i = 0; i < GetAllKeys(table).Count; i++)
        {
            string translatedText = LocalizationSettings.StringDatabase.GetLocalizedString(table, GetAllKeys(table)[i]);

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
                    if (skipText)
                    {
                        displayText = translatedText;
                        j = translatedText.Length;
                        skipText = false;
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
                yield return new WaitUntil(() => skipText);
        }

        skipText = false;
    }

    void NextDialogue()
    {
        if (currKey >= showToKey || GetAllKeys(tableName).Count <= currKey)
        {
            Close();
            return;
        }

        string name = GetAllKeys(tableName)[currKey].Split('_')[0];

        List<string> keysNames = GetAllKeys("CharacterNames");

        string characterName = LocalizationSettings.StringDatabase.GetLocalizedString("CharacterNames", name);

        Debug.Log(characterName);
        
        textName.text = characterName;

        string translatedText =
            LocalizationSettings.StringDatabase.GetLocalizedString(tableName, GetAllKeys(tableName)[currKey]);
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
                if (skipText)
                {
                    displayText = translatedText;
                    j = translatedText.Length;
                    skipText = false;
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
            yield return new WaitUntil(() => skipText);
        skipText = false;
        yield return null;
        NextDialogue();
    }

    public void SkipText()
    {
        skipText = true;
    }
}