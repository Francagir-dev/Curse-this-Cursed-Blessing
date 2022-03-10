using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackle : MonoBehaviour
{
    [SerializeField] GameObject damageColl;
    [SerializeField] float timeToCharge;
    [SerializeField] float timeStop;
    [SerializeField] float timeForward;
    [SerializeField] float speed;
    bool go = false;
    bool returning = false;
    Enemy enemy;

    private void Awake()
    {
        damageColl.SetActive(false);
        enemy = GetComponent<Enemy>();
    }

    public void BeginCharge()
    {
        if (returning) { returning = false; return; }
        damageColl.SetActive(true);
        StartCoroutine(Charge());
        returning = true;
    }

    IEnumerator Charge()
    {
        enemy.CanRotate = true;
        yield return new WaitForSeconds(timeToCharge);
        enemy.CanRotate = false;
        yield return new WaitForSeconds(timeStop);
        float time = timeForward;
        while (time >= 0)
        {
            go = true;
            time -= Time.deltaTime;
            enemy.transform.position += enemy.transform.forward * speed;
            yield return null;
        }
        Stop();
    }

    public void Stop()
    {
        damageColl.SetActive(false);
        go = false;
        StopAllCoroutines();
        enemy.Animator.SetTrigger("EndTackle");
    }
}
