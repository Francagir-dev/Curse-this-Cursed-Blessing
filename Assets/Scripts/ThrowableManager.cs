using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableManager : MonoBehaviour
{
    public List<GameObject> throwables;
    public float maxTime;
    int index;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        index = 0;
        throwables.ForEach(x => x.SetActive(false));
    }

    public void OnEnable()
    {
        throwables[index].transform.SetPositionAndRotation(transform.position, transform.rotation);
        throwables[index].SetActive(true);
        StartCoroutine(Timer(throwables[index]));
        index++;

        if (index >= throwables.Count)
            index = 0;
    }

    IEnumerator Timer(GameObject obj)
    {
        yield return new WaitForSeconds(maxTime);
        obj.SetActive(false);
    }
}
