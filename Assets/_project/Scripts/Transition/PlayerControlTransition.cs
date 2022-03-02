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
        bossArena = GameObject.Find("--PlayerSpawn--").transform;
    }
    private void Start()
    {
        playertransform = Movement.Instance.transform;
    }

    public void TeleportToBoss()
    {
        playertransform.position = bossArena.position;
        playertransform.rotation = bossArena.rotation;
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
