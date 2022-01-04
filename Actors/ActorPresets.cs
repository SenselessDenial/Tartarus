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
        public static Hero Gabriel { get; private set; }
        public static Hero Sophie { get; private set; }
        public static Hero Eva { get; private set; }
        public static Hero Madeline { get; private set; }

        public static EnemyPreset Beast { get; private set; }

        public static Hero Anna { get; private set; }


        private static Tileset portraits;
        private static Tileset heroIcons;

        public static void Begin()
        {
            portraits = new Tileset("character_portraits.png", 65, 78);
            heroIcons = new Tileset("heroicons.png", 32, 16);


            // Gabriel
            Actor gabriel = new Actor("Gabriel", 9, 3, 5, 3, 5, 35, 10, 20, 15, 20);
            gabriel.Modifiers.AddInnateOffensiveBonus(Elements.Physical, OffensiveBonuses.Great);
            gabriel.Modifiers.AddInnateOffensiveBonus(Elements.Pierce, OffensiveBonuses.Good);
            gabriel.Modifiers.AddInnateOffensiveBonus(Elements.Fire, OffensiveBonuses.Bad);
            gabriel.Modifiers.AddInnateOffensiveBonus(Elements.Electric, OffensiveBonuses.Awful);
            gabriel.Modifiers.AddInnateResistance(Elements.Physical, Resistances.Strong);
            gabriel.Modifiers.AddInnateResistance(Elements.Electric, Resistances.Weak);
            

            
           
            // Anna
            Anna = new Hero("Anna", 2, 8, 4, 8, 3, 5, 30, 20, 30, 15);
            Anna.Portrait = portraits[0];
            Anna.Icon = heroIcons[0];
            Anna.Color = new Color(222, 106, 60);
            Anna.IsUnlocked = true;

            // Sophie
            Sophie = new Hero("Sophie", 5, 5, 5, 5, 5, 20, 20, 20, 20, 20);
            Sophie.Portrait = portraits[1];
            Sophie.Icon = heroIcons[1];
            Sophie.Color = new Color(54, 89, 64);
            Sophie.IsUnlocked = true;

            // Eva
            Eva = new Hero("Eva", 10, 1, 6, 2, 6, 30, 5, 25, 15, 25);
            Eva.Portrait = portraits[2];
            Eva.Icon = heroIcons[2];
            Eva.Color = new Color(247, 107, 214);
            Eva.IsUnlocked = true;

            // Madeline
            Madeline = new Hero("Madeline", 3, 7, 3, 3, 9, 15, 15, 20, 20, 30);
            Madeline.Portrait = portraits[3];
            Madeline.Icon = heroIcons[3];
            Madeline.Color = new Color(51, 230, 212);
            Madeline.IsUnlocked = true;

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
            public Actor Actor => actor.Copy();
            public bool IsUnlocked;
            public GTexture Portrait;
            public Actor actor;

            public HeroPreset(Actor actor, bool isUnlocked, GTexture portrait)
            {
                this.actor = actor;
                IsUnlocked = isUnlocked;
                Portrait = portrait;
            }

            public HeroPreset(Actor actor, bool isUnlocked) 
                : this(actor, isUnlocked, null) { }
        }

        public struct EnemyPreset
        {
            public Actor Actor => actor.Copy();
            private Actor actor;

            public EnemyPreset(Actor actor)
            {
                this.actor = actor;
            }


        }


    }
}
