using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;  

namespace Tartarus
{
    public class Hero : Actor
    {
        public GTexture Icon { get; private set; }
        public bool IsUnlocked { get; set; }
        public Color Color { get; private set; }

        public Item Item { get; private set; }
        public int ItemAmount = 0;
        public bool HasItem => Item != null;

        public static readonly GTexture CardBase = new GTexture("hero_card.png");
        public static readonly GTexture BookmarkBase = new GTexture("hero_bookmark.png");

        


        public Hero(string name,
                    int str, int dex, int mag, int end, int res, int spd,
                    int strWeight, int dexWeight, int magWeight, int endWeight, int resWeight, int spdWeight) 
            : base(name, str, dex, mag, end, res, spd, strWeight, dexWeight, magWeight, endWeight, resWeight, spdWeight)
        {
            AddSkill(Skill.Gun);
            AddSkill(Skill.Guard);
            AddSkill(Skill.Pass);
        }

        public Hero(string name, int str, int dex, int mag, int end, int res, int spd)
            : this(name, str, dex, mag, end, res, spd, str, dex, mag, end, res, spd) { }

        public void SetItems(GTexture portrait, GTexture icon, Color color)
        {
            Portrait = portrait;
            Icon = icon;
            Color = color;
        }

        public Hero(Hero parent) : base(parent)
        {
            Icon = parent.Icon;
            IsUnlocked = parent.IsUnlocked;
            Color = parent.Color;
        }

        public void Draw(Vector2 pos)
        {
            Color color = Color.White;
            if (IsDead)
                color = Color.Gray;

            CardBase.Draw(pos, color);
            Icon.Draw(pos, color);
            new HealthBar(28, 2, Color.Yellow, Color.Black, false, XPPercent).Draw(new Vector2(34, 11) + pos);
            new HealthBar(59, 2, Color.Red, Color.Black, false, HPPercent).Draw(new Vector2(3, 18) + pos);
            new HealthBar(59, 2, Color.DeepSkyBlue, Color.Black, false, MPPercent).Draw(new Vector2(3, 31) + pos);

            Drawing.SmallFont.Draw(HP + "/" + MaxHP, new Vector2(43, 22) + pos, Color.Black);
            Drawing.SmallFont.Draw(MP + "/" + MaxMP, new Vector2(43, 35) + pos, Color.Black);
        }

        public void DrawBookmark(Vector2 pos)
        {
            Color fontColor = new Color(247, 238, 225);
            BookmarkBase.Draw(pos);
            Portrait.Draw(pos + new Vector2(3, 80), DrawAlignment.BottomLeft);
            Drawing.Font.DrawOutline(Name, pos + new Vector2(35, 84), fontColor, PixelFont.Alignment.Center);
            Drawing.Font.DrawOutline("STR:", pos + new Vector2(4, 98), fontColor);
            Drawing.Font.DrawOutline("DEX:", pos + new Vector2(4, 98 + (11 * 1)), fontColor);
            Drawing.Font.DrawOutline("MAG:", pos + new Vector2(4, 98 + (11 * 2)), fontColor);
            Drawing.Font.DrawOutline("END:", pos + new Vector2(4, 98 + (11 * 3)), fontColor);
            Drawing.Font.DrawOutline("RES:", pos + new Vector2(4, 98 + (11 * 4)), fontColor);
            Drawing.Font.DrawOutline("SPD:", pos + new Vector2(4, 98 + (11 * 5)), fontColor);

            Drawing.Font.DrawOutline(Strength.ToString(), pos + new Vector2(68, 98), fontColor, PixelFont.Alignment.Right);
            Drawing.Font.DrawOutline(Dexterity.ToString(), pos + new Vector2(68, 98 + (11 * 1)), fontColor, PixelFont.Alignment.Right);
            Drawing.Font.DrawOutline(Magic.ToString(), pos + new Vector2(68, 98 + (11 * 2)), fontColor, PixelFont.Alignment.Right);
            Drawing.Font.DrawOutline(Endurance.ToString(), pos + new Vector2(68, 98 + (11 * 3)), fontColor, PixelFont.Alignment.Right);
            Drawing.Font.DrawOutline(Resilience.ToString(), pos + new Vector2(68, 98 + (11 * 4)), fontColor, PixelFont.Alignment.Right);
            Drawing.Font.DrawOutline(Speed.ToString(), pos + new Vector2(68, 98 + (11 * 5)), fontColor, PixelFont.Alignment.Right);

            Vector2 skillpos = pos + new Vector2(4, 167);

            foreach (var skill in Skills.NonBasicSkills)
            {
                Drawing.Font.DrawOutline(skill.Name, skillpos, fontColor, PixelFont.Alignment.Left);
                skillpos.Y += 11;
            }
        }

    }
}
