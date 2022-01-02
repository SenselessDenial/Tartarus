using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class CharacterSelect : Scene
    {
        private CharacterPicker picker;
        private Entity pressStart;



        public CharacterSelect() : base()
        {
            // DO NOT INITALIZE OBJECTS HERE!
        }

        public override void Begin()
        {
            base.Begin();
            FillColor = Color.LightSalmon;
            Camera.Scale = new Vector2(4);

            HeroPresets.Begin();

            picker = new CharacterPicker(this);
            picker.Position = new Vector2(20, 20);
            picker.Add(HeroPresets.Anna);
            picker.Add(HeroPresets.Sophie);
            picker.Add(HeroPresets.Eva);
            picker.Add(HeroPresets.Madeline);
            picker.Add(HeroPresets.Gabriel);

            pressStart = new Entity(this);
            pressStart.Position = new Vector2(Camera.Width / 2, Camera.Height - 20);
            pressStart.Add(new Text("PRESS START TO BEGIN", Color.White, true, PixelFont.Alignment.Center));

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
            }

        }

        public override void Render()
        {
            base.Render();


        }


    }
}
