using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControlTransition : MonoBehaviour
{
    //TEMPORAL
    [SerializeField] private Transition transition;
    [SerializeField] private Transform playertransform;
    [SerializeField] private Transform lastPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        //TEMPORAL
        transition = FindObjectOfType<Transition>();
        playertransform = GetComponent<Transform>();
    }

    public void EsbirroCombat()
    {
        transition.ToOnToOff();
    }

    public void SavePosition()
    {
        lastPosition = playertransform;
        EsbirroCombat();
    }

    public void BackToExplore()
    {
        playertransform = lastPosition;
        EsbirroCombat();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChanceScene"))
        {
            transition.On();
            //TEMPORAL
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
