using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class CharacterSelectScene : Scene
    {
        private CharacterPicker picker;
        private Entity helper;
        private Background background;
        private Text pressStart;
        private SineWaver waver;
        


        public CharacterSelectScene() : base()
        {
            // DO NOT INITALIZE OBJECTS HERE!
        }

        public override void Begin()
        {
            base.Begin();
            FillColor = Color.LightSalmon;
            Camera.Scale = new Vector2(4);

            ActorPresets.Begin();
            RunData.Reset();


            picker = new CharacterPicker(this);
            picker.Position = new Vector2(20, 20);
            picker.Add(ActorPresets.Anna);
            picker.Add(ActorPresets.Sophie);
            picker.Add(ActorPresets.Eva);
            picker.Add(ActorPresets.Madeline);
            //picker.Add(ActorPresets.Gabriel);

            helper = new Entity(this);
            background = new Background(helper, new GTexture("viennabg2.png"));
            helper.Add(background);
            pressStart = new Text("PRESS START TO BEGIN",
                new Vector2(Camera.Width / 2, Camera.Height - 20),
                Color.White, true, PixelFont.Alignment.Center);
            helper.Add(pressStart);
            waver = new SineWaver(helper);
            waver.amplitude = 2f;
            waver.angFrequency = 2.5f;
            waver.displacement = pressStart.DrawingPosition.Y;
            helper.Layer = -2;
        }

        public override void End()
        {
            base.End();


        }

        public override void Update()
        {
            base.Update();

            if (Input.Pressed(MappedKeys.Start))
            {
                picker.CreateParty();
                SetNextScene(SceneManager.MapScene);
            }
            background.Color = Calc.MultiplyColors(picker.CurrentHero.Color, new Color(150, 150, 150));
            background.Offset += new Vector2(0.2f, 0.1f);
            pressStart.Offset.Y = waver.value;
        }

        public override void Render()
        {
            base.Render();


        }


    }
}
