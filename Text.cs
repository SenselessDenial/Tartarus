using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Tartarus.PixelFont;

namespace Tartarus
{
    public class Text : GraphicsComponent
    {
        public string Message = "";
        public Color Color = Color.White;
        public bool IsOutlined = false;
        public Alignment Alignment = Alignment.Left;
         
        public Text(Entity entity, string message, Vector2 offset, Color color, bool isOutlined, Alignment alignment) 
            : base(entity, false) 
        {
            Offset = offset;
            Message = message;
            Color = color;
            IsOutlined = isOutlined;
            Alignment = alignment;
        }

        public Text(string message, Vector2 offset, Color color, bool isOutlined, Alignment alignment)
            : this(null, message, offset, color, isOutlined, alignment) { }

        public Text(string message, Color color, bool isOutlined)
            : this(message, new Vector2(0, 0), color, isOutlined, Alignment.Left) { }

        public override void Render()
        {
            base.Render();

            if (IsOutlined)
                Drawing.Font.DrawOutline(Message, DrawingPosition, Color, Alignment);
            else
                Drawing.Font.Draw(Message, DrawingPosition, Color, Alignment);

        }




    }
}
