using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    class Actor
    {
        public string Name { get; private set; }
        public StatSheet Stats { get; private set; }
        public SkillSet Skills { get; private set; }
        public ModifierList Modifiers { get; private set; }
        public Party Party { get; set; }
        public Item HeldItem { get; private set; }
        public int HeldItemAmount { get; private set; }
        public int Level { get; private set; }
        public int XP;
        public int XPToNext => XPFormula(Level);

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

        public bool CanLevelUp => XP >= XPToNext;
        public bool IsDead => HP <= 0;

        public string Card => Name + "\nHP: " + HP + "/" + MaxHP + "\nMP: " + MP + "/" + MaxMP;

        private static int MaxLevel => 100;

        public Actor(string name)
        {
            Name = name;
            Stats = StatSheet.Default;
            Skills = new SkillSet(this);
            Modifiers = new ModifierList();
            Party = null;
            Level = 1;
            XP = 0;

            MaxHP = CalculateMaxHP();
            MaxMP = CalculateMaxMP();
        }

        public void LevelUp()
        {
            if (Level >= MaxLevel)
            {
                Logger.Log(Name + " is already at max level of " + MaxLevel + ". Cannot level up.");
                return;
            }

            if (CanLevelUp)
                XP -= XPFormula(Level);
            Level += 1;
            
            for (var i = 0; i < 3; i++)
                Stats.IncrementRandom();

            MaxHP = CalculateMaxHP();
            MaxMP = CalculateMaxMP();
        }

        public void LevelUp(int iterations)
        {
            for (int i = 0; i < iterations; i++)
                LevelUp();
        }

        public bool UseSkill(Skill skill, Actor target)
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

        public bool UseSkill(Skill skill, Party party)
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

        public bool HasSkill(Skill skill)
        {
            return Skills.Contains(skill);
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            HP = MathHelper.Clamp(HP, 0, MaxHP);

            if (damage >= 0)
                Logger.Log(Name + " has taken " + damage + " damage.");
            else
                Logger.Log(Name + " has healed " + (damage * -1) + " HP.");
            if (IsDead)
                Logger.Log(Name + " is dead!");
        }

        private int CalculateMaxHP()
        {
            double a = 50;
            double b = (Math.Pow(Endurance, 0.7) / 4) * Level;
            double c = 5 * Level;

            return (int)(a + b + c);
        }

        private int CalculateMaxMP()
        {
            double a = 20;
            double b = (Math.Pow(Magic, 0.5) / 4) * Level;
            double c = 3 * Level;

            return (int)(a + b + c);
        }

        public void AddSkill(Skill skill)
        {
            Skills.Add(skill);
        }

        private Func<int, int> XPFormula = (int level) => 100 * level * (level + 1);
    }

    public enum Resistances
    {
        None = 0,
        Weak = -50,
        Strong = 50
    }


}