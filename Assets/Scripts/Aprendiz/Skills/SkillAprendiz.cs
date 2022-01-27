using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAprendiz : MonoBehaviour, SkillInterface
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected string skillName;
    [SerializeField] protected SkillAprendiz skillAprendiz; 
    [SerializeField] protected float damageSkill;
    private float prevSpeed;
    private Enemy enemy;
    public SkillAprendiz(Animator animator, string skillName)
    {
        _animator = animator;
        this.skillName = skillName;
    }

   
    public IEnumerator EndAttack(float delay, string nameSkill, SkillAprendiz skillAprendiz)
    {
        yield return new WaitForSeconds(delay);
        _animator.SetBool(nameSkill, false);
        skillAprendiz.enabled = false;
        enemy.State = Enemy.States.Moving;
    }

    private void OnEnable()
    {
        skillAprendiz = this;
        _animator = transform.GetChild(0).GetComponent<Animator>();
        Animate(skillName, skillAprendiz);
    }

    private void OnDisable()
    {
        GetComponentInParent<Enemy>().SpeedMovement = 1200f;
    }

    public void Animate(string nameSkill, SkillAprendiz skillAprendiz)
    {
        //if we use a Bool to decide who is the correct animation in the animation's tree (Maybe change)
        _animator.SetBool(nameSkill, true);
        StartCoroutine(EndAttack(_animator.GetCurrentAnimatorStateInfo(0).length, nameSkill, skillAprendiz));
    }
}