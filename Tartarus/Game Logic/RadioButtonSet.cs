using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    class RadioButtonSet : IDrawUpdatable
    {
        public Vector2 Position { get; private set; }
        public int Spacing { get; private set; }

        private List<Button> buttons;
        private Button current;
        public bool IsVertical { get; private set; }
        public int Count => buttons.Count;
        private int CurrentIndex => buttons.IndexOf(current);

        public bool IsVisible = true;

        public int Width
        {
            get
            {
                int width = 0;

                if (IsVertical)
                {
                    foreach (var item in buttons)
                        if (item.Width > width)
                            width = item.Width;
                }
                else
                {
                    foreach (var item in buttons)
                        width += item.Width + Spacing;
                    width -= Spacing;
                }
                return width;
            }
        }

        public int Height
        {
            get
            {
                int height = 0;

                if (IsVertical)
                {
                    foreach (var item in buttons)
                        height += item.Height + Spacing;
                    height -= Spacing;
                }
                else
                {
                    foreach (var item in buttons)
                        if (item.Height > height)
                            height = item.Height;
                }
                return height;
            }
        }

        public void Clear()
        {
            buttons.Clear();
            current = null;
        }

        public RadioButtonSet(Vector2 pos, bool isVertical)
        {
            Position = pos;
            buttons = new List<Button>();
            current = null;
            Spacing = 1;
            IsVertical = isVertical;
        }

        public void Add(Button button)
        {
            button.Position = Position;

            if (IsVertical)
                foreach (var item in buttons)
                    button.Position.Y += item.Height + Spacing;
            else
                foreach (var item in buttons)
                    button.Position.X += item.Width + Spacing;

            buttons.Add(button);
            if (current == null)
            {
                current = button;
                current.IsSelected = true;
            }
        }

        public void Add(params Button[] buttons)
        {
            foreach (var item in buttons)
                Add(item);
        }

        public void MovePrevious()
        {
            if (current == null)
            {
                Logger.Log("Can't move down cause current button is null.");
                return;
            }
            

            current.IsSelected = false;
            int index = CurrentIndex;
            for (int i = 0; i < buttons.Count; i++)
            {
                index++;
                if (index >= Count)
                    index = 0;

                if (buttons[index].IsSelectable == true)
                {
                    current = buttons[index];
                    current.IsSelected = true;
                    return;
                }
            }
            Logger.Log("There are no selectable buttons in this set.");
        }

        public void MoveNext()
        {
            if (current == null)
            {
                Logger.Log("Can't move up cause current button is null.");
                return;
            }
            
            current.IsSelected = false;
            int index = CurrentIndex;
            for (int i = 0; i < buttons.Count; i++)
            {
                index--;
                if (index < 0)
                    index = buttons.Count - 1;

                if (buttons[index].IsSelectable == true)
                {
                    current = buttons[index];
                    current.IsSelected = true;
                    return;
                }
            }
            Logger.Log("There are no selectable buttons in this set.");
        }

        public void Invoke()
        {
            if (current != null && current.IsSelectable)
                current.Invoke();
            else if (current == null)
                Logger.Log("Can't Invoke because current button is null.");
            else
                Logger.Log("Can't invoke because current button is not selectable.");
        }

        public void Update()
        {
            foreach (var item in buttons)
                item.Update();
        }

        public void Draw()
        {
            if (!IsVisible)
                return;

            foreach (var item in buttons)
                item.Draw();
        }
    }
}
