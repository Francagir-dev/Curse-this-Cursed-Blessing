using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private EventSystem eventSystem;

    #region PauseEventSystemTime

    private void Awake()
    {
        InitializeFirstSelectedPause();
        FindObjectOfType<Movement>().isPaused = true;
    }

    private void OnEnable()
    {
        InitializeFirstSelectedPause();
        FindObjectOfType<Movement>().isPaused = true;
    }

    private void OnDestroy()
    {
        FindObjectOfType<Movement>().isPaused = false;
    }

    private void OnDisable()
    {
        FindObjectOfType<Movement>().isPaused = false;
    }

    /// <summary>
      /// Search Event System and assign to local variable
      /// Sets first selected game object variable to first button (Continue game)
      /// Scale time to 0
      /// </summary>
      void InitializeFirstSelectedPause()
      {
          eventSystem = FindObjectOfType<EventSystem>();
          StartCoroutine(ChangeEventSystem( GameObject.Find("ContinuePause")));
          
          Time.timeScale = 0f;
      }

    IEnumerator ChangeEventSystem(GameObject itemSelected)
    {
        eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        eventSystem.SetSelectedGameObject(itemSelected);
    }

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