using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tartarus
{
    public class MapScene : Scene
    {
        BasicEffect ef = new BasicEffect(TartarusGame.Instance.GraphicsDevice);

        Entity helper;
        NumberDisplayer money;
        ParticleManager pm;


        private MapReader reader;


        public MapScene() : base()
        {
            // DO NOT INITALIZE OBJECTS HERE!

        }

        public override void Begin()
        {
            base.Begin();
            FillColor = Color.LightCoral;
            Camera.Scale = new Vector2(4);

            if (RunData.Map.IsDone)
                SetNextScene(SceneManager.WinScene);

            helper = new Entity(this);
            money = new NumberDisplayer(helper, 0, 0.008f);
            money.Offset = new Vector2(this.Camera.Width - 50, 0f);
            pm = new ParticleManager(helper);
            

            reader = new MapReader(this);
        }

        public override void End()
        {
            base.End();

            Entities.Clear();
        }

        public override void Update()
        {
            base.Update();
            money.TargetNum = RunData.Money;

            if (Input.Pressed(Microsoft.Xna.Framework.Input.Keys.K))
                pm.Spawn(ParticleManager.ParticleID.Miss, Calc.Next(-500, -10), new Vector2(90, 50));
        }

        public override void Render()
        {
            base.Render();

            RunData.Map.Draw(new Vector2(50, 50));
        }


    }
}
