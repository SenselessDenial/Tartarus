using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class ActorOLD
    {
        public string Name { get; protected set; }
        public StatSheetOLD Stats { get; protected set; }
        public SkillSetOLD Skills { get; protected set; }
        public ModifierListOLD Modifiers { get; protected set; }
        public ItemOLD HeldItem { get; private set; }
        public int HeldItemAmount { get; private set; }

        public PartyOLD Party { get; set; }
        
        public int HP
        {
            get => Stats.HP.Value;
            set => Stats.HP.Value = value;
        }
        public int MP
        {
            get => Stats.MP.Value;
            set => Stats.MP.Value = value;
        }
        public int MaxHP
        {
            get => Stats.HP.MaxValue;
            set => Stats.HP.MaxValue = value;
        }
        public int MaxMP
        {
            get => Stats.MP.MaxValue;
            set => Stats.MP.MaxValue = value;
        }
        public int Strength => Stats.Strength.Value;
        public int Magic => Stats.Magic.Value;
        public int Endurance => Stats.Endurance.Value;
        public int Resilience => Stats.Resilience.Value;
        public int Speed => Stats.Speed.Value;

        public int Level => Stats.Level;
        public int XP
        {
            get => Stats.XP;
            set => Stats.XP = value;
        }
        public int XPToNext => Stats.XPToNext;
        public bool CanLevelUp => Stats.CanLevelUp;
        public bool IsDead => HP <= 0;
        public string Card => Name + "\nHP: " + HP + "/" + MaxHP + "\nMP: " + MP + "/" + MaxMP;

        protected ActorOLD()
        {
            Name = "UNCREATED_ACTOR";
            Stats = null;
            Skills = null;
            Modifiers = null;
        }

        private void AssembleActor(ActorOLD actor, string name, StatSheetOLD stats, SkillSetOLD skills, ModifierListOLD modifiers)
        {
            actor.Name = name;
            actor.Stats = stats;
            actor.Skills = skills;
            actor.Modifiers = modifiers;
        }

        public ActorOLD(string name)
        {
            Name = name;
            Stats = new StatSheetOLD(this);
            Skills = new SkillSetOLD(this);
            Modifiers = new ModifierListOLD(this);
        }

        public ActorOLD(string name, 
                     int str, int mag, int end, int res, int spd, 
                     int strWeight, int magWeight, int endWeight, int resWeight, int spdWeight)
        {
            Name = name;
            Stats = new StatSheetOLD(this, str, mag, end, res, spd, strWeight, magWeight, endWeight, resWeight, spdWeight);
            Skills = new SkillSetOLD(this);
            Modifiers = new ModifierListOLD(this);
        }

        public ActorOLD(string name,
                     int str, int mag, int end, int res, int spd)
            : this(name, str, mag, end, res, spd, 20, 20, 20, 20, 20) { }

        public void LevelUp()
        {
            Stats.LevelUp();
        }

        public void LevelUp(int iterations)
        {
            for (int i = 0; i < iterations; i++)
                LevelUp();
        }

        public bool UseSkill(SkillOLD skill, ActorOLD target)
        {
            if (skill.HitsMultipleTargets && target.Party != null)
                return UseSkill(skill, target.Party);

            if (!HasSkill(skill))
                Logger.Log(Name + " does not have the skill " + skill.Name + ".");
            else if (!skill.IsUsableBy(this))
                Logger.Log(Name + " does not have enough HP or MP to use the skill " + skill.Name + ".");
            else if (!skill.IsUseableOn(target))
                Logger.Log(skill.Name + " cannot be used on " + target.Name + ".");
            else 
            {
                int hpCost = (int)(skill.HPCost * MaxHP / 100f);
                Logger.Log(Name + " has " + HP + " HP & " + MP + " MP.");
                HP -= hpCost;
                MP -= skill.MPCost;
                Logger.Log("The costs are " + hpCost + " HP & " + skill.MPCost + " MP.");
                Logger.Log(Name + " now has " + HP + " HP & " + MP + " MP.");

                if (skill.CalculateHit(this, target))
                {
                    int power = skill.Calculate(this, target);
                    Logger.Log(Name + " has used the skill " + skill.Name + " on " + target.Name + ".");
                    switch (skill.Type)
                    {
                        case SkillTypes.Damage:
                            target.TakeDamage(power);
                            break;
                        case SkillTypes.Healing:
                        case SkillTypes.Reviving:
                            target.TakeDamage(-power);
                            break;
                        default:
                            Logger.Log("Skill type not implemented.");
                            return false;
                    }
                }
                else
                    Logger.Log(Name + " has missed the attack on " + target.Name + ".");
                return true;
            }

            return false;
        }

        public bool UseSkill(SkillOLD skill, PartyOLD party)
        {
            if (!HasSkill(skill))
                Logger.Log(Name + " does not have the skill " + skill.Name + ".");
            else if (!skill.IsUsableBy(this))
                Logger.Log(Name + " does not have enough HP or MP to use the skill " + skill.Name + ".");
            else if (!skill.IsUseableOn(party))
                Logger.Log(skill.Name + " cannot be used on the given party.");
            else
            {
                int hpCost = (int)(skill.HPCost * MaxHP / 100f);
                Logger.Log(Name + " has " + HP + " HP & " + MP + " MP.");
                HP -= hpCost;
                MP -= skill.MPCost;
                Logger.Log("The costs are " + hpCost + " HP & " + skill.MPCost + " MP.");
                Logger.Log(Name + " now has " + HP + " HP & " + MP + " MP.");

                Logger.Log(Name + " has used the skill " + skill.Name + " on " + party.FirstLivingActor.Name + "'s party.");
                foreach (var target in party)
                {
                    if (!skill.IsUseableOn(target))
                        continue;

                    if (skill.CalculateHit(this, target))
                    {
                        int power = skill.Calculate(this, target);
                        
                        switch (skill.Type)
                        {
                            case SkillTypes.Damage:
                                target.TakeDamage(power);
                                break;
                            case SkillTypes.Healing:
                            case SkillTypes.Reviving:
                                target.TakeDamage(-power);
                                break;
                            default:
                                Logger.Log("Skill type not implemented.");
                                return false;
                        }
                    }
                    else
                        Logger.Log(Name + " has missed the attack on " + target.Name + ".");
                }

                return true;
            }

            return false;
        }

        public bool HasSkill(SkillOLD skill)
        {
            return Skills.Contains(skill);
        }

        public void ReceiveXP(int xp)
        {
            XP += xp;
            Logger.Log(Name + " has received " + xp + " XP.");
            while (CanLevelUp)
                LevelUp();
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;

            if (damage >= 0)
                Logger.Log(Name + " has taken " + damage + " damage.");
            else
                Logger.Log(Name + " has healed " + (damage * -1) + " HP.");
            if (IsDead)
                Logger.Log(Name + " is dead!");
        }

        public void AddSkill(SkillOLD skill)
        {
            Skills.Add(skill);
        }

        public void AddInnateResistance(Elements element, Resistances resistance)
        {
            Modifiers.AddInnateResistance(element, resistance);
        }

        public void AddInnateOffensiveBonus(Elements element, OffensiveBonuses bonus)
        {
            Modifiers.AddInnateOffensiveBonus(element, bonus);
        }

        public int CalculateXPDrop()
        {
            double a = -0.1 * Modifiers.NumOfWeaknesses;
            double b = 0.5 * Modifiers.NumOfResistances;
            double c = 0.005 * (Endurance + Resilience);

            return (int)(10 * Level * (1 + a + b + c));
        }

        public ActorOLD Copy()
        {
            ActorOLD temp = new ActorOLD();
            AssembleActor(temp, Name, Stats.Copy(temp), Skills.Copy(temp), Modifiers.Copy(temp));
            return temp;
        }
        
    }

    public enum Resistances
    {
        None = 0,
        Weak = -50,
        Strong = 50
    }


}