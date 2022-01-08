using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class Button
    {
        public GTexture Texture { get; private set; }
        public string Message { get; private set; }
        public Action Action { get; private set; }
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                color = isSelected ? Color.White : unselectedColor;
            }
        }
        public bool IsSelectable
        {
            get => isSelectable;
            set
            {
                isSelectable = value;
                if (!isSelectable)
                    color = unselectableColor;
            }
        }

        private bool isSelected = false;
        private bool isSelectable = true;


        private Color color;
        private static Color unselectedColor = new Color(180, 180, 200);
        private static Color unselectableColor = new Color(100, 100, 100);
        public int Width => Message != null ? Drawing.Font.MeasureX(Message) : Texture.Width;
        public int Height => Message != null ? 10 : Texture.Height;



        public Button(GTexture texture, bool isSelectable, Action action)
        {
            Texture = texture;
            Action = action;
            IsSelected = false;
            IsSelectable = isSelectable;
        }

        public Button(GTexture texture, Action action) 
            : this(texture, true, action) { }

        public Button(string message, Action action)
        {
            Message = message;
            Action = action;
            IsSelected = false;
            IsSelectable = true;
        }

        public void Invoke()
        {
            Action.Invoke();
        }

        public void Update()
        {
            color = unselectedColor;
            if (!IsSelectable)
                color = unselectableColor;
            else if (IsSelected)
                color = Color.White;
        }

        public void Draw(Vector2 position)
        {
            if (Message != null)
                Drawing.Font.DrawOutline(Message, position, color);
            else
                Texture.Draw(position, color);
        }

        public void Draw(Vector2 position, PixelFont.Alignment alignment)
        {
            if (Message != null)
                Drawing.Font.DrawOutline(Message, position, color, alignment);
        }


    }
}
