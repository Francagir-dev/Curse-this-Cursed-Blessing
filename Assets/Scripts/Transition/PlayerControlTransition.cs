using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerControlTransition : MonoBehaviour
{
    //TEMPORAL
    Transition transition;
    Transform playertransform;
    Transform bossArena;
    Vector3 lastPosition;

    public UnityEvent onTouch;

    // Start is called before the first frame update
    private void Awake()
    {
        //TEMPORAL
        transition = FindObjectOfType<Transition>();
        playertransform = Movement.instance.transform;
        bossArena = GameObject.Find("--PlayerSpawn--").transform;
    }

    public void SavePosition()
    {
        lastPosition = playertransform.position;
    }

    public void BackToExplore()
    {
        playertransform.position = lastPosition;
    }

    public void SceneChange()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transition.On();
            transition.onTransition = onTouch;
        }
    }
}
