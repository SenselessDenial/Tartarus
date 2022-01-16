using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class ItemOLD
    {
        public string Name { get; private set; }
        public SkillOLD Skill { get; private set; }
        public int Cost { get; private set; }
        public int MaxAmount { get; private set; }

        public ItemOLD(string name, SkillOLD skill, int cost, int maxAmount)
        {
            Name = name;
            Skill = skill;
            Cost = cost;
            MaxAmount = maxAmount;
        }

        public static ItemOLD TeslaGrenade = new ItemOLD("Tesla Grenade", SkillOLD.ElecBomb, 50, 10);


    }
}
