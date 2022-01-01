using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class RadioButtonSetComponent : GraphicsComponent
    {
        public int Spacing { get; private set; }

        private List<Button> buttons;
        private Button current;
        public bool IsVertical { get; private set; }
        public int Count => buttons.Count;
        private int CurrentIndex => buttons.IndexOf(current);

        public override int Width
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

        public override int Height
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

        


        public RadioButtonSetComponent(Entity entity, bool isVertical) : base(entity, true)
        {
            buttons = new List<Button>();
            Spacing = 1;
            Offset = Vector2.Zero;
            IsVertical = isVertical;
        }

        public void Add(Button button)
        {
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

        public void Clear()
        {
            buttons.Clear();
            current = null;
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

        public override void Update()
        {
            foreach (var item in buttons)
                item.Update();
        }

        public override void Render()
        {
            Vector2 currentPos = DrawingPosition;

            if (IsVertical)
            {
                foreach (var item in buttons)
                {
                    item.Draw(currentPos);
                    currentPos.Y += item.Height + Spacing;
                }
            }
            else
            {
                foreach (var item in buttons)
                {
                    item.Draw(currentPos);
                    currentPos.X += item.Width + Spacing;
                }
            }
  
        }





    }
}
