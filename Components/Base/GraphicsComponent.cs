using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class GraphicsComponent : Component
    {
        public Vector2 Offset;
        public Color Color = Color.White;
        public virtual int Width { get; }
        public virtual int Height { get; }
        internal Vector2 DrawingPosition => (Entity != null) ? Entity.Position + Offset : Offset;
        public Rectangle Bounds => new Rectangle((int)DrawingPosition.X, (int)DrawingPosition.Y, Width, Height);
        public bool IsOnScreen => (Scene != null) ? Bounds.Intersects(new Rectangle(Scene.Camera.Viewport.X - 1, Scene.Camera.Viewport.Y - 1, Scene.Camera.Viewport.Width + 2, Scene.Camera.Viewport.Height + 2)) : false;

        public GraphicsComponent(Entity entity, bool isActive)
            : base(entity, isActive, true) 
        {
            Offset = Vector2.Zero;
        }

        public GraphicsComponent() 
            : this(null, true) { }




    }
}
