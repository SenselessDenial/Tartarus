using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tartarus
{
    class Test
    {
        private Actor aa;
        private Actor ab;
        private Actor ac;

        private Actor ba;
        private Actor bb;

        private Party a;
        private Party b;

        private Encounter encounter;

        private Player Guy;
        private Actor enemy;
        private Tileset buttons;
        private ClickableButton buttontest;

        private ClickableButton attack;
        private ClickableButton heal;
        private ClickableButton revive;

        private Font font;

        private List<ClickableButton> buttonList;

        private Background bg;

        public void Initialize()
        {
            aa = new Actor("AAAAAAA");
            aa.Skills.Add(Skill.Attack);
            aa.Skills.Add(Skill.Fireball);
            ab = new Actor("ABABABA");
            ac = new Actor("ACACACA");

            ba = new Actor("BABABAB");
            bb = new Actor("BBBBBBB");

            a = new Party(4, aa, ab, ac);
            b = new Party(ba, bb);

            encounter = new Encounter(a, b);

            buttons = new Tileset("buttonset.png", 33, 17);

            buttonList = new List<ClickableButton>();

            Guy = new Player();
            enemy = new Actor("Uncle Ben");
            enemy.MaxHP = 400;
            enemy.HP = 10;

            font = new Font("small_font.png", 4, 6);

            bg = new Background(new GTexture("bgTexture.png"), new Vector2(0, 0), true, true);

            buttontest = new ClickableButton(new Vector2(100, 150), font.FindTexture("yuh poobah"), () => 
            {
                encounter.UseSkill(Skill.Fireball, aa, bb);
            });

            attack = new ClickableButton(new Vector2(20, 20), buttons[0, 3], buttons[1, 3], () =>
             {
                 encounter.UseSkill(Skill.Attack, aa, ba);
             });

            heal = new ClickableButton(new Vector2(60, 20), buttons[0, 1], buttons[1, 1], () =>
            {
                
            });

            revive = new ClickableButton(new Vector2(100, 60), buttons[0, 2], buttons[1, 2], () =>
            {
                encounter.Next();
                Logger.Log(encounter.CurrentActor.Name);
            });

            buttonList.Add(attack);
            buttonList.Add(heal);
            buttonList.Add(revive);
            buttonList.Add(buttontest);
          
        }

        public void Update()
        {
            foreach (var item in buttonList)
            {
                item.Update();
            }
            bg.Update();
        }

        public void Draw()
        {
            bg.Render();

            foreach (var item in buttonList)
            {
                item.Render();
            }
        }
    }
}