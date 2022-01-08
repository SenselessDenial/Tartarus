using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class LoseScene : Scene
    {

        private int score = 1234;


        public LoseScene() : base()
        {
            // DO NOT INITALIZE OBJECTS HERE!
        }

        public override void Begin()
        {
            base.Begin();
            FillColor = Color.Black;
            Camera.Scale = new Vector2(4);

        }

        public override void End()
        {
            base.End();

            Entities.Clear();
        }

        public override void Update()
        {
            base.Update();

            if (Input.PressedAnyMappedKey())
                SetNextScene(SceneManager.MainMenu);

            score = Calc.Next(1000, 10000);
        }

        public override void Render()
        {
            base.Render();

            Drawing.Font.Draw("You lose!", new Vector2(50, 60));
            Drawing.Font.Draw("Score: " + score, new Vector2(50, 100));

        }


    }
}
