
    using System.Collections;

    public interface SkillInterface
    {
        IEnumerator EndAttack(float delay, string nameSkill, SkillAprendiz skillAprendiz);
        void Animate(string nameSkill, SkillAprendiz skillAprendiz);

    }
