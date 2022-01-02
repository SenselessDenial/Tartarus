using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Tartarus
{
    /// <summary>
    /// Static class for drawing. All drawing goes through this SpriteBatch instance.
    /// </summary>
    static class Drawing
    {
        public static SpriteBatch SpriteBatch { get; private set; }
        public static Font SmallFont { get; private set; }

        public static PixelFont Font { get; private set; }
        private static GTexture Pixel { get; set; }

        public static void Initialize()
        {
            SpriteBatch = new SpriteBatch(TartarusGame.Instance.GraphicsDevice);
            Pixel = new GTexture(1, 1, Color.White);
            SmallFont = new Font("small_font.png", 4, 6);
            Font = new PixelFont("fontboxes.png", "newfont.xml");
        }

        public static void DrawPoint(Vector2 pos, Color color)
        {
            Pixel.Draw(pos, color);
        }

        public static void DrawBox(Rectangle rect, Color color)
        {
            Pixel.Draw(new Vector2(rect.X, rect.Y), color, new Vector2(rect.Width, rect.Height));
        }

        public static void DrawBoxBorder(Rectangle rect, Color color)
        {
            Pixel.Draw(new Vector2(rect.X, rect.Y), color, new Vector2(rect.Width, 1f));
            Pixel.Draw(new Vector2(rect.X, rect.Y), color, new Vector2(1f, rect.Height));
            Pixel.Draw(new Vector2(rect.X + rect.Width - 1, rect.Y), color, new Vector2(1f, rect.Height));
            Pixel.Draw(new Vector2(rect.X, rect.Y + rect.Height - 1), color, new Vector2(rect.Width, 1f));
        }

        public static void DrawBox(Rectangle rect, Color innerColor, Color borderColor)
        {
            DrawBox(rect, innerColor);
            DrawBoxBorder(rect, borderColor);
        }

        public static void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            float distance = Calc.Distance(start, end);
            float angle = Calc.Angle(start, end);
            Logger.Log(-angle);
            Pixel.Draw(start, color, angle, new Vector2(0, 0), new Vector2(distance, 1f));


        }





    }
}
