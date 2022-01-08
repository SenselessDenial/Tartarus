using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;  

namespace Tartarus
{
    public class HeroNew : ActorNew
    {
        public GTexture Icon { get; private set; }
        public bool IsUnlocked { get; set; }
        public Color Color { get; private set; }

        public ItemNew Item { get; private set; }
        public int ItemAmount = 0;
        public bool HasItem => Item != null;

        public HeroNew(string name,
                    int str, int mag, int end, int res, int spd,
                    int strWeight, int magWeight, int endWeight, int resWeight, int spdWeight) 
            : base(name, str, mag, end, res, spd, strWeight, magWeight, endWeight, resWeight, spdWeight)
        {
            AddSkill(SkillNew.Gun);
            AddSkill(SkillNew.Guard);
            AddSkill(SkillNew.Pass);
        }

        public void SetItems(GTexture portrait, GTexture icon, Color color)
        {
            Portrait = portrait;
            Icon = icon;
            Color = color;
        }

        public HeroNew(HeroNew parent) : base(parent)
        {
            Icon = parent.Icon;
            IsUnlocked = parent.IsUnlocked;
            Color = parent.Color;
        }







    }
}
