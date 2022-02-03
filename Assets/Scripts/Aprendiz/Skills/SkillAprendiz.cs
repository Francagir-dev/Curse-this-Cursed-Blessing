using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAprendiz : MonoBehaviour, SkillInterface
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected string skillName;
    [SerializeField] protected SkillAprendiz skillAprendiz; 
    [SerializeField] protected float damageSkill;
    [SerializeField] protected Aprendiz _aprendiz;
    private float prevSpeed;
    public SkillAprendiz(Animator animator, string skillName)
    {
        _animator = animator;
        this.skillName = skillName;
    }

   
    public IEnumerator EndAttack(float delay, string nameSkill, SkillAprendiz skillAprendiz)
    {
        yield return new WaitForSeconds(delay);
        _animator.SetBool(nameSkill, false);
        enabled = false;
        GetComponentInParent<Aprendiz>().IsCastingSkill = false;
    }

    private void OnEnable()
    {
        skillAprendiz = this;
        skillAprendiz.GetComponentInParent<Enemy>().SpeedMovement = 0;
        _animator = transform.GetChild(0).GetComponent<Animator>();
        Animate(skillName, skillAprendiz);
    }

    private void OnDisable()
    {
        _aprendiz.SpeedMovement = 1200f;
        _aprendiz.IsCastingSkill = false;
    }


    public void Animate(string nameSkill, SkillAprendiz skillAprendiz)
    {
        //if we use a Bool to decide who is the correct animation in the animation's tree
        _animator.SetBool(nameSkill, true);
        StartCoroutine(EndAttack(_animator.GetCurrentAnimatorStateInfo(0).length, nameSkill, skillAprendiz));
    }
}