using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class AINew 
    {
        public static AINew RandomSkillAndTarget = new AINew(SkillSelectTypes.Random, TargetSelectTypes.Random);

        public SkillSelectTypes SkillSelectType { get; private set; }
        public TargetSelectTypes TargetSelectType { get; private set; }


        public AINew(SkillSelectTypes skillSelectType, TargetSelectTypes targetSelectType)
        {
            SkillSelectType = skillSelectType;
            TargetSelectType = targetSelectType;
        }

        public AINew() 
            : this(SkillSelectTypes.Random, TargetSelectTypes.Random) { }

        public SkillNew ChooseSkill(EnemyNew enemy, EncounterNew encounter)
        {
            switch (SkillSelectType)
            {
                case SkillSelectTypes.Random:
                    return Calc.ChooseRandom(enemy.Skills.UsableSkills);
                default:
                    return null;
            }
        }

        public List<ActorNew> ChooseTargets(SkillNew skill, EncounterNew encounter)
        {
            List<ActorNew> temp = new List<ActorNew>();
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
