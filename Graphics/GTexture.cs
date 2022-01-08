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

        public GTexture(GTexture parent, Rectangle clipRect)
        {
            Texture = parent.Texture;
            ClipRect = clipRect;
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

        public GTexture Copy()
        {
            GTexture temp = new GTexture(Width, Height, Color.HotPink);
            var colors = new Color[Texture.Width * Texture.Height];
            Texture.GetData(colors);
            var newColors = new Color[Width * Height];

            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    newColors[i + (j * Width)] = colors[ClipRect.X + i + (ClipRect.Y + j) * Texture.Width];
            temp.Texture.SetData(newColors);
            temp.ClipRect = new Rectangle(0, 0, Width, Height);
            return temp;
        }

        private Color[] GetColors()
        {
            var colors = new Color[Texture.Width * Texture.Height];
            Texture.GetData(colors);
            return colors;
        }


        public void Draw(Vector2 pos, Color color, float rotation, Vector2 origin, Vector2 scale)
        {
            Drawing.SpriteBatch.Draw(Texture, pos, ClipRect, color, rotation, origin, scale, SpriteEffects.None, 0f);
        }

        public void Draw(Vector2 pos, Rectangle rect, Color color)
        {
            Drawing.SpriteBatch.Draw(Texture, pos, new Rectangle(ClipRect.X + rect.X, ClipRect.Y + rect.Y, rect.Width, rect.Height), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public void Draw(Vector2 pos, Vector2 origin, Color color)
        {
            Draw(pos, color, 0f, origin, new Vector2(1, 1));
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

        public void DrawOutline(Vector2 pos, Color color)
        {
            
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                {
                    Draw(pos + new Vector2(i, j), Color.Black);
                }

            Draw(pos, color);
        }

        public void FillArea(Rectangle area, Color color)
        {
            Vector2 drawingPos = new Vector2(area.X, area.Y);
            int height = Height;
            while (true)
            {
                if (drawingPos.Y >= area.Y + area.Height)
                    break;

                if (drawingPos.Y + Height > area.Y + area.Height)
                    height = area.Y + area.Height - (int)drawingPos.Y;

                while (true)
                {
                    if (drawingPos.X + Width <= area.X + area.Width)
                        Draw(drawingPos, new Rectangle(0, 0, Width, height), color);
                    else
                    {
                        Draw(drawingPos, new Rectangle(0, 0, area.X + area.Width - (int)drawingPos.X, height), color);
                        break;
                    }
                    drawingPos.X += Width;
                }
                drawingPos.X = area.X;
                drawingPos.Y += Height;
            }
        }

        public static GTexture FromParent(GTexture parent, Rectangle clipRect)
        {
            return new GTexture(parent, clipRect);
        }

        public GTexture GetSubtexture(int x, int y, int width, int height)
        {
            return new GTexture(this, x, y, width, height);
        }

        public GTexture ReplaceWithPattern(GTexture pattern, Color colorToReplace)
        {
            GTexture temp = Copy();
            var colors = temp.GetColors();
            GTexture patternTemp = pattern.Copy();
            var patternColors = patternTemp.GetColors();

            for (int i = 0; i < temp.Width; i++)
                for (int j = 0; j < temp.Height; j++)
                    if (colors[i + j * temp.Width] == colorToReplace)
                        colors[i + (j * temp.Width)] = patternColors[i % patternTemp.Width + (j % patternTemp.Height) * patternTemp.Width];
            temp.Texture.SetData(colors);
            return temp;
        }

        public GTexture ReplaceWithColor(Color newColor, Color colorToReplace)
        {
            GTexture temp = Copy();
            var colors = temp.GetColors();

            for (int i = 0; i < temp.Width; i++)
                for (int j = 0; j < temp.Height; j++)
                    if (colors[i + j * temp.Width] == colorToReplace)
                        colors[i + (j * temp.Width)] = newColor;
            temp.Texture.SetData(colors);
            return temp;
        }

        public void Dispose()
        {
            Texture.Dispose();
        }
    }
}
