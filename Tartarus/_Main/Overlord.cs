using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Tartarus
{
    static class Overlord
    {
        public static int WindowWidth = 800;
        public static int WindowHeight = 800;

        public static Vector2 CameraPos = new Vector2(0, 0);
        public static float CameraScale = 4f;

        public static Matrix Matrix = Matrix.CreateTranslation(-CameraPos.X, -CameraPos.Y, 0f) * Matrix.CreateScale(CameraScale) * Matrix.Identity;




    }
}
