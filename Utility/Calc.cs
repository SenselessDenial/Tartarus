using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

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

        public static float NextFloat(float min, float max)
        {
            return (max - min) * (float)random.NextDouble() + min;
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

            float angle = (float)Math.Atan(yDiff / xDiff);

            if (angle < 0)
                return angle + MathHelper.Pi;
            return angle;
        }

        #region XML

        public static XmlDocument XmlFromString(string file)
        {
            XmlDocument temp = new XmlDocument();
            temp.Load(file);
            return temp;
        }

        public static bool HasAttr(this XmlElement xml, string attribute)
        {
            return xml.Attributes[attribute] != null;
        }

        public static int AttrInt(this XmlElement xml, string attribute)
        {
            if (!xml.HasAttr(attribute))
            {
                throw new Exception("Attribute does not exist.");
            }
            return Convert.ToInt32(xml.GetAttribute(attribute));
        }

        #endregion

        public static Vector2 NormalizeSquare(this Vector2 vector)
        {
            Vector2 v = vector;
            v.Normalize();
            float scalar = v.X >= v.Y ? 1 / v.X : 1 / v.Y;
            v.X *= scalar;
            v.Y *= scalar;
            return v;
        }

        public static int FindNearestDivisible(int number, int divider)
        {
            if (divider == 0 || number % divider == 0)
                return number;

            int temp = number;
            for (int i = 0; i < divider; i++)
            {
                temp++;
                if (temp % divider == 0)
                    return temp;
            }
            return number;
        }

        public static Color MultiplyColors(Color a, Color b)
        {
            Color color = new Color();
            color.R = (byte)((a.R * b.R) / 255);
            color.G = (byte)((a.G * b.G) / 255);
            color.B = (byte)((a.B * b.B) / 255);
            color.A = (byte)((a.A * b.A) / 255);
            return color;
        }

        public static Song SongFromFile(string name, string file)
        {
            return Song.FromUri(name, new Uri("file:///" + Path.Combine(TartarusGame.ResourceFolderPath, file)));
        }


        public static SoundEffect SFXFromFile(string file)
        {
            var filename = Path.Combine(TartarusGame.ResourceFolderPath, file);
            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var sfx = SoundEffect.FromStream(fileStream);
            fileStream.Close();
            return sfx;
        }

        public static T ChooseRandom<T>(List<T> list)
        {
            int index = Calc.Next(0, list.Count);
            return list[index];

        }

        public static Vector2 FindVector(Vector2 direction, float magnitude)
        {
            Vector2 vec = direction;
            vec.Normalize();
            vec *= magnitude;
            return vec;
        }

        public static Vector2 UnitVectorFromAngle(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        public static Vector2 RandomVectorFromAngle(float minAngle, float maxAngle, float magnitude)
        {
            float angle = NextFloat(minAngle, maxAngle);
            Vector2 vec = UnitVectorFromAngle(angle);
            vec *= magnitude;
            return vec;

        }

    }
}
