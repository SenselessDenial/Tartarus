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
        public GTexture Texture
        {
            get => texture;
            set { texture = value; Alignment = alignment; }
        }

        private GTexture texture;

        public override int Width => Texture.Width;
        public override int Height => Texture.Height;

        public DrawAlignment Alignment
        {
            get => alignment;
            set
            {
                alignment = value;
                switch (alignment)
                {
                    case DrawAlignment.TopLeft:
                        Offset = new Vector2(0, 0);
                        break;
                    case DrawAlignment.TopCenter:
                        Offset = new Vector2(-Texture.Width / 2, 0f);
                        break;
                    case DrawAlignment.TopRight:
                        Offset = new Vector2(-Texture.Width, 0f);
                        break;
                    case DrawAlignment.CenterLeft:
                        Offset = new Vector2(0, -Texture.Height / 2);
                        break;
                    case DrawAlignment.Center:
                        Offset = new Vector2(-Texture.Width / 2, -Texture.Height / 2);
                        break;
                    case DrawAlignment.CenterRight:
                        Offset = new Vector2(-Texture.Width, -Texture.Height / 2);
                        break;
                    case DrawAlignment.BottomLeft:
                        Offset = new Vector2(0, -Texture.Height);
                        break;
                    case DrawAlignment.BottomCenter:
                        Offset = new Vector2(-Texture.Width / 2, -Texture.Height);
                        break;
                    case DrawAlignment.BottomRight:
                        Offset = new Vector2(-Texture.Width, -Texture.Height);
                        break;
                    default:
                        break;
                }
            }
        }
        private DrawAlignment alignment;

        

        public Image(Entity entity, GTexture texture, DrawAlignment alignment = DrawAlignment.TopLeft)
            : base(entity, false)
        {
            Texture = texture;
            Alignment = alignment;
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

    public enum DrawAlignment
    {
        TopLeft,
        TopCenter,
        TopRight,
        CenterLeft,
        Center,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }
}
