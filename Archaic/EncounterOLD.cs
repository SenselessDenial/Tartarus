using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class EncounterOLD
    {
        public PartyOLD Alpha { get; private set; }
        public PartyOLD Beta { get; private set; }
        public PartyOLD ActiveParty { get; private set; }
        public PartyOLD InactiveParty => (ActiveParty == Alpha) ? Beta : Alpha;
        public ActorOLD CurrentActor { get; private set; }

        public bool IsOver => Alpha.IsAllDead || Beta.IsAllDead;

        public EncounterOLD(PartyOLD alpha, PartyOLD beta)
        {
            Alpha = alpha;
            Beta = beta;
            ActiveParty = alpha;
            CurrentActor = Alpha.FirstLivingActor;
        }

        public void Next()
        {
            if (!IsOver)
            {
                if (ActiveParty == Alpha)
                {
                    if (CurrentActor == Alpha.LastLivingActor)
                    {
                        ActiveParty = Beta;
                        CurrentActor = Beta.FirstLivingActor;
                    }
                    else
                        CurrentActor = Alpha.NextLivingActor(CurrentActor);
                }
                else if (ActiveParty == Beta)
                {
                    if (CurrentActor == Beta.LastLivingActor)
                    {
                        ActiveParty = Alpha;
                        CurrentActor = Alpha.FirstLivingActor;
                    }
                    else
                        CurrentActor = Beta.NextLivingActor(CurrentActor);
                }
            }
            else
                Logger.Log("Encounter is over! There is no need to cycle.");
        }

        public bool UseSkill(SkillOLD skill, ActorOLD user, ActorOLD target)
        {
            if (IsOver)
                Logger.Log("Encounter is already over! Skill not used.");
            else
            {
                if (Contains(user) && Contains(target) && CurrentActor == user)
                {
                    bool success = user.UseSkill(skill, target);
                    if (success)
                    {
                        Next();
                        return true;
                    }
                    else
                        Logger.Log("Encounter did not proceed because skill failed.");
                }
                else
                {
                    if (CurrentActor != user)
                        Logger.Log("User is not current actor.");
                    else
                        Logger.Log("Encounter does not contain either the user or target.");
                }
            }
            return false;
        }

        public bool Contains(ActorOLD actor)
        {
            return Alpha.Contains(actor) || Beta.Contains(actor);
        }

        public void GiveXPToParty(bool isAlpha)
        {
            if (isAlpha)
                Alpha.ReceiveXP(Beta);
            else
                Beta.ReceiveXP(Alpha);
        }


    }
}