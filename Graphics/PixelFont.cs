using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tartarus
{
    public class PixelFont
    {
        //Implement this!
        public enum FontStyle
        {
            CaseSensitive,
            NotCaseSensitive
        }

        //Add justify if desired
        public enum Alignment
        {
            Left,
            Center,
            Right
        }

        public const int ErrorValue = -1;

        private List<GTexture> references;
        public Dictionary<int, PixelFontCharacter> Characters;

        private PixelFont()
        {
            references = new List<GTexture>();
            Characters = new Dictionary<int, PixelFontCharacter>();
        }

        public PixelFont(GTexture texture, string xmlFile) : this()
        {
            references.Add(texture);
            XmlDocument doc = Calc.XmlFromString(Path.Combine(TartarusGame.ResourceFolderPath, xmlFile));

            PixelFontCharacter temp = null;
            foreach (XmlElement character in doc.DocumentElement.ChildNodes)
            {
                temp = new PixelFontCharacter(texture, character);
                Characters.Add(temp.Character, temp);
            }
        }

        public PixelFont(string textureFile, string xmlFile) : this()
        {
            GTexture texture = new GTexture(textureFile);
            references.Add(texture);
            XmlDocument doc = Calc.XmlFromString(Path.Combine(TartarusGame.ResourceFolderPath, xmlFile));

            PixelFontCharacter temp = null;
            foreach (XmlElement character in doc.DocumentElement.ChildNodes)
            {
                temp = new PixelFontCharacter(texture, character);
                Characters.Add(temp.Character, temp);
            }


        }

        public PixelFontCharacter Get(int id)
        {
            if (Characters.TryGetValue(id, out PixelFontCharacter val))
                return val;
            return null;
        }

        public bool TryGetValue(char key, out PixelFontCharacter value)
        {
            if (Characters.TryGetValue(key, out PixelFontCharacter temp) == false)
            {
                Characters.TryGetValue(ErrorValue, out value);
                return false;
            }
            value = temp;
            return true;
        }

        public int MeasureX(char value)
        {
            PixelFontCharacter temp;
            if (TryGetValue(value, out temp))
            {
                return temp.XAdvance;
            }
            return 0;
        }

        public int MeasureX(string value)
        {
            int count = 0;

            for (int i = 0; i < value.Length; i++)
            {
                count += MeasureX(value[i]);
            }
            return count;
        }

        public void Draw(char value, Vector2 position, Color color)
        {
            PixelFontCharacter toDraw;
            TryGetValue(value, out toDraw);
            toDraw.Texture.Draw(position, color);
        }

        public void Draw(char value, Vector2 position)
        {
            Draw(value, position, Color.White);
        }

        public void Draw(string value, Vector2 position, Color color, Alignment alignment = Alignment.Left)
        {
            Vector2 currentPos;
            int size = MeasureX(value);
            switch (alignment)
            {
                case Alignment.Left:
                    currentPos = position;
                    break;
                case Alignment.Right:
                    currentPos = new Vector2(position.X - size, position.Y);
                    break;
                case Alignment.Center:
                    currentPos = new Vector2(position.X - (size / 2), position.Y);
                    break;
                default:
                    currentPos = position;
                    break;
            }

            char currentChar;
            PixelFontCharacter toDraw;

            for (var i = 0; i < value.Length; i++)
            {
                currentChar = value[i];
                TryGetValue(currentChar, out toDraw);
                if (toDraw != null)
                {
                    toDraw.Texture.Draw(new Vector2(currentPos.X - toDraw.XOffset, currentPos.Y - toDraw.YOffset), color);
                    currentPos.X += toDraw.XAdvance;
                }
            }
        }


        public void Draw(string value, Vector2 position)
        {
            Draw(value, position, Color.White, Alignment.Left);
        }

        public void DrawOutline(string value, Vector2 position, Color color, Alignment alignment = Alignment.Left)
        {
            Vector2 currentPos;
            int size = MeasureX(value);
            switch (alignment)
            {
                case Alignment.Left:
                    currentPos = position;
                    break;
                case Alignment.Right:
                    currentPos = new Vector2(position.X - size, position.Y);
                    break;
                case Alignment.Center:
                    currentPos = new Vector2(position.X - (size / 2), position.Y);
                    break;
                default:
                    currentPos = position;
                    break;
            }

            char currentChar;
            PixelFontCharacter toDraw;

            for (var i = 0; i < value.Length; i++)
            {
                currentChar = value[i];
                TryGetValue(currentChar, out toDraw);
                if (toDraw != null)
                {
                    toDraw.Texture.DrawOutline(new Vector2(currentPos.X - toDraw.XOffset, currentPos.Y - toDraw.YOffset), color);
                    currentPos.X += toDraw.XAdvance;
                }
            }
        }

    }

    /// <summary>
    /// Represenetation of a character. Use Unicode decimal value for id.
    /// </summary>
    public class PixelFontCharacter
    {
        public int Character { get; private set; }
        public int XAdvance { get; private set; }
        public int XOffset { get; private set; }
        public int YOffset { get; private set; }
        public GTexture Texture { get; private set; }

        public PixelFontCharacter(int character, GTexture texture)
        {
            Character = character;
            Texture = texture;
        }

        public PixelFontCharacter(int character, GTexture texture, Rectangle clipRect)
        {
            Character = character;
            Texture = GTexture.FromParent(texture, clipRect);
        }

        public PixelFontCharacter(int character, GTexture texture, XmlElement xml)
        {
            Character = character;
            Texture = texture.GetSubtexture(xml.AttrInt("x"), xml.AttrInt("y"), xml.AttrInt("width"), xml.AttrInt("height"));
        }

        public PixelFontCharacter(GTexture texture, XmlElement xml)
        {
            Character = xml.AttrInt("id");
            Texture = texture.GetSubtexture(xml.AttrInt("x"), xml.AttrInt("y"), xml.AttrInt("width"), xml.AttrInt("height"));
            XAdvance = xml.AttrInt("xadvance");
            XOffset = xml.AttrInt("xoffset");
            YOffset = xml.AttrInt("yoffset");
        }
    }

}