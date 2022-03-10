using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeSystem: MonoBehaviour
{
    [SerializeField] int life;
    public int Life { get => life; protected set => life = value; }
    bool inv = false;
    public bool Inv { private get => inv; set => inv = value; }

    public UnityEvent onHeal;
    public UnityEvent onDamage;

    /// <summary>
    /// Recibe daño
    /// </summary>
    /// <param name="damage">Daño que recibe</param>
    public void Damage(int damage)
    {
        if (Inv) return;

        life -= damage;
        onDamage.Invoke();
    }

    /// <summary>
    /// Cura para ambos casos pido lo que se quita por si un enemigo o el jugador quita o cura diferente
    /// </summary>
    /// <param name="heal">Cantidad de vida para curar</param>
    public void Heal(int heal)
    {
        life += heal;
        onHeal.Invoke();
    }
}
