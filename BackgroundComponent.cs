using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class BackgroundComponent : GraphicsComponent
    {
        public GTexture BGTexture { get; private set; }
        public override int Width => BGTexture.Width;
        public override int Height => BGTexture.Height;

        public new Entity Entity
        {
            get => entity;
            private set { entity = value; CalculateDrawingPositions(); }
        }
        private Entity entity;

        private Vector2 prevDrawingPosition;
        private List<Vector2> drawingPositions;

        public bool LoopX { get; private set; }
        public bool LoopY { get; private set; }

        private bool UpToDate => prevDrawingPosition == DrawingPosition;

        public BackgroundComponent(GTexture bGTexture, Vector2 offset, bool loopX, bool loopY) : base()
        {
            BGTexture = bGTexture;
            Offset = offset;
            LoopX = loopX;
            LoopY = loopY;

            prevDrawingPosition = DrawingPosition;
            drawingPositions = new List<Vector2>();
        }

        public BackgroundComponent(GTexture bGTexture)
            : this(bGTexture, new Vector2(0, 0), true, true) { }

        private List<float> FindPointsX()
        {
            List<float> points = new List<float>();
            if (IsOnScreen)
                points.Add(DrawingPosition.X);

            float temp = DrawingPosition.X;
            while (true)
            {
                temp += BGTexture.Width;
                if (temp > Scene.Camera.Viewport.Width + Scene.Camera.Viewport.X)
                    break;
                else
                    points.Add(temp);
            }

            temp = DrawingPosition.X;
            while (true)
            {
                temp -= BGTexture.Width;
                if (temp + BGTexture.Width < Scene.Camera.Viewport.X)
                    break;
                else
                    points.Add(temp);
            }

            return points;
        }
        private List<float> FindPointsY()
        {
            List<float> points = new List<float>();
            if (IsOnScreen)
                points.Add(DrawingPosition.Y);

            float temp = DrawingPosition.Y;
            while (true)
            {
                temp += BGTexture.Height;
                if (temp > Scene.Camera.Viewport.Height + Scene.Camera.Viewport.Y)
                    break;
                else
                    points.Add(temp);
            }

            temp = DrawingPosition.Y;
            while (true)
            {
                temp -= BGTexture.Height;
                if (temp + BGTexture.Height < Scene.Camera.Viewport.Y)
                    break;
                else
                    points.Add(temp);
            }

            return points;
        }

        private void CalculateDrawingPositions()
        {
            drawingPositions.Clear();
            List<float> xPos = new List<float>();
            List<float> yPos = new List<float>();

            if (LoopX)
                xPos = FindPointsX();
            else
                if (IsOnScreen)
                    xPos.Add(DrawingPosition.X);

            if (LoopY)
                yPos = FindPointsY();
            else
                if (IsOnScreen)
                    yPos.Add(DrawingPosition.Y);

            drawingPositions = Calc.FindAllPermutations(xPos, yPos);
        }

        public override void Update()
        {
            if (!UpToDate)
                CalculateDrawingPositions();

            prevDrawingPosition = DrawingPosition;
        }

        public override void Render()
        {
            //Logger.Log(drawingPositions.Count);

            foreach (var pos in drawingPositions)
                BGTexture.Draw(pos);
        }



    }
}
