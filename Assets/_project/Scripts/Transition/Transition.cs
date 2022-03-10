using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Transition : MonoBehaviour
{
    static Transition instance;
    public static Transition Instance { get => instance; private set => instance = value; }

    Animator anim;
    float closeDuration;

    private void Awake()
    {
        if (instance == null)
            Instance = this;
        else Destroy(gameObject);

        anim = GetComponent<Animator>();
        closeDuration = anim.GetCurrentAnimatorStateInfo(0).length;
    }

    public void Do(UnityAction actions)
    {
        StartCoroutine(Wait());
        IEnumerator Wait()
        {
            anim.SetBool("Close", true);
            yield return new WaitForSeconds(closeDuration);
            actions.Invoke();
            anim.SetBool("Close", false);
        }
    }

    public void Do(string sceneName)
    {
        StartCoroutine(Wait());
        IEnumerator Wait()
        {
            anim.SetBool("Close", true);
            yield return new WaitForSeconds(closeDuration);
            AsyncOperation operation = 
                SceneManager.LoadSceneAsync(SceneManager.GetSceneByName(sceneName).buildIndex, LoadSceneMode.Single);
            yield return new WaitUntil(() => operation.isDone);
            anim.SetBool("Close", false);
        }
    }
}
