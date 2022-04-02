using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private EventSystem eventSystem;


    #region PauseEventSystemTime

    /*  /// <summary>
      /// Search Event System and assign to local variable
      /// Sets first selected game object variable to first button (Continue game)
      /// Scale time to 0
      /// </summary>
      void InitializeFirstSelectedPause()
      {
          eventSystem = FindObjectOfType<EventSystem>();
          GameObject buttonContinue = GameObject.Find("ContinuePause");
          eventSystem.firstSelectedGameObject = buttonContinue;
          eventSystem.SetSelectedGameObject(buttonContinue);
          //nulleamos because Unity
          eventSystem.firstSelectedGameObject = null;
          eventSystem.SetSelectedGameObject(null);
          
          buttonContinue.GetComponent<Button>().Select();
          eventSystem.firstSelectedGameObject = buttonContinue;
          eventSystem.SetSelectedGameObject(buttonContinue);
          Time.timeScale = 0f;
      }
  */

    /// <summary>
    /// Sets first selected from event system to null
    /// Destroy menu
    /// Scales time to 1 again
    /// </summary>
    void StopPause()
    {
        eventSystem.firstSelectedGameObject = null;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    #endregion

    #region PauseButtons

    /// <summary>
    /// Calls stop pause
    /// </summary>
    public void ResumeGame() => StopPause();


    /// <summary>
    /// FUTURE IMPLEMENTATION
    /// </summary>
    public void OptionsMenu()
    {
        Debug.Log("Called to options");
    }

    /// <summary>
    /// For now, will quit game
    /// In future maybe needs to call, save game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion
}