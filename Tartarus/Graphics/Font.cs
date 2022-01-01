using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class Font
    {
        private Dictionary<int, GTexture> characters;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Font(string filename, int width, int height)
        {
            characters = new Dictionary<int, GTexture>();
            Width = width;
            Height = height;

            Tileset sheet = new Tileset(filename, width, height);

            characters.Add(' ', sheet[0]);
            for (int i = 0; i <= 11; i++)
            {
                
                characters.Add('/' + i, sheet[i + 1]); 
            }
            for (int i = 0; i <= 25; i++)
            {
                characters.Add('A' + i, sheet[i + 13]);
            }
        }

        public void Draw(string message, Vector2 pos)
        {
            Vector2 offset = Vector2.Zero;
            for (int i = 0; i < message.Length; i++)
            {
                char current = message[i];
                if (current == '\n')
                {
                    offset.Y += Height;
                    offset.X = 0;
                }
                else
                {
                    Draw(current, pos + offset);
                    offset.X += Width;
                }

            }
        }

        public void Draw(char character, Vector2 pos)
        {
            if (characters.ContainsKey(char.ToUpper(character)))
            {
                characters[char.ToUpper(character)].Draw(pos);
            }
            else
            {
                Logger.Log("Char not found.");
            }
        }

        public void Draw(object obj, Vector2 pos)
        {
            Draw(obj.ToString(), pos);
        }

        public GTexture FindTexture(string message)
        {
            BoxCollider tempBox = FindCollider(message, new Vector2(0, 0));

            GTexture temp = new GTexture(tempBox.Width, tempBox.Height, Color.Transparent);
            var colors = new Color[temp.Width * temp.Height];
            temp.Texture.GetData(colors);
            

            Vector2 pos = Vector2.Zero;

            for (int i = 0; i < message.Length; i++)
            {
                var currentChar = char.ToUpper(message[i]); 
                if (currentChar == '\n')
                {
                    pos.X = 0;
                    pos.Y += Height;
                    continue;
                }

                var current = characters[currentChar];
                var currColors = new Color[current.Texture.Width * current.Texture.Height];
                current.Texture.GetData(currColors);
                


                for (int j = 0; j < current.Width; j++)
                {
                    for (int k = 0; k < current.Height; k++)
                    {
                        var currColor = currColors[(k + current.ClipRect.Y) * current.Texture.Width + (j + current.ClipRect.X)];

                        colors[(j + (int)pos.X) + ((k + (int)pos.Y) * temp.Width)] = currColor;
                    }
                }
                pos.X += Width;
            }

            temp.Texture.SetData(colors);

            return temp;
        }


        public BoxCollider FindCollider(string message, Vector2 pos)
        {
            int x = (int)pos.X;
            int y = (int)pos.Y;
            int width = 0;
            int height = Height;
            int tempWidth = 0;

            for (int i = 0; i < message.Length; i++)
            {
                char current = message[i];
                if (current == '\n')
                {
                    height += Height;
                    if (tempWidth > width)
                        width = tempWidth;

                    tempWidth = 0;
                }
                else
                {
                    tempWidth += Width;
                }
            }
            if (tempWidth > width)
                width = tempWidth;

            return new BoxCollider(x, y, width, height);
        }

    }
}
