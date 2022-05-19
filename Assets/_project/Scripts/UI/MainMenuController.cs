using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject mainMenuPanel;
    public GameObject controlPanel;
    public GameObject creditsPanel;

    public void StartButton()
    {
        Transition.Instance.Do("Prologue");
    }

    public void Options()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        controlPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Controls(bool active)
    {
        controlPanel.SetActive(active);
        mainMenuPanel.SetActive(!active);
        optionsPanel.SetActive(false);
    }

    public void Credits()
    {
        controlPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

}