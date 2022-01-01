using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    class Background
    {
        public GTexture BGTexture { get; private set; }

        public Vector2 Position;
        private Vector2 prevPosition;
        private List<Vector2> drawingPositions;
        public float X
        {
            get { return Position.X; }
            set { Position.X = value; }
        }
        public float Y
        {
            get { return Position.Y; }
            set { Position.Y = value; }
        }

        public bool LoopX { get; private set; }
        public bool LoopY { get; private set; }

        private bool UpToDate => prevPosition == Position;

        public Background(GTexture bGTexture, Vector2 basePos, bool loopX, bool loopY)
        {
            BGTexture = bGTexture;
            Position = basePos;
            LoopX = loopX;
            LoopY = loopY;

            prevPosition = Position;
            drawingPositions = new List<Vector2>();
        }

        public Background(GTexture bGTexture) 
            : this(bGTexture, new Vector2(0,0), true, true) { }

        private List<float> FindPointsX()
        {
            List<float> points = new List<float>();
            points.Add(Position.X);

            float temp = Position.X;
            while (true)
            {
                temp += BGTexture.Width;
                if (temp > TartarusGame.Instance.ScreenWidth)
                    break;
                else
                    points.Add(temp);
            }

            temp = Position.X;
            while (true)
            {
                temp -= BGTexture.Width;
                if (temp < -BGTexture.Width)
                    break;
                else
                    points.Add(temp);
            }

            return points;
        }
        private List<float> FindPointsY()
        {
            List<float> points = new List<float>();
            points.Add(Position.Y);

            float temp = Position.Y;
            while (true)
            {
                temp += BGTexture.Height;
                if (temp > TartarusGame.Instance.ScreenHeight)
                    break;
                else
                    points.Add(temp);
            }

            temp = Position.Y;
            while (true)
            {
                temp -= BGTexture.Height;
                if (temp < -BGTexture.Height)
                    break;
                else
                    points.Add(temp);
            }

            return points;
        }

        public void Update()
        {
            if (UpToDate == false)
            {
                drawingPositions.Clear();
                List<float> xPos = new List<float>();
                List<float> yPos = new List<float>();

                if (LoopX)
                    xPos = FindPointsX();
                else
                    xPos.Add(Position.X);

                if (LoopY)
                    yPos = FindPointsY();
                else
                    yPos.Add(Position.Y);


                drawingPositions = Calc.FindAllPermutations(xPos, yPos);
            }

            prevPosition = Position;
        }

        public void Render()
        {
            foreach (var pos in drawingPositions)
                BGTexture.Draw(pos);
        }


    }
}
