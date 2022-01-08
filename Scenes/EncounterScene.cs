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

        private EncounterNewEntity ee;

        private GTexture box;

        public EncounterScene() : base()
        {
            // DO NOT INITALIZE OBJECTS HERE!
        }

        public override void Begin()
        {
            base.Begin();
            FillColor = Color.DarkTurquoise;
            Camera.Scale = new Vector2(4);

            ee = new EncounterNewEntity(this, new EncounterNew(RunData.PlayerParty, RunData.CurrentEnemyParty, true));

            box = new GTexture("herobox.png").ReplaceWithPattern(new GTexture("pattern.png"), Color.White);
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

            if (ee.HeroesWin)
                SetNextScene(SceneManager.MapScene);
            if (ee.EnemiesWin)
                SetNextScene(SceneManager.LoseScene);

        }

        public override void Render()
        {
            base.Render();

            box.Draw(new Vector2(0, 0));

        }


    }
}
