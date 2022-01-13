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
        public static HeroNew Anna { get; private set; }
        public static HeroNew Sophie { get; private set; }
        public static HeroNew Eva { get; private set; }
        public static HeroNew Madeline { get; private set; }

        public static EnemyNew Beast { get; private set; }

        private static Tileset portraits;
        private static Tileset heroIcons;

        public static void Begin()
        {
            portraits = new Tileset("character_portraits.png", 65, 78);
            heroIcons = new Tileset("heroicons.png", 32, 16);
            
            // Anna
            Anna = new HeroNew("Anna", 2, 3, 8, 4, 8, 5);
            Anna.SetItems(portraits[0], heroIcons[0], new Color(222, 106, 60));
            Anna.AddSkill(SkillNew.Fireball);
            Anna.AddSkill(SkillNew.Heal);
            Anna.AddSkill(SkillNew.ElecBomb);
            Anna.IsUnlocked = true;

            // Sophie
            Sophie = new HeroNew("Sophie", 5, 8, 3, 4, 3, 7);
            Sophie.SetItems(portraits[1], heroIcons[1], new Color(54, 89, 64));
            Sophie.IsUnlocked = true;

            // Eva
            Eva = new HeroNew("Eva", 12, 2, 1, 6, 5, 4);
            Eva.SetItems(portraits[2], heroIcons[2], new Color(247, 107, 214));
            Eva.IsUnlocked = true;

            // Madeline
            Madeline = new HeroNew("Madeline", 4, 4, 10, 4, 4, 4);
            Madeline.SetItems(portraits[3], heroIcons[3], new Color(51, 230, 212));
            Madeline.IsUnlocked = true;
            Madeline.AddSkill(SkillNew.Heal, SkillNew.Revive);
            

            ////// Enemy Presets

            // Beast
            Beast = new EnemyNew("Beast", 5, 5, 5, 5, 5, 5, 1, 0, Affiliations.None, 3);
            Beast.SetItems(new GTexture("wolf.png"));
            Beast.AddSkill(SkillNew.Bash);
            Beast.AddInnateResistance(Elements.Fire, Resistances.Weak);




        }

        public static void End()
        {
            
        }


    }
}
