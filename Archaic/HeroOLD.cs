using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class HeroOLD : ActorOLD
    {
        public GTexture Portrait;
        public GTexture Icon;
        public Color Color;
        public bool IsUnlocked;

        private HeroOLD() 
            : base() { }

        public HeroOLD(string name,
                     int str, int mag, int end, int res, int spd,
                     int strWeight, int magWeight, int endWeight, int resWeight, int spdWeight)
            : base(name, str, mag, end, res, spd, strWeight, magWeight, endWeight, resWeight, spdWeight) { }

        public new HeroOLD Copy()
        {
            HeroOLD temp = new HeroOLD();
            AssembleHero(temp, Name, Stats.Copy(temp), Skills.Copy(temp), Modifiers.Copy(temp));
            temp.Portrait = Portrait;
            temp.Icon = Icon;
            temp.Color = Color;
            temp.IsUnlocked = IsUnlocked;

            return temp;
        }

        private void AssembleHero(HeroOLD actor, string name, StatSheetOLD stats, SkillSetOLD skills, ModifierListOLD modifiers)
        {
            actor.Name = name;
            actor.Stats = stats;
            actor.Skills = skills;
            actor.Modifiers = modifiers;
            actor.Party = null;
        }


    }
}
