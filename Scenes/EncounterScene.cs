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

        private EncounterEntity ee;

        public EncounterScene() : base()
        {
            // DO NOT INITALIZE OBJECTS HERE!
        }

        public override void Begin()
        {
            base.Begin();
            FillColor = Color.Red;
            Camera.Scale = new Vector2(4);

            ee = new EncounterEntity(this, RunData.PlayerParty, RunData.CurrentEnemyParty);
        }

        public override void End()
        {
            base.End();

            Entities.Clear();
        }

        public override void Update()
        {
            base.Update();

            if (Input.Pressed(Microsoft.Xna.Framework.Input.Keys.Q))
                SetNextScene(SceneManager.MapScene);

            if (ee.IsOver)
                SetNextScene(SceneManager.MapScene);

        }

        public override void Render()
        {
            base.Render();


        }


    }
}
