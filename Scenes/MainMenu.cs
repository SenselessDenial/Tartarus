using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tartarus
{
    public class MainMenu : Scene
    {
        private Entity logo;
        private Image logoImage;
        private Entity menu;
        private RadioButtonSetComponent rs;

        public MainMenu() 
            : base() { }

        public override void Begin()
        {
            base.Begin();
            FillColor = Color.Black;
            Camera.Scale = new Vector2(4);

            logo = new Entity(this, new Vector2(20, 20));
            logoImage = new Image(logo, new GTexture("paul_blart.png"));

            menu = new Entity(this, new Vector2(40, 40));
            rs = new RadioButtonSetComponent(menu, true);
            rs.Add(new Button(Drawing.SmallFont.FindTexture("start"), () => 
            { TartarusGame.Instance.Scene = SceneManager.CharacterSelect; }));
            rs.Add(new Button(Drawing.SmallFont.FindTexture("exit"), () => { Exit(); }));
        }

        public override void End()
        {
            base.End();
        }

        public override void Update()
        {
            base.Update();

            if (Input.Pressed(MappedKeys.Down))
                rs.MoveNext();
            if (Input.Pressed(MappedKeys.Up))
                rs.MovePrevious();
            if (Input.Pressed(MappedKeys.A) || Input.Pressed(MappedKeys.Start))
                rs.Invoke();
        }

        public override void Render()
        {
            base.Render();


        }
















    }
}
