using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class Camera
    {
        // needs work!
        
        public Renderer Renderer { get; private set; }
        public Matrix Matrix { get; private set; }

        public Vector2 Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                UpdateMatrix();
            }
        }
        private Vector2 scale;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                UpdateMatrix();
            }
        }
        private Vector2 position;


        public Camera(Renderer renderer)
        {
            Renderer = renderer;
            Matrix = Matrix.Identity;
            Scale = Vector2.One;
            Position = Vector2.Zero;
        }

        public void UpdateMatrix()
        {
            Matrix translate = Matrix.CreateTranslation(-Position.X, -Position.Y, 0f);
            Matrix scale = Matrix.CreateScale(Scale.X, Scale.Y, 1f);

            Matrix = scale * translate * Matrix.Identity;
        }

        public void Translate(float x, float y)
        {
            Position = new Vector2(Position.X + x, Position.Y + y);
        }





    }
}
