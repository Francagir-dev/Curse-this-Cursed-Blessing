using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject mainMenuPanel;
    public void StartButton()
    {
        Transition.Instance.Do("Exploracion1");
    }

    public void Options()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
}