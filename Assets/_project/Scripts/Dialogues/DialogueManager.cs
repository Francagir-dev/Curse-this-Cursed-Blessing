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
    [SerializeField] LocalizeStringEvent _stringEvent;
    public LocalizedString myString;
   private List<string> keys = new List<string>();
    [SerializeField] private string tableName;
    [SerializeField] [Range(0f, 20f)] private float timeChangingText = 5f;
    [SerializeField] private float timeBetweenChar = .1f;
    private StringTableCollection collection;
    private StringTable stringTable;
    [SerializeField] private bool automaticText;
    void OnEnable()
    {
        myString.StringChanged += UpdateString;
        StartCoroutine(PrologueText(tableName, timeChangingText));
    }

    void OnDisable()
    {
        myString.StringChanged -= UpdateString;
    }

    void UpdateString(string s)
    {
    }

    void GetAllKeys(string tableName)
    {
        collection = LocalizationEditorSettings.GetStringTableCollection(tableName);
        stringTable = collection.GetTable("en") as StringTable;

        foreach (var v in stringTable)
        {
            keys.Add(stringTable.SharedData.GetEntry(v.Key).Key);
        }
        Debug.Log(keys[0]);
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

    public void SetText(string key)
    {
        string translatedText = LocalizationSettings.StringDatabase.GetLocalizedString("Prologue", key);
        StartCoroutine(WriteText(translatedText, timeChangingText));
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
    }
    
}