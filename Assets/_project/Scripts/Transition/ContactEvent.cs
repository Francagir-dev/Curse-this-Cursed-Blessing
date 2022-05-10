using UnityEngine;
using UnityEngine.Events;

public class ContactEvent : MonoBehaviour
{
    public UnityEvent onTouch;
    LevelManagment manag;

    protected void Awake()
    {
        manag = FindObjectOfType<LevelManagment>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Transition.Instance.Do(onTouch.Invoke);
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
