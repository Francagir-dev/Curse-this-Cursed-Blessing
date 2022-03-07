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
    [SerializeField] [Range(2f, 20f)] private float timeChangingText = 5f;
    private StringTableCollection collection;
    private StringTable stringTable;

    void OnEnable()
    {
        myString.StringChanged += UpdateString;
        StartCoroutine(ChangeText(tableName, timeChangingText));
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

    IEnumerator ChangeText(string table, float timeBetweenSentences)
    {
        GetAllKeys(table);

        for (int i = 0; i < keys.Count; i++)
        {
            string translatedText = LocalizationSettings.StringDatabase.GetLocalizedString(table, keys[i]);
            
            textDialogue.text = translatedText;
            yield return new WaitForSeconds(timeBetweenSentences);
        }

       
    }
}