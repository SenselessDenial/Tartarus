using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    public class Background : GraphicsComponent
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

        public Background(Entity entity, GTexture bGTexture, Vector2 offset, bool loopX, bool loopY) : base()
        {
            drawingPositions = new List<Vector2>();
            BGTexture = bGTexture;
            Offset = offset;
            LoopX = loopX;
            LoopY = loopY;
            entity?.Add(this);

            prevDrawingPosition = DrawingPosition;
        }

        public Background(Entity entity, GTexture bGTexture)
            : this(entity, bGTexture, new Vector2(0,0), true, true) { }

        public Background(GTexture bGTexture)
            : this(null, bGTexture, new Vector2(0, 0), true, true) { }

        private List<float> FindPointsX()
        {
            List<float> points = new List<float>();
            if (DrawingPosition.X <= Scene.Camera.Viewport.Width + Scene.Camera.Viewport.X && DrawingPosition.X + BGTexture.Width >= Scene.Camera.Viewport.X)
                points.Add(DrawingPosition.X);

            float temp = DrawingPosition.X;
            while (true)
            {
                temp += BGTexture.Width;
                if (temp > Scene.Camera.Viewport.Width + Scene.Camera.Viewport.X)
                    break;
                else if (temp + BGTexture.Width < Scene.Camera.Viewport.X)
                    continue;
                else
                    points.Add(temp);
            }

            temp = DrawingPosition.X;
            while (true)
            {
                temp -= BGTexture.Width;
                if (temp + BGTexture.Width < Scene.Camera.Viewport.X)
                    break;
                else if (temp > Scene.Camera.Viewport.Width + Scene.Camera.Viewport.X)
                    continue;
                else
                    points.Add(temp);
            }

            return points;
        }
        private List<float> FindPointsY()
        {
            List<float> points = new List<float>();
            if (DrawingPosition.Y <= Scene.Camera.Viewport.Height + Scene.Camera.Viewport.Y && DrawingPosition.Y + BGTexture.Height >= Scene.Camera.Viewport.Y)
                points.Add(DrawingPosition.Y);

            float temp = DrawingPosition.Y;
            while (true)
            {
                temp += BGTexture.Height;
                if (temp > Scene.Camera.Viewport.Height + Scene.Camera.Viewport.Y)
                    break;
                else if (temp + BGTexture.Height < Scene.Camera.Viewport.Y)
                    continue;
                else
                    points.Add(temp);
            }

            temp = DrawingPosition.Y;
            while (true)
            {
                temp -= BGTexture.Height;
                if (temp + BGTexture.Height < Scene.Camera.Viewport.Y)
                    break;
                else if (temp > Scene.Camera.Viewport.Height + Scene.Camera.Viewport.Y)
                    continue;
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
                xPos.Add(DrawingPosition.X);

            if (LoopY)
                yPos = FindPointsY();
            else 
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
            foreach (var pos in drawingPositions)
                BGTexture.Draw(pos, Color);
        }



    }
}
