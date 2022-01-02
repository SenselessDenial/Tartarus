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
        public static HeroPreset Gabriel { get; private set; }
        public static HeroPreset Anna { get; private set; }
        public static HeroPreset Sophie { get; private set; }
        public static HeroPreset Eva { get; private set; }
        public static HeroPreset Madeline { get; private set; }

        public static void Begin()
        {
            // Gabriel
            Actor gabriel = new Actor("Gabriel", 9, 3, 5, 3, 5, 35, 10, 20, 15, 20);
            gabriel.Modifiers.AddInnateOffensiveBonus(Elements.Physical, OffensiveBonuses.Great);
            gabriel.Modifiers.AddInnateOffensiveBonus(Elements.Pierce, OffensiveBonuses.Good);
            gabriel.Modifiers.AddInnateOffensiveBonus(Elements.Fire, OffensiveBonuses.Bad);
            gabriel.Modifiers.AddInnateOffensiveBonus(Elements.Electric, OffensiveBonuses.Awful);
            gabriel.Modifiers.AddInnateResistance(Elements.Physical, Resistances.Strong);
            gabriel.Modifiers.AddInnateResistance(Elements.Electric, Resistances.Weak);
            

            
           
            // Anna
            Actor anna = new Actor("Anna", 2, 8, 4, 8, 3, 5, 30, 20, 30, 15);

            // Sophie
            Actor sophie = new Actor("Sophie", 5, 5, 5, 5, 5, 20, 20, 20, 20, 20);

            // Eva
            Actor eva = new Actor("Eva", 10, 1, 6, 2, 6, 30, 5, 25, 15, 25);

            // Madeline
            Actor madeline = new Actor("Madeline", 3, 7, 3, 3, 9, 15, 15, 20, 20, 30);


            Gabriel = new HeroPreset(gabriel, false);

            Anna = new HeroPreset(anna, true);
            Sophie = new HeroPreset(sophie, true);
            Eva = new HeroPreset(eva, true);
            Madeline = new HeroPreset(madeline, true);


        }

        public static void End()
        {
            
        }

        public struct HeroPreset
        {
            public Actor Actor;
            public bool IsUnlocked;

            public HeroPreset(Actor actor, bool isUnlocked)
            {
                Actor = actor;
                IsUnlocked = isUnlocked;
            }
        }


    }
}
