using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleWalkCutscene : MonoBehaviour
{
    Animator anim;
    Vector3 prevPosition;
    [SerializeField] float adjustment = 1;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        prevPosition = transform.position;
    }

    private void Update()
    {
        anim.SetFloat("Speed", Vector3.Distance(transform.position, prevPosition) * adjustment);
        prevPosition = transform.position;
    }
}
