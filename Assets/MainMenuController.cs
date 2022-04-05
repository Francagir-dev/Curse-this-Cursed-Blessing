using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartButton()
    {
        Transition.Instance.Do("Exploracion1");
    }

    public void Options()
    {
        Debug.Log("Opcionse no se nada de ninguna opción");
    }
    public void Quit()
    {
        Application.Quit();
    }
}