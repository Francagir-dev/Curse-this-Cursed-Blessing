using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;


public class DialogueManager : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI textDialogue;
   [SerializeField]  LocalizeStringEvent _stringEvent;
   public LocalizedString myString;
   public string textLocalized;
   void OnEnable()
   {
      myString.StringChanged += UpdateString;
   }

   void OnDisable()
   {
      myString.StringChanged -= UpdateString;
   }

   void UpdateString(string s)
   {
      var translatedText = LocalizationSettings.StringDatabase.GetLocalizedString("ID_FILE", numFiles);
   }

   void Change()
   {
   }
}