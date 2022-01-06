using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class EncounterNew
    {
        public HeroParty Heroes { get; private set; }
        public EnemyParty Enemies { get; private set; }
        public ActorNew CurrentActor
        {
            get
            {
                if (IsHeroesTurn)
                    return Heroes[CurrentIndex];
                else
                    return Enemies[CurrentIndex];
            }
        }

        public bool IsHeroesTurn { get; private set; }
        private int CurrentIndex;


        public bool IsOver => Heroes.IsAllDead || Enemies.IsAllDead;
        public bool HeroesWin => Enemies.IsAllDead;
        public bool EnemiesWin => Heroes.IsAllDead;

        public EncounterNew(HeroParty heroes, EnemyParty enemies, bool heroesGoFirst)
        {
            Heroes = heroes;
            Enemies = enemies;
            IsHeroesTurn = heroesGoFirst;
            CurrentIndex = heroesGoFirst ? Heroes.IndexOfFirstLivingActor : Enemies.IndexOfFirstLivingActor;
        }

        public void UseSkill(SkillNew skill, int indexOfTarget)
        {
            if (IsOver)
            {
                Logger.Log("Encounter is over. Cannot proceed.");
                return;
            }


            ActorNew user = CurrentActor;
            ActorNew target;

            if (IsHeroesTurn && indexOfTarget >= 0 && indexOfTarget < Heroes.Count)
                target = skill.Type == SkillTypes.Damage ? Enemies[indexOfTarget] : (ActorNew)Heroes[indexOfTarget];
            else if (!IsHeroesTurn && indexOfTarget >= 0 && indexOfTarget < Enemies.Count)
                target = skill.Type == SkillTypes.Damage ? Heroes[indexOfTarget] : (ActorNew)Enemies[indexOfTarget];
            else
            {
                Logger.Log("Index is out of range. Cannot proceed in encounter.");
                return;
            }

            if (user == null || target == null)
            {
                Logger.Log("Current actor or selected target is null. Cannot proceed in encounter.");
                return;
            }

            if (!user.ContainsSkill(skill))
            {
                Logger.Log(user.Name + " does not contain the skill " + skill.Name);
                return;
            }
            else if (!skill.IsUsableBy(user))
            {
                Logger.Log(user.Name + " cannot use the skill " + skill.Name);
                return;
            }
            else if (!skill.IsUseableOn(target))
            {
                Logger.Log(target.Name + " cannot be affected by the skill " + skill.Name);
                return;
            }
            else
            {
                Logger.Log(user.Name + " has " + user.HP + " HP & " + user.MP + " MP.");
                user.TakeCosts(skill);
                Logger.Log(user.Name + " now has " + user.HP + " HP & " + user.MP + " MP.");
                if (skill.CalculateHit(user, target))
                {
                    Logger.Log(target.Name + " has " + target.HP + " HP & " + target.MP + " MP.");
                    target.TakeDamage(skill.Calculate(user, target));
                    Logger.Log(target.Name + " now has " + target.HP + " HP & " + target.MP + " MP.");
                }
                else
                {
                    Logger.Log(user.Name + "'s attack on " + target.Name + " has missed.");
                }
            }

            if (IsOver)
            {
                Logger.Log("Encounter is over!");
                EndOfEncounter();
            }
            else
            {
                CycleCurrentActor();
            }
        }

        private void CycleCurrentActor()
        {
            if (IsHeroesTurn)
            {
                if (CurrentIndex == Heroes.IndexOfLastLivingActor)
                {
                    IsHeroesTurn = false;
                    CurrentIndex = Enemies.IndexOfFirstLivingActor;
                }
                else
                {
                    CurrentIndex = Heroes.IndexOfNextLivingActor(CurrentIndex);
                }
            }
            else
            {
                if (CurrentIndex == Enemies.IndexOfLastLivingActor)
                {
                    IsHeroesTurn = true;
                    CurrentIndex = Heroes.IndexOfFirstLivingActor;
                }
                else
                {
                    CurrentIndex = Enemies.IndexOfNextLivingActor(CurrentIndex);
                }
            }
        }
        private void EndOfEncounter()
        {
            if (HeroesWin)
            {
                Heroes.ReceiveXP(Enemies.XPDrop);
                Heroes.ReceiveMoney(Enemies.MoneyDrop);
            }
        }


    }
}
