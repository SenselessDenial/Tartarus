using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public static class ActorPresets
    {
        public static HeroPreset Gabriel { get; private set; }
        public static HeroPreset Anna { get; private set; }
        public static HeroPreset Sophie { get; private set; }
        public static HeroPreset Eva { get; private set; }
        public static HeroPreset Madeline { get; private set; }

        public static EnemyPreset Beast { get; private set; }


        private static Tileset portraits;

        public static void Begin()
        {
            portraits = new Tileset("character_portraits.png", 65, 78);


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

            Anna = new HeroPreset(anna, true, portraits[0]);
            Sophie = new HeroPreset(sophie, true, portraits[1]);
            Eva = new HeroPreset(eva, true, portraits[2]);
            Madeline = new HeroPreset(madeline, true, portraits[3]);

            ////// Enemy Presets

            // Beast
            Actor beast = new Actor("Beast", 7, 5, 3, 3, 7);
            beast.AddSkill(Skill.Bash);
            beast.AddInnateResistance(Elements.Fire, Resistances.Weak);
            Beast = new EnemyPreset(beast);




        }

        public static void End()
        {
            
        }

        public struct HeroPreset
        {
            public Actor Actor;
            public bool IsUnlocked;
            public GTexture Portrait;

            public HeroPreset(Actor actor, bool isUnlocked, GTexture portrait)
            {
                Actor = actor;
                IsUnlocked = isUnlocked;
                Portrait = portrait;
            }

            public HeroPreset(Actor actor, bool isUnlocked) 
                : this(actor, isUnlocked, null) { }
        }

        public struct EnemyPreset
        {
            public Actor Actor;

            public EnemyPreset(Actor actor)
            {
                Actor = actor;
            }


        }


    }
}
