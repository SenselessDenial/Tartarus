using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class SkillNew
    {
        public string Name { get; private set; }
        public SkillTypes Type { get; private set; }
        public Elements Element { get; private set; }
        public Stats UserStat { get; private set; }
        public Targets Target { get; private set; }
        public Stats TargetStat { get; private set; }
        public int HPCost { get; private set; }
        public int MPCost { get; private set; }
        public int Power { get; private set; }
        public int Accuracy { get; private set; }
        public int CritChance { get; private set; }
        public int ArmorTearing { get; private set; }
        public int MinNumberOfHits { get; private set; }
        public int MaxNumberOfHits { get; private set; }
        public StatusEffects StatusEffect { get; private set; }
        public int StatusEffectChance { get; private set; }
        public bool AlwaysHit { get; private set; }
        public bool AlwaysEffect { get; private set; }
        public bool HitsMultipleTargets => Target == Targets.All || Target == Targets.AllyParty || Target == Targets.EnemyParty;

        private static int VARIANCE_AMP_PERCENT = 5;
        private static double DEFENDER_BONUS = 1.5;

        public SkillNew(string name, SkillTypes type,
                    Elements element, Stats userStat,
                    Targets target, Stats targetStat,
                    int hPCost, int mPCost,
                    int power, int accuracy,
                    int critChance, int armorTearing,
                    int minNumberOfHits, int maxNumberOfHits,
                    StatusEffects statusEffect, int statusEffectChance,
                    bool alwaysHit, bool alwaysEffect)
        {
            Name = name;
            Type = type;
            Element = element;
            UserStat = userStat;
            Target = target;
            TargetStat = targetStat;
            HPCost = hPCost;
            MPCost = mPCost;
            Power = power;
            Accuracy = accuracy;
            CritChance = critChance;
            ArmorTearing = armorTearing;
            MinNumberOfHits = minNumberOfHits;
            MaxNumberOfHits = maxNumberOfHits;
            StatusEffect = statusEffect;
            StatusEffectChance = statusEffectChance;
            AlwaysHit = alwaysHit;
            AlwaysEffect = alwaysEffect;
        }

        public SkillNew(string name, SkillTypes type,
                     Elements element, Stats userStat,
                     Targets target, Stats targetStat,
                     int hPCost, int mPCost,
                     int power, int accuracy,
                     int critChance)
            : this(name, type, element, userStat, target, targetStat, hPCost, mPCost, power, accuracy, critChance, 0, 1, 1, StatusEffects.None, 0, false, false)
        { }

        public int CalculateRawDamage(ActorNew user)
        {
            int actorStat = user.Stats[UserStat];
            int variance = Calc.NextRange(0, VARIANCE_AMP_PERCENT);

            double a = this.Power;
            double b = Math.Pow(actorStat, 0.15);
            double c = actorStat + 1 + ((double)actorStat / 100);
            double d = 1;
            double e = (100.0 + variance) / 100;

            return (int)(a * b * c * d * e / 2);
        }

        public int CalculateDamageReduction(ActorNew attacker, ActorNew defender)
        {
            Resistances defResistance = defender.Modifiers.GetInnateResistance(Element);

            int a = (int)defResistance;
            double b = defender.Stats[TargetStat] * DEFENDER_BONUS;
            int c = attacker.Stats[UserStat];

            double d = a + b - c;

            return (int)MathHelper.Clamp((float)d, defResistance.MinReduction(), 90);
        }

        public int CalculateHealing(ActorNew user, ActorNew target)
        {
            if (Type == SkillTypes.Healing)
            {
                double a = 2 * Power;
                double b = 2 * Power * Math.Pow(user.Stats[UserStat], 0.6);
                double c = Power * Math.Pow(target.Stats[TargetStat], 0.6);

                return (int)((a + b + c) / 5);
            }
            else if (Type == SkillTypes.Reviving)
            {
                double d = (double)Power / 100;
                double e = target.MaxHP;

                return (int)(d * e);
            }
            throw new Exception("Healing type not implemented.");
        }

        public bool CalculateHit(ActorNew user, ActorNew target)
        {
            if (Type != SkillTypes.Damage)
                return true;
            else
            {
                double a = Accuracy;
                double b = user.Level * 0.25;
                double c = user.Speed * 1.3;
                double d = target.Speed * 2.0;

                int e = (int)(a + b + c - d);

                int roll = Calc.RollFlat();

                return e > roll;
            }
        }

        public int CalculateHPCost(ActorNew user)
        {
            return (int)(HPCost * user.MaxHP / 100f);
        }

        public int Calculate(ActorNew attacker, ActorNew defender)
        {
            if (Type == SkillTypes.Damage)
            {
                int a = CalculateRawDamage(attacker);
                double b = CalculateDamageReduction(attacker, defender) / 100.0;

                return (int)(a * (1 - b));
            }
            else
            {
                int c = CalculateHealing(attacker, defender);
                return -c;
            }

        }

        public bool IsUsableBy(ActorNew actor)
        {
            return actor.MP >= MPCost && actor.HP > (int)(HPCost * actor.MaxHP / 100f);
        }

        public bool IsUseableOn(ActorNew actor)
        {
            switch (Type)
            {
                case SkillTypes.Damage:
                    return !actor.IsDead;
                case SkillTypes.Healing:
                    return !actor.IsDead;
                case SkillTypes.Reviving:
                    return actor.IsDead;
                default:
                    Logger.Log("SkillNew Type not accounted for!");
                    return false;
            }
        }

        public static bool IsBasic(SkillNew skill)
        {
            return skill == Attack ||
                   skill == Gun ||
                   skill == Guard ||
                   skill == Pass;
        }



        public static SkillNew Attack = new SkillNew("Attack", SkillTypes.Damage, Elements.Physical, Stats.Strength, Targets.Enemy, Stats.Endurance, 0, 0, 10, 90, 5);
        public static SkillNew Bash = new SkillNew("Bash", SkillTypes.Damage, Elements.Physical, Stats.Strength, Targets.Enemy, Stats.Endurance, 3, 0, 20, 85, 10);
        public static SkillNew Gun = new SkillNew("Gun", SkillTypes.Damage, Elements.Pierce, Stats.Strength, Targets.Enemy, Stats.Endurance, 0, 0, 10, 80, 5);
        public static SkillNew Guard = new SkillNew("Guard", SkillTypes.Healing, Elements.Physical, Stats.None, Targets.Self, Stats.None, 0, 0, 0, 100, 0);
        public static SkillNew Pass = new SkillNew("Pass", SkillTypes.Healing, Elements.Physical, Stats.None, Targets.Self, Stats.None, 0, 0, 0, 100, 0);
        public static SkillNew Fireball = new SkillNew("Fireball", SkillTypes.Damage, Elements.Fire, Stats.Magic, Targets.Enemy, Stats.Resilience, 0, 4, 15, 90, 0);
        public static SkillNew Heal = new SkillNew("Heal", SkillTypes.Healing, Elements.Healing, Stats.Magic, Targets.Ally, Stats.Resilience, 0, 4, 15, 100, 0);
        public static SkillNew Revive = new SkillNew("Revive", SkillTypes.Reviving, Elements.Healing, Stats.Magic, Targets.AllyExcludingSelf, Stats.Resilience, 0, 20, 25, 100, 0);
        public static SkillNew ElecBomb = new SkillNew("ElecBomb", SkillTypes.Damage, Elements.Electric, Stats.Strength, Targets.EnemyParty, Stats.Resilience, 0, 0, 30, 70, 10);









    }
}
