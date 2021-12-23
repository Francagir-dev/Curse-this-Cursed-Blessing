using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeSystem:MonoBehaviour
{
    public int life;
    public UnityEvent onHeal;
    public UnityEvent onDamage;

    public void Damage(int damage)
    {
        life -= damage;
        onDamage.Invoke();
    }

    public void Heal(int heal)
    {
        life += heal;
        onHeal.Invoke();
    }
}
