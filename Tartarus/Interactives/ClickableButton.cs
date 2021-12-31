using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    class ClickableButton
    {
        public Vector2 Position { get; private set; }
        public GTexture Texture { get; private set; }
        public GTexture TexturePressed { get; private set; }
        private GTexture current;
        public BoxCollider Collider { get; private set; }
        public Action Action { get; private set; }
        private Color color;


        public ClickableButton(Vector2 pos, GTexture texture, Action action)
        {
            Position = pos;
            Texture = texture;
            current = Texture;
            TexturePressed = null;
            Action = action;
            color = Color.White;
            UpdateCollider();
        }

        public ClickableButton(Vector2 pos, GTexture texture, GTexture texturePressed, Action action)
        {
            Position = pos;
            Texture = texture;
            current = Texture;
            TexturePressed = texturePressed;
            Action = action;
            color = Color.White;
            UpdateCollider();
        }

        public void Update()
        {
            

            if (TexturePressed == null)
            {
                color = Color.LightGray;
                if (Collider.Collides(Input.MousePos) && Input.LeftMouseDown())
                    color = Color.DarkSlateGray;
                else if (Collider.Collides(Input.MousePos))
                    color = Color.White;
            }
            else
            {
                current = Texture;
                if (Collider.Collides(Input.MousePos) && Input.LeftMouseDown())
                    current = TexturePressed;
            }

            if (Input.LeftClick() && Collider.Collides(Input.MousePos))
                Action.Invoke();
        }

        public void Draw()
        {
            current.Draw(Position, color);
        }

        private void UpdateCollider()
        {
            Collider = new BoxCollider((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }



    }
}
