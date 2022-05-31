using UnityEngine;
using UnityEngine.Localization;


public class PersistentInfo : MonoBehaviour
{
    public static PersistentInfo info;
    public Locale localeSelected;

    private void Awake()
    {
        if (info == null)
        {
            DontDestroyOnLoad(gameObject);
            info = this;
        }
        else if (info != this)
        {
            Destroy(gameObject);
        }

        if (localeSelected != null)
        {
            Debug.Log(localeSelected);
           // LocalizationSettings.SelectedLocale = localeSelected;
        }
        
    }
}