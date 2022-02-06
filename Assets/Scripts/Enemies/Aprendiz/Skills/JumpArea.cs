using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JumpArea : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] [Range(0f, 9001f)] private float upForce;

    [SerializeField] [Range(0, 10f)] private float fallCooldown;
    //[SerializeField] private NavMeshAgent agent;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // agent.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX |
                         RigidbodyConstraints.FreezePositionZ;
        rb.useGravity = true;
        rb.AddForce(Vector3.forward + Vector3.up);
        StartCoroutine(Fall());
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallCooldown);
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
        rb.useGravity = false;
        // agent.enabled = true;
        enabled = false;
    }
}