using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorAxe : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    IEnumerator EndAttack(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        _animator.SetBool("FloorAxe", false);
        GetComponent<FloorAxe>().enabled = false;
    }

    private void Awake()
    {
        _animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        //TestFuncion
        //Animate();
    }

    public void Animate()
    {
        //if we use a Bool to decide who is the correct animation in the animation's tree
        _animator.SetBool("FloorAxe", true);
        StartCoroutine(EndAttack(_animator.GetCurrentAnimatorStateInfo(0).length));
    }
}
