using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    class SelectionMatrix : GraphicsComponent
    {
        private Button[,] matrix;

        public PixelFont.Alignment Alignment = PixelFont.Alignment.Center;

        public new int Width;
        public new int Height;
        public int XSpacing;
        public int YSpacing;

        public Point FocusPoint = new Point(0, 0);

        public Point CurrentPoint
        {
            get => currentPoint;
            set
            {
                if (CurrentButton != null)
                    CurrentButton.IsSelected = false;

                if (value.X < 0)
                    value.X = 0;
                else if (value.X >= Width)
                    value.X = Width - 1;

                if (value.Y < 0)
                    value.Y = 0;
                else if (value.Y >= Height)
                    value.Y = Height - 1;

                currentPoint = value;
                CurrentButton.IsSelected = true;
            }
        }

        private Point currentPoint;

        private Button CurrentButton => matrix[CurrentPoint.X, CurrentPoint.Y];

        public SelectionMatrix(Entity entity, int width, int height, int xSpacing, int ySpacing) 
            : base(entity, true) 
        {
            matrix = new Button[width, height];
            Width = width;
            Height = height;
            XSpacing = xSpacing;
            YSpacing = ySpacing;
        }

        private int Count
        {
            get
            {
                int count = 0;
                for (int i = 0; i < Width; i++)
                    for (int j = 0; j < Height; j++)
                        if (matrix[i, j] != null)
                            count++;
                return count;
            }
        }

        public void Add(int x, int y, Button button)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                Logger.Log("Index is out of range. Cannot add the button.");
                return;
            }
            matrix[x, y] = button;
            if (Count == 1)
            {
                CurrentPoint = new Point(x, y);
                button.IsSelected = true;
            }
                
        }

        public void Add(int index, Button button)
        {
            Add(index % Width, index / Width, button);
        }

        public void Add(params Button[] buttons)
        {
            int index = 0;
            foreach (var button in buttons)
            {
                Add(index, button);
                index++;
            }       
        }

        public void Clear()
        {
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    matrix[i, j] = null;
        }

        public void MoveUp()
        {
            if (CurrentButton == null)
            {
                Logger.Log("Current button is null. Cannot move.");
                return;
            }

            Point temp = CurrentPoint;
            for (int i = 0; i < Height; i++)
            {
                temp.Y++;
                if (temp.Y >= Height)
                    temp.Y = 0;

                for (int j = 0; j < Width; j++)
                {
                    if (matrix[temp.X, temp.Y] != null)
                    {
                        if (matrix[temp.X, temp.Y].IsSelectable)
                        {
                            CurrentPoint = temp;
                            return;
                        }
                    }

                    temp.X++;
                    if (temp.X >= Width)
                        temp.X = 0;
                }

            }

        }

        public void MoveDown()
        {
            if (CurrentButton == null)
            {
                Logger.Log("Current button is null. Cannot move.");
                return;
            }

            Point temp = CurrentPoint;
            for (int i = 0; i < Height; i++)
            {
                temp.Y--;
                if (temp.Y < 0)
                    temp.Y = Height - 1;

                for (int j = 0; j < Width; j++)
                {
                    if (matrix[temp.X, temp.Y] != null)
                    {
                        if (matrix[temp.X, temp.Y].IsSelectable)
                        {
                            CurrentPoint = temp;
                            return;
                        }
                    }

                    temp.X++;
                    if (temp.X >= Width)
                        temp.X = 0;
                }

            }
        }

        public void MoveRight()
        {
            if (CurrentButton == null)
            {
                Logger.Log("Current button is null. Cannot move.");
                return;
            }

            Point temp = CurrentPoint;
            for (int i = 0; i < Width; i++)
            {
                temp.X++;
                if (temp.X >= Width)
                    temp.X = 0;

                for (int j = 0; j < Height; j++)
                {
                    if (matrix[temp.X, temp.Y] != null)
                    {
                        if (matrix[temp.X, temp.Y].IsSelectable)
                        {
                            CurrentPoint = temp;
                            return;
                        }
                    }

                    temp.Y++;
                    if (temp.Y >= Height)
                        temp.Y = 0;
                }


               
            }
        }

        public void MoveLeft()
        {
            if (CurrentButton == null)
            {
                Logger.Log("Current button is null. Cannot move.");
                return;
            }

            Point temp = CurrentPoint;
            for (int i = 0; i < Width; i++)
            {
                temp.X--;
                if (temp.X < 0)
                    temp.X = Width - 1;

                for (int j = 0; j < Height; j++)
                {
                    if (matrix[temp.X, temp.Y] != null)
                    {
                        if (matrix[temp.X, temp.Y].IsSelectable)
                        {
                            CurrentPoint = temp;
                            return;
                        }
                    }

                    temp.Y++;
                    if (temp.Y >= Height)
                        temp.Y = 0;
                }
            }
        }

        public void Invoke()
        {
            if (CurrentButton == null)
            {
                Logger.Log("Current button is null. Cannot invoke.");
                return;
            }
            CurrentButton?.Invoke();
        }

        public override void Update()
        {
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    matrix[i, j]?.Update();
        }

        public override void Render()
        {
            Vector2 startPos = DrawingPosition - new Vector2(FocusPoint.X * XSpacing, FocusPoint.Y * YSpacing);
            Vector2 currPos = DrawingPosition - new Vector2(FocusPoint.X * XSpacing, FocusPoint.Y * YSpacing);
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    matrix[i, j]?.Draw(currPos, Alignment);
                    currPos.Y += YSpacing;
                }
                currPos.Y = startPos.Y;
                currPos.X += XSpacing;
            }
        }














    }
}
