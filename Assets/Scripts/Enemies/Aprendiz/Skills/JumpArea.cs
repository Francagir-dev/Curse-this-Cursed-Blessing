using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpArea : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] [Range(0f, 50f)] private float upForce;
    [SerializeField] [Range(0, 10f)] private float fallCooldown;

    private void Awake()
    {
        Debug.Log("alo");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        rb.useGravity = true;
        rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
        StartCoroutine(Fall());
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallCooldown);
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
        rb.useGravity = false;
        enabled = false;
    }
}
