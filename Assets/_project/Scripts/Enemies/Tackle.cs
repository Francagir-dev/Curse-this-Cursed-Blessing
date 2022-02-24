using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackle : MonoBehaviour
{
    [SerializeField] float timeToCharge;
    [SerializeField] float timeStop;
    [SerializeField] float timeForward;
    [SerializeField] float speed;
    bool go = false;
    Enemy enemy;

    private void Awake()
    {
        enemy = FindObjectOfType<Enemy>();
    }

    private void OnEnable()
    {
        StartCoroutine(Charge());
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

    void Stop()
    {
        go = false;
        StopAllCoroutines();
        StartCoroutine(Wait());
        IEnumerator Wait()
        {
            enemy.Animator.SetTrigger("EndSpin");

            yield return new WaitForSeconds(.5f);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!go) return;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<LifeSystem>().Damage(1);
        }
        Stop();
    }
}
