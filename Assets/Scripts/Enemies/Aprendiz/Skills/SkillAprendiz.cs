using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAprendiz : MonoBehaviour, SkillInterface
{
    
   [SerializeField] protected Animator _animator;

   public Animator Animator
   {
       get => _animator;
       set => _animator = value;
   }

   [SerializeField] protected string skillName;
   [SerializeField] private Enemy enemy;
    
    public string SkillName
    {
        get => skillName;
        set => skillName = value;
    }

  
   
    public SkillAprendiz(Animator animator, string skillName)
    {
        _animator = animator;
        this.skillName = skillName;
    }

   
    public IEnumerator EndAttack(float delay, string nameSkill)
    {
        yield return new WaitForSeconds(delay);
        _animator.SetBool(nameSkill, false);
        enemy.ChangeState(Enemy.States.Moving);
        enabled = false;
    }

    private void OnEnable()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        Animate(skillName);
    }

    public void Animate(string nameSkill)
    {
        //if we use a Bool to decide who is the correct animation in the animation's tree (Maybe change)
        _animator.SetBool(nameSkill, true);
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
        AnimationClip trueClip = null;
        foreach (var item in clips)
        {
            if (item.name == nameSkill)
            {
                trueClip = item;
                break;
            }
        }
        StartCoroutine(EndAttack(trueClip.length + .5f, nameSkill));
    }
}