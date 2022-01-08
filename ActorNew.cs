using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class ActorNew
    {
        public string Name { get; protected set; }
        public StatSheetNew Stats { get; protected set; }
        public ModifierListNew Modifiers { get; protected set; }
        public SkillSetNew Skills { get; protected set; }
        public GTexture Portrait { get; protected set; }

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
        public float HPPercent => (float)HP / MaxHP;
        public string Card => Name + "\nHP: " + HP + "/" + MaxHP + "\nMP: " + MP + "/" + MaxMP;

        public ActorNew(ActorNew parent)
        {
            Name = parent.Name;
            Stats = parent.Stats.Copy(this);
            Modifiers = parent.Modifiers.Copy(this);
            Skills = parent.Skills.Copy(this);
            Portrait = parent.Portrait;
        }

        protected ActorNew(string name,
                     int str, int mag, int end, int res, int spd, int level, int xp)
        {
            Name = name;
            Stats = new StatSheetNew(this, str, mag, end, res, spd, level, xp);
            Skills = new SkillSetNew(this);
            Modifiers = new ModifierListNew(this);
        }

        public ActorNew(string name,
                     int str, int mag, int end, int res, int spd,
                     int strWeight, int magWeight, int endWeight, int resWeight, int spdWeight)
        {
            Name = name;
            Stats = new StatSheetNew(this, str, mag, end, res, spd, strWeight, magWeight, endWeight, resWeight, spdWeight);
            Skills = new SkillSetNew(this);
            Modifiers = new ModifierListNew(this);
        }

        protected ActorNew(string name,
                     int str, int mag, int end, int res, int spd)
            : this(name, str, mag, end, res, spd, 20, 20, 20, 20, 20) { }

        private void LevelUp()
        {
            Stats.LevelUp();
        }

        private void LevelUp(int iterations)
        {
            for (int i = 0; i < iterations; i++)
                LevelUp();
        }

        public bool HasSkill(SkillNew skill)
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

        public void TakeCosts(SkillNew skill)
        {
            HP -= skill.CalculateHPCost(this);
            MP -= skill.MPCost;
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

        public void AddSkill(SkillNew skill)
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

        public bool ContainsSkill(SkillNew skill)
        {
            return Skills.Contains(skill);
        }




    }
}
