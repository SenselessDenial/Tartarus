using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class Stat
    {
        private int value;
        private int maxValue;
        private int weight;

        public int Value
        {
            get => value;
            set
            {
                if (value >= MaxValue)
                    this.value = MaxValue;
                else if (value <= MinValue)
                    this.value = MinValue;
                else
                    this.value = value;
            }
        }
        public int MaxValue
        {
            get => maxValue;
            set
            {
                this.value += value - maxValue;
                maxValue = value <= MinValue ? MinValue : value;
                Value = this.value;
            }
        }
        public int Weight
        {
            get => value >= maxValue ? 0 : weight;
            private set => weight = value;
        }
        private static int MinValue => 0;

        public Stat(int value, int maxValue, int weight)
        {
            this.value = value;
            this.maxValue = maxValue;
            Weight = weight;
        }

        public Stat(int maxValue, int weight)
            : this(maxValue, maxValue, weight) { }

        public Stat(int maxValue)
            : this(maxValue, 1) { }

    }

    public enum Stats
    {
        None,
        HP,
        MP,
        Strength,
        Dexterity,
        Magic,
        Endurance,
        Resilience,
        Speed
    }


}
