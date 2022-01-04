using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class BoxCollider
    {
        public Rectangle Bounds { get; set; }
        public int Width => Bounds.Width;
        public int Height => Bounds.Height;

        public BoxCollider(Rectangle bounds)
        {
            Bounds = bounds;
        }

        public BoxCollider(int x, int y, int width, int height)
            : this(new Rectangle(x, y, width, height)) { }

        public bool Collides(Vector2 point)
        {
            return Bounds.Contains(point);
        }

        public bool Collides(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }

        public bool Collides(BoxCollider collider)
        {
            return Bounds.Intersects(collider.Bounds);
        }


    }
}
