using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class StatSheetOLD
    {
        public ActorOLD Actor { get; private set; }
        public Stat HP { get; private set; }
        public Stat MP { get; private set; }
        public Stat Strength { get; private set; }
        public Stat Magic { get; private set; }
        public Stat Endurance { get; private set; }
        public Stat Resilience { get; private set; }
        public Stat Speed { get; private set; }

        public int Level { get; private set; }
        public int XP { get; set; }

        public bool CanLevelUp => XP >= XPToNext;
        public int XPToNext => XPFormula(Level);
        private readonly Func<int, int> XPFormula = (int level) => 100 * level * (level + 1);
        internal static int MaxLevel = 100;

        public int this[Stats stat]
        {
            get
            {
                switch (stat)
                {
                    case Stats.HP:
                        return HP.Value;
                    case Stats.MP:
                        return MP.Value;
                    case Stats.Strength:
                        return Strength.Value;
                    case Stats.Magic:
                        return Magic.Value;
                    case Stats.Endurance:
                        return Endurance.Value;
                    case Stats.Resilience:
                        return Resilience.Value;
                    case Stats.Speed:
                        return Speed.Value;
                    case Stats.None:
                        return 0;
                    default:
                        throw new Exception("Stat not found.");
                }
            }
            set
            {
                switch (stat)
                {
                    case Stats.HP:
                        HP.Value = value;
                        break;
                    case Stats.MP:
                        MP.Value = value;
                        break;
                    case Stats.Strength:
                        Strength.Value = value;
                        break;
                    case Stats.Magic:
                        Magic.Value = value;
                        break;
                    case Stats.Endurance:
                        Endurance.Value = value;
                        break;
                    case Stats.Resilience:
                        Resilience.Value = value;
                        break;
                    case Stats.Speed:
                        Speed.Value = value;
                        break;
                    default:
                        Logger.Log("Stat not found.");
                        break;
                }
            }
        }

        private StatSheetOLD(ActorOLD actor, int strength, int magic, int endurance, int resilience, int speed,
                         int strWeight, int magWeight, int endWeight, int resWeight, int spdWeight, int level, int xp)
        {
            Actor = actor;
            Strength = new Stat(strength, 99, strWeight);
            Magic = new Stat(magic, 99, magWeight);
            Endurance = new Stat(endurance, 99, endWeight);
            Resilience = new Stat(resilience, 99, resWeight);
            Speed = new Stat(speed, 99, spdWeight);

            Level = level;
            XP = xp;
            HP = new Stat(CalculateMaxHP());
            MP = new Stat(CalculateMaxMP());
        }

        public StatSheetOLD(ActorOLD actor, int strength, int magic, int endurance, int resilience, int speed,
                         int strWeight, int magWeight, int endWeight, int resWeight, int spdWeight)
            : this(actor, strength, magic, endurance, resilience, speed,
                         strWeight, magWeight, endWeight, resWeight, spdWeight, 1, 0) { }

        public StatSheetOLD(ActorOLD actor, int strength, int magic, int endurance, int resilience, int speed)
            : this(actor, strength, magic, endurance, resilience, speed, 20, 20, 20, 20, 20) { }

        public StatSheetOLD(ActorOLD actor)
            : this(actor, 5, 5, 5, 5, 5, 20, 20, 20, 20, 20) { }

        public void IncrementRandom()
        {
            ChoicePool<Stats> statPool = new ChoicePool<Stats>();
            statPool.Add(Stats.Strength, Strength.Weight);
            statPool.Add(Stats.Magic, Magic.Weight);
            statPool.Add(Stats.Endurance, Endurance.Weight);
            statPool.Add(Stats.Resilience, Resilience.Weight);
            statPool.Add(Stats.Speed, Speed.Weight);

            this[statPool.Choose()] += 1;
        }

        private int CalculateMaxHP()
        {
            double a = 50;
            double b = (Math.Pow(Endurance.Value, 0.7) / 4);
            double c = 5;

            return (int)(a + b + c);
        }

        private int CalculateMaxMP()
        {
            double a = 20;
            double b = (Math.Pow(Magic.Value, 0.5) / 4);
            double c = 3;

            return (int)(a + b + c);
        }

        public void LevelUp()
        {
            if (Level >= MaxLevel)
            {
                Logger.Log(Actor.Name + " is already at max level of " + MaxLevel + ". Cannot level up.");
                return;
            }

            if (CanLevelUp)
                XP -= XPFormula(Level);
            Level += 1;

            for (var i = 0; i < 3; i++)
                IncrementRandom();

            HP.MaxValue = CalculateMaxHP();
            MP.MaxValue = CalculateMaxMP();

            Logger.Log(Actor.Name + " has leveled up to level " + Level + "!");
        }

        public override string ToString()
        {
            return "HP: " + HP.Value + " / " + HP.MaxValue
               + "\nMP: " + MP.Value + " / " + MP.MaxValue
               + "\nSTR: " + Strength.Value
               + "\nMAG: " + Magic.Value
               + "\nEND: " + Endurance.Value
               + "\nRES: " + Resilience.Value
               + "\nSPD: " + Speed.Value;
        }

        public StatSheetOLD Copy(ActorOLD actor)
        {
            return new StatSheetOLD(actor, Strength.Value, Magic.Value, Endurance.Value, Resilience.Value, Speed.Value,
                                   Strength.Weight, Magic.Weight, Endurance.Weight, Resilience.Weight, Speed.Weight, Level, XP);
        }


    }
}
