using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private EventSystem eventSystem;
    private Movement _movement;
   [SerializeField] private GameObject optionsMenu;
   [SerializeField] private GameObject pauseMenu;
    #region PauseventSystemTime

    private void Awake()
    {
        InitializeFirstSelectedPause();
        _movement = FindObjectOfType<Movement>();
        if (_movement != null) _movement.isPaused = true;
    }

    private void OnEnable()
    {
        InitializeFirstSelectedPause();
        if (_movement != null) _movement.isPaused = true;
    }

    private void OnDestroy()
    {
        if (_movement != null) _movement.isPaused = false;
    }

   private void OnDisable()
    {
        if (_movement != null) _movement.isPaused = false;
    }

    /// <summary>
    /// Search Event System and assign to local variable
    /// Sets first selected game object variable to first button (Continue game)
    /// Scale time to 0
    /// </summary>
    void InitializeFirstSelectedPause()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        StartCoroutine(ChangeEventSystem(GameObject.Find("ContinuePause")));

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
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
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