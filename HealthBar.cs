using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class HealthBar
    {
        public int Width;
        public int Height;
        public Color Color = Color.Red;
        public Color BackColor = Color.Black;
        public bool HasOutline = true;
        public float Percent
        {
            get => percent;
            set
            {
                if (value > 1f)
                    percent = 1f;
                else if (value < 0f)
                    percent = 0f;
                else
                    percent = value;
            }
        }

        private float percent = 1f;


        public HealthBar(int width, int height, Color color, Color backColor, bool hasOutline, float percent)
        {
            Width = width;
            Height = height;
            Color = color;
            BackColor = backColor;
            HasOutline = hasOutline;
            Percent = percent;
        }

        public HealthBar(int width, int height, float percent)
            : this(width, height, Color.Red, Color.Black, true, percent) { }

        public void Draw(Vector2 pos)
        {
            Drawing.DrawBox(new Rectangle((int)pos.X, (int)pos.Y, Width, Height), BackColor);
            Drawing.DrawBox(new Rectangle((int)pos.X, (int)pos.Y, (int)Math.Ceiling(Width * Percent), Height), Color);
            if (HasOutline)
                Drawing.DrawBoxBorder(new Rectangle((int)pos.X - 1, (int)pos.Y - 1, Width + 2, Height + 2), BackColor);
        }




    }
}
