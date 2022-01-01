using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public static class HeroPresets
    {
        public static Actor Gabriel
        {
            get
            {
                Actor temp = new Actor("Gabriel", 9, 3, 5, 3, 5, 35, 10, 20, 15, 20);
                temp.Modifiers.AddInnateOffensiveBonus(Elements.Physical, OffensiveBonuses.Great);
                temp.Modifiers.AddInnateOffensiveBonus(Elements.Pierce, OffensiveBonuses.Good);
                temp.Modifiers.AddInnateOffensiveBonus(Elements.Fire, OffensiveBonuses.Bad);
                temp.Modifiers.AddInnateOffensiveBonus(Elements.Electric, OffensiveBonuses.Awful);
                temp.Modifiers.AddInnateResistance(Elements.Physical, Resistances.Strong);
                temp.Modifiers.AddInnateResistance(Elements.Electric, Resistances.Weak);
                return temp;
            }
        }











    }
}
