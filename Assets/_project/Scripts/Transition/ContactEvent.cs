using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ContactEvent : MonoBehaviour
{
    public UnityEvent onTouch;
    public UnityEvent onInstantTouch;
    LevelManagment manag;
    [SerializeField] float actionDelay = 0;
    bool stop = false;
    protected void Awake()
    {
        manag = FindObjectOfType<LevelManagment>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !stop)
        {
            onInstantTouch.Invoke();
            stop = true;
            StartCoroutine(Wait());
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(actionDelay);
            UnityEvent eve = new UnityEvent();
            eve.AddListener(onTouch.Invoke);
            eve.AddListener(() => stop = false);
            Transition.Instance.Do(eve.Invoke);
        }
    }

    public void SceneChange(string sceneName)
    {
        Transition.Instance.Do(sceneName);
    }

    public void SectionChange(int section)
    {
        manag.SetActiveSection(section);
    }
}
