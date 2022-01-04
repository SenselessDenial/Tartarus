using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class WinScene : Scene
    {

        public WinScene() : base()
        {
            // DO NOT INITALIZE OBJECTS HERE!
        }

        public override void Begin()
        {
            base.Begin();
            FillColor = Color.Cyan;
            Camera.Scale = new Vector2(4);

        }

        public override void End()
        {
            base.End();


        }

        public override void Update()
        {
            base.Update();

            if (Input.Pressed(MappedKeys.Start))
                SetNextScene(SceneManager.MainMenu);

        }

        public override void Render()
        {
            base.Render();

            Drawing.Font.DrawOutline("You win!", new Vector2(20, 20), Color.Gainsboro);
        }


    }
}
