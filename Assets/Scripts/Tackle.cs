using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackle : MonoBehaviour
{
    [SerializeField] float timeToCharge;
    [SerializeField] float timeStop;
    [SerializeField] float timeForward;
    [SerializeField] float speed;
    Enemy enemy;
    Rigidbody rb;

    private void Awake()
    {
        enemy = FindObjectOfType<Enemy>();
        rb = enemy.GetComponent<Rigidbody>();
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
            time -= Time.deltaTime;
            rb.velocity = enemy.transform.forward * speed;
            yield return null;
        }

        enemy.Animator.SetTrigger("EndRoad");

        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
