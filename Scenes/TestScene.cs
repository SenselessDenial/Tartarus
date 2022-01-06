using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class TestScene : Scene
    {
        private HeroNew a;
        private EnemyNew b;

        private EncounterNew e;





        public TestScene() : base()
        {
            // DO NOT INITALIZE OBJECTS HERE!
        }

        public override void Begin()
        {
            base.Begin();
            FillColor = Color.Lime;
            Camera.Scale = new Vector2(4);

            a = new HeroNew("bob", 2, 2, 2, 2, 2, 1, 1, 1, 1, 1);
            b = new EnemyNew("arg", 1, 1, 1, 1, 1, 1, 0, Affiliations.None, 20);

            e = new EncounterNew(new HeroParty(a), new EnemyParty(b), true);

        }

        public override void End()
        {
            base.End();


        }

        public override void Update()
        {
            base.Update();

            if (Input.Pressed(MappedKeys.A))
                e.UseSkill(SkillNew.Attack, 0);


        }

        public override void Render()
        {
            base.Render();


        }


    }
}
