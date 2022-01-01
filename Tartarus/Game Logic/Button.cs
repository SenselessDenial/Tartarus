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
        public Action Action { get; private set; }
        public bool IsSelected;
        public bool IsSelectable;

        private Color color;
        private static Color unselectedColor = new Color(180, 180, 200);
        private static Color unselectableColor = new Color(100, 100, 100);
        public int Width => Texture.Width;
        public int Height => Texture.Height;


        public Button(GTexture texture, bool isSelectable, Action action)
        {
            Texture = texture;
            Action = action;
            IsSelected = false;
            IsSelectable = isSelectable;
            color = unselectedColor;
        }

        public Button(GTexture texture, Action action) 
            : this(texture, true, action) { }

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
            Texture.Draw(position, color);
        }


    }
}
