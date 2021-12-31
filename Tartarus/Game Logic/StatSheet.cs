using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class StatSheet
    {
        public Stat HP { get; private set; }
        public Stat MP { get; private set; }
        public Stat Strength { get; private set; }
        public Stat Magic { get; private set; }
        public Stat Endurance { get; private set; }
        public Stat Resilience { get; private set; }
        public Stat Speed { get; private set; }
        public static StatSheet Default => new StatSheet(10, 10, 5, 5, 5, 5, 5, 20, 20, 20, 20, 20);

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

        public StatSheet(int hP, int mP, int strength, int magic, int endurance, int resilience, int speed,
                         int strWeight, int magWeight, int endWeight, int resWeight, int spdWeight)
        {
            HP = new Stat(hP);
            MP = new Stat(mP);
            Strength = new Stat(strength, 99, strWeight);
            Magic = new Stat(magic, 99, magWeight);
            Endurance = new Stat(endurance, 99, endWeight);
            Resilience = new Stat(resilience, 99, resWeight);
            Speed = new Stat(speed, 99, spdWeight);
        }

        public StatSheet(int hP, int mP, int strength, int magic, int endurance, int resilience, int speed)
            : this(hP, mP, strength, magic, endurance, resilience, speed, 20, 20, 20, 20, 20) { }

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


    }
}
