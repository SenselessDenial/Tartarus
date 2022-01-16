using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class Encounter
    {
        public HeroParty Heroes { get; private set; }
        public EnemyParty Enemies { get; private set; }

        public Actor CurrentActor => IsHeroesTurn ? Heroes[CurrentIndex] : (Actor)Enemies[CurrentIndex];
        public Hero CurrentHero => IsHeroesTurn ? Heroes[CurrentIndex] : null;
        public Enemy CurrentEnemy => !IsHeroesTurn ? Enemies[CurrentIndex] : null;

        public bool IsHeroesTurn { get; private set; }
        private int CurrentIndex;


        public bool IsOver => Heroes.IsAllDead || Enemies.IsAllDead;
        public bool HeroesWin => Enemies.IsAllDead;
        public bool EnemiesWin => Heroes.IsAllDead;

        public Encounter(HeroParty heroes, EnemyParty enemies, bool heroesGoFirst)
        {
            Heroes = heroes;
            Enemies = enemies;
            IsHeroesTurn = heroesGoFirst;
            CurrentIndex = heroesGoFirst ? Heroes.IndexOfFirstLivingActor : Enemies.IndexOfFirstLivingActor;
        }

        private void UseSkill(Skill skill, int indexOfTarget)
        {
            if (IsOver)
            {
                Logger.Log("Encounter is over. Cannot proceed.");
                return;
            }

            Actor user = CurrentActor;
            Actor target;

            if (IsHeroesTurn && indexOfTarget >= 0 && indexOfTarget < Heroes.Count)
                target = skill.Type == SkillTypes.Damage ? Enemies[indexOfTarget] : (Actor)Heroes[indexOfTarget];
            else if (!IsHeroesTurn && indexOfTarget >= 0 && indexOfTarget < Enemies.Count)
                target = skill.Type == SkillTypes.Damage ? Heroes[indexOfTarget] : (Actor)Enemies[indexOfTarget];
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
        }

        public void UseSkillAndProceed(Skill skill, IEnumerable<Actor> targets)
        {
            if (IsOver)
            {
                Logger.Log("Encounter is over. Cannot proceed.");
                return;
            }

            Actor user = CurrentActor;

            if (user == null || targets == null)
            {
                Logger.Log("Current actor or targets is null. Cannot proceed in encounter.");
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

            user.TakeCosts(skill);

            foreach (var target in targets)
            {
                if (!skill.IsUseableOn(target))
                {
                    Logger.Log(target.Name + " cannot be affected by the skill " + skill.Name);
                    return;
                }
                else
                {
                    UseSkill(skill, target);
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

        public void UseSkillAndProceed(Skill skill, params Actor[] targets)
        {
            UseSkillAndProceed(skill, (IEnumerable<Actor>)targets);
        }

        private void UseSkill(Skill skill, Actor target)
        {
            if (Heroes.Contains(target))
                UseSkill(skill, (Hero)target);
            else if (Enemies.Contains(target))
                UseSkill(skill, (Enemy)target);
        }
        private void UseSkill(Skill skill, Hero target)
        {
            if (IsOver)
            {
                Logger.Log("Encounter is over. Cannot proceed.");
                return;
            }

            var user = CurrentActor;

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
        private void UseSkill(Skill skill, Enemy target)
        {
            if (IsOver)
            {
                Logger.Log("Encounter is over. Cannot proceed.");
                return;
            }

            var user = CurrentActor;

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
