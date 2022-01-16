using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class EnemyAI 
    {
        public static EnemyAI RandomSkillAndTarget = new EnemyAI(SkillSelectTypes.Random, TargetSelectTypes.Random);

        public SkillSelectTypes SkillSelectType { get; private set; }
        public TargetSelectTypes TargetSelectType { get; private set; }


        public EnemyAI(SkillSelectTypes skillSelectType, TargetSelectTypes targetSelectType)
        {
            SkillSelectType = skillSelectType;
            TargetSelectType = targetSelectType;
        }

        public EnemyAI() 
            : this(SkillSelectTypes.Random, TargetSelectTypes.Random) { }

        public Skill ChooseSkill(Enemy enemy, Encounter encounter)
        {
            switch (SkillSelectType)
            {
                case SkillSelectTypes.Random:
                    return Calc.ChooseRandom(enemy.Skills.UsableSkills);
                default:
                    return null;
            }
        }

        public List<Actor> ChooseTargets(Skill skill, Encounter encounter)
        {
            List<Actor> temp = new List<Actor>();
            if (skill.HitsMultipleTargets)
            {
                temp.AddRange(encounter.Heroes);
            }
            else
            {
                switch (TargetSelectType)
                {
                    case TargetSelectTypes.Random:
                        temp.Add(encounter.Heroes.RandomLivingActor());
                        break;
                    default:
                        break;
                }
            }
            return temp;
        }

        public enum SkillSelectTypes
        {
            Random
        }
        public enum TargetSelectTypes
        {
            Random
        }




    }
}
