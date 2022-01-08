using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    public class ItemNew
    {
      

        public string Name { get; private set; }
        public SkillNew Skill { get; private set; }
        public int Cost { get; private set; }
        public int MaxAmount { get; private set; }

        public ItemNew(string name, SkillNew skill, int cost, int maxAmount)
        {
            Name = name;
            Skill = skill;
            Cost = cost;
            MaxAmount = maxAmount;
        }

        public static ItemNew TeslaGrenade = new ItemNew("Tesla Grenade", SkillNew.ElecBomb, 50, 10);



    }
}
