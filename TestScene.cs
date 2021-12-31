using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tartarus
{
    class TestScene : Scene
    {
        private Entity test;
        EncounterManager em;
        Encounter e;

        Actor a;
        Actor a2;
        Actor b;

        Party alpha;
        Party beta;

        Background v;

        public override void Begin()
        {
            base.Begin();
            Camera.Scale = new Vector2(4f);

            test = new Entity(this);
            var im = new Image("wolf.png");
            test.Add(im);
            im.Offset = new Vector2(40, 40);

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
            b.LevelUp(9);

            v = new Background(new GTexture("viennabg.png"));


            alpha = new Party(a, a2);
            beta = new Party(b);

            e = new Encounter(alpha, beta);
            em = new EncounterManager(e);

            test.Add(em);

        }

        public override void Update()
        {
            base.Update();
        }

















    }
}
