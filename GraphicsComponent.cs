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
        public virtual int Width { get; }
        public virtual int Height { get; }
        protected Vector2 DrawingPosition => Entity != null ? Entity.Position + Offset : Offset;

        public GraphicsComponent(Entity entity, bool isActive)
            : base(entity, isActive, true) 
        {
            Offset = Vector2.Zero;
        }

        public GraphicsComponent() 
            : this(null, false) { }


    }
}
