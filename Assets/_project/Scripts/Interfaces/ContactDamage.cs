using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ContactDamage : MonoBehaviour, ISkillDamage
{
    [SerializeField] int damage = 1;
    [SerializeField] UnityEvent onDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            MakeDamage(Movement.Instance.LifeSystem);
    }

    public void MakeDamage(LifeSystem life)
    {
        life.Damage(damage);
        onDamage.Invoke();
    }
}
