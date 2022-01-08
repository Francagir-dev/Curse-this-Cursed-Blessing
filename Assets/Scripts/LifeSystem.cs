using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeSystem:MonoBehaviour
{
    public int life;
    public UnityEvent onHeal;
    public UnityEvent onDamage;

    //Recibe daño
    public void Damage(int damage)
    {
        life -= damage;
        onDamage.Invoke();
    }

    //Cura para ambos casos pido lo que se quita por si un enemigo o el jugador quita o cura diferente
    public void Heal(int heal)
    {
        life += heal;
        onHeal.Invoke();
    }
}
