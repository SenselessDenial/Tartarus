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
            im = new Image(entity, new GTexture("buttonset.png"));
            im.Offset = new Vector2(-im.Width / 2, -im.Height / 2);
            t = new TimingDiagram(entity);
            t.Add(0, 1, 2, Ease.BounceOut);
            t.AddMaintain(1);
            t.Add(1, 0, 2, Ease.BounceOut);
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

            im.Color = new Color(255, 255, 255, (int)(t.Value * 255));

            if (Input.PressedAnyMappedKey())
                SetNextScene(SceneManager.MainMenu);
            
        }

        public override void Render()
        {
            base.Render();


        }


    }
}
