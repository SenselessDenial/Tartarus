using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tartarus
{
    public class GTexture : IDisposable
    {
        public Texture2D Texture { get; private set; }
        public Rectangle ClipRect { get; private set; }
        public int Width => ClipRect != null ? ClipRect.Width : Texture.Width;
        public int Height => ClipRect != null ? ClipRect.Height : Texture.Height;

        public GTexture(string file)
        {
            var filename = Path.Combine(TartarusGame.ResourceFolderPath, file);
            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            Texture2D texture = Texture2D.FromStream(TartarusGame.Instance.GraphicsDevice, fileStream);
            fileStream.Close();
            Texture = texture;
            Texture.Name = file;
            ClipRect = new Rectangle(0, 0, Texture.Width, Texture.Height);
        }

        public GTexture(GTexture parent, int x, int y, int width, int height)
        {
            Texture = parent.Texture;
            ClipRect = new Rectangle(x, y, width, height);
        }

        public GTexture(int width, int height, Color color)
        {
            Texture = new Texture2D(TartarusGame.Instance.GraphicsDevice, width, height);
            var colors = new Color[width * height];
            for (int i = 0; i < width * height; i++)
                colors[i] = color;
            Texture.SetData(colors);
            ClipRect = new Rectangle(0, 0, Texture.Width, Texture.Height);
        } 
        
        public void Draw(Vector2 pos, Color color, float rotation, Vector2 origin, Vector2 scale)
        {
            Drawing.SpriteBatch.Draw(Texture, pos, ClipRect, color, rotation, origin, scale, SpriteEffects.None, 0f);
        }

        public void Draw(Vector2 pos, Color color)
        {
            Draw(pos, color, 0f, new Vector2(0, 0), new Vector2(1, 1));
        }

        public void Draw(Vector2 pos, Color color, Vector2 scale)
        {
            Draw(pos, color, 0f, new Vector2(0, 0), scale);
        }

        public void Draw(Vector2 pos)
        {
            Draw(pos, Color.White, 0f, new Vector2(0, 0), new Vector2(1, 1));
        }

        public void Dispose()
        {
            Texture.Dispose();
        }
    }
}
