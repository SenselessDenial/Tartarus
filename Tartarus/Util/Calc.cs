using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tartarus
{
    /// <summary>
    /// Static class. Handles calculations, RNG, etc.
    /// </summary>
    static class Calc
    {
        private static Random random = new Random();

        // Rolls a value between 0-99.
        public static int RollFlat()
        {
            return random.Next(0, 100);
        }

        // Rolls a value between 0-99, but curved toward the center. The extremities are less likely.
        public static int RollCurved()
        {
            return (RollFlat() + RollFlat()) / 2;
        }

        public static int Next(int min, int max)
        {
            return random.Next(min, max);
        }

        public static int NextRange(int midpoint, int amplitude)
        {
            return midpoint + Next(-amplitude, amplitude);
        }

        public static Vector2 ToVector2(this Point p)
        {
            return new Vector2(p.X, p.Y);
        }

        public static int MinReduction(this Resistances resistance)
        {
            switch (resistance)
            {
                case Resistances.Strong:
                    return 25;
                case Resistances.Weak:
                    return -60;
                default:
                    return 0;
            }
        }

        public static List<Vector2> FindAllPermutations(List<float> xList, List<float> yList)
        {
            List<Vector2> vectors = new List<Vector2>();

            foreach (var x in xList)
                foreach (var y in yList)
                    vectors.Add(new Vector2(x, y));

            return vectors;
        }

        public static float Distance(Vector2 start, Vector2 end)
        {
            float xDiff = end.X - start.X;
            float yDiff = end.Y - start.Y;

            return (float)Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
        }

        public static float Angle(Vector2 start, Vector2 end)
        {
            float xDiff = end.X - start.X;
            float yDiff = end.Y - start.Y;

            return (float)Math.Atan(yDiff / xDiff);
        }


        
    }
}
