using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public interface EnemyInterface
{
    /// <summary>
    /// Movement of enemy through the NavMesh
    /// </summary>
    /// <param name="navigation">Component indicates will move through mesh</param>
    /// <param name="playerTransform">Position where Enemy will move</param>
    /// <param name="speed">Movement Speed (Will be multiplied by Time.deltaTime)</param>
    void Movement(NavMeshAgent navigation, Transform playerMove, float speed);
    bool IsDeath(int health);
    string RandomizeSkill();
    void ThrowSkill(string skillName);
    void ReceiveDamage(int damageReceived);
    IEnumerator CastSkill();
    void ChangeStates();
}
