using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class EncounterScene : Scene
    {

        public EncounterScene() : base()
        {
            // DO NOT INITALIZE OBJECTS HERE!
        }

        public override void Begin()
        {
            base.Begin();
            FillColor = Color.Red;
            Camera.Scale = new Vector2(4);


            

        }

        public override void End()
        {
            base.End();


        }

        public override void Update()
        {
            base.Update();

            if (Input.Pressed(MappedKeys.A))
                SetNextScene(SceneManager.MapScene);

        }

        public override void Render()
        {
            base.Render();


        }


    }
}
