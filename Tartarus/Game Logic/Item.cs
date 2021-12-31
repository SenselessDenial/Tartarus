using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    class Item
    {
        public string Name { get; private set; }
        public Skill Skill { get; private set; }
        public int Cost { get; private set; }
        public int MaxAmount { get; private set; }

        public Item(string name, Skill skill, int cost, int maxAmount)
        {
            Name = name;
            Skill = skill;
            Cost = cost;
            MaxAmount = maxAmount;
        }

        public static Item TeslaGrenade = new Item("Tesla Grenade", Skill.ElecBomb, 50, 10);


    }
}
