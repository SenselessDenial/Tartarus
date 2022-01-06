using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class TitleScreenScene : Scene
    {
        Entity entity;
        TimingDiagram t;
        Image im;
        GSong gs;

        public TitleScreenScene() : base()
        {
            // DO NOT INITALIZE OBJECTS HERE!
        }

        public override void Begin()
        {
            base.Begin();
            FillColor = Color.Black;
            Camera.Scale = new Vector2(4);

            entity = new Entity(this);
            entity.Position = new Vector2(Camera.Width / 2, Camera.Height / 2);
            im = new Image(entity, new GTexture("fazbeargaming.png"), Image.DrawAlignment.Center);
            t = new TimingDiagram(entity);
            t.Add(0, 255, 3, Ease.SineOut);
            t.AddMaintain(1);
            t.Add(255, 0, 3, Ease.SineIn);
            t.AddMaintain(1);
            t.AddAction(() => { im.Texture = new GTexture("fartholomew_mini.png"); });
            t.Add(0, 255, 3, Ease.SineOut);
            t.AddMaintain(1);
            t.Add(255, 0, 3, Ease.SineIn);
            t.AddMaintain(1);
            t.AddAction(() => { SetNextScene(SceneManager.MainMenu); });
            t.Start();
        }

        public override void End()
        {
            base.End();


        }

        public override void Update()
        {
            base.Update();

            im.Color = new Color(255, 255, 255, (int)t.Value);

            if (Input.PressedAnyMappedKey())
                SetNextScene(SceneManager.MainMenu);
            
        }

        public override void Render()
        {
            base.Render();


        }


    }
}
