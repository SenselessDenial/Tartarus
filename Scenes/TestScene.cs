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

        public TestScene() : base()
        {
            // DO NOT INITALIZE OBJECTS HERE!
        }

        public override void Begin()
        {
            base.Begin();
            FillColor = Color.Lime;
            Camera.Scale = new Vector2(4);

        }

        public override void End()
        {
            base.End();


        }

        public override void Update()
        {
            base.Update();



        }

        public override void Render()
        {
            base.Render();


        }


    }
}
