using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tartarus
{
    class Demo
    {

        EncounterManager em;
        Encounter e;

        Actor a;
        Actor a2;
        Actor b;

        Party alpha;
        Party beta;

        Background v;

        GTexture evil_blart;
        GTexture blart;
        GTexture fart;


        public void Initialize()
        {
            a = new Actor("Paul Blart");
            a.AddSkill(Skill.Fireball);
            a.AddSkill(Skill.Gun);
            a.AddSkill(Skill.Heal);
            a.AddSkill(Skill.ElecBomb);

            a2 = new Actor("Fartholomew");
            a2.AddSkill(Skill.Gun);
            a2.AddSkill(Skill.Revive);
            a2.LevelUp(4);

            b = new Actor("Evil Paul Blart");
            b.AddSkill(Skill.Fireball);
            b.AddSkill(Skill.ElecBomb);

            v = new Background(new GTexture("viennabg.png"));

            evil_blart = new GTexture("paul_blart.png");
            blart = new GTexture("blart_mini.png");
            fart = new GTexture("fartholomew_mini.png");


            for (int i = 0; i < 10; i++)
                b.LevelUp();

            alpha = new Party(a, a2);
            beta = new Party(b);

            e = new Encounter(alpha, beta);
            em = new EncounterManager(e);
        }

        public void Update()
        {
            v.X += 0.3f;
            v.Y += 0.1f;
            v.Update();
            
            em.Update();


            if (Input.Pressed(Keys.Up))
                em.CurrentChoice.MoveNext();
            else if (Input.Pressed(Keys.Down))
                em.CurrentChoice.MovePrevious();

            if (Input.Pressed(Keys.C))
                em.GoBack();

            if (Input.Pressed(Keys.Space))
                em.CurrentChoice.Invoke();


            
        }

        public void Draw()
        {
            v.Draw();

            TartarusGame.SmallFont.FindTexture(a.Card).Draw(new Vector2(10, 80));
            TartarusGame.SmallFont.FindTexture(a2.Card).Draw(new Vector2(10, 135));
            TartarusGame.SmallFont.FindTexture(b.Card).Draw(new Vector2(100, 80));
            em.Draw();

            evil_blart.Draw(new Vector2(5f * (float)Math.Cos(TartarusGame.RawTotalTime), 100));
            blart.Draw(new Vector2(20, 100));
            fart.Draw(new Vector2(10, 155));
        }
    }
}
