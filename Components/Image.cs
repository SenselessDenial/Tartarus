using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class Image : GraphicsComponent, IDisposable
    {
        public GTexture Texture { get; private set; }

        public override int Width => Texture.Width;
        public override int Height => Texture.Height;

        public Image(Entity entity, GTexture texture)
            : base(entity, false)
        {
            Texture = texture;
        }

        public Image(GTexture texture)
            : this(null, texture) { }

        public Image(string filename)
            : this(new GTexture(filename)) { }

        public override void Render()
        {
            Texture.Draw(DrawingPosition, Color);
        }

        public void Dispose()
        {
            Texture.Dispose();
        }
    }
}
