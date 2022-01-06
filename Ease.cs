using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework; 

namespace Tartarus
{
    public class Ease
    {
        public delegate float Easing(float t);

        public static Easing Linear = t => t;

        public static Easing QuadIn = t => t * t;
        public static Easing QuadOut = Reverse(QuadIn);
        public static Easing QuadInOut = Combine(QuadIn, QuadOut);

        public static Easing CubicIn = t => t * t * t;
        public static Easing CubicOut = Reverse(CubicIn);
        public static Easing CubicInOut = Combine(CubicIn, CubicOut);

        public static Easing QuarticIn = t => t * t * t * t;
        public static Easing QuarticOut = Reverse(QuarticIn);
        public static Easing QuarticInOut = Combine(QuarticIn, QuarticOut);

        public static Easing SineIn = t => 1f - (float)Math.Cos(MathHelper.PiOver2 * t);
        public static Easing SineOut = t => (float)Math.Sin(MathHelper.PiOver2 * t);
        public static Easing SineInOut = t => -(float)(Math.Cos(MathHelper.Pi * t) - 1) / 2;

        public static Easing CircIn = t => 1f - (float)Math.Sqrt(1f - Math.Pow(t, 2));
        public static Easing CircOut = Reverse(CircIn);
        public static Easing CircInOut = Combine(CircIn, CircOut);

        public const float C1 = 1.70158f;
        public const float C2 = C1 + 1f;
        public const float C3 = 7.5625f;
        public const float C4 = 2.75f;

        public static Easing BackIn = t => C2 * t * t * t - C1 * t * t;
        public static Easing BackOut = t => 1 + C2 * (float)Math.Pow(t - 1, 3) + C1 * (float)Math.Pow(t - 1, 2);
        public static Easing BackInOut = Combine(BackIn, BackOut);

        public static Easing BounceIn = t => 1 - BounceOut(1 - t);
        public static Easing BounceOut = t =>
        {
            if (t < 1 / C4)
            {
                return C3 * t * t;
            }
            else if (t < 2 / C4)
            {
                return C3 * (t -= 1.5f / C4) * t + 0.75f;
            }
            else if (t < 2.5 / C4)
            {
                return C3 * (t -= 2.25f / C4) * t + 0.9375f;
            }
            else
            {
                return C3 * (t -= 2.625f / C4) * t + 0.984375f;
            }
        };
        public static Easing BounceInOut = Combine(BounceIn, BounceOut);



        public static Easing Reverse(Easing function)
        {
            return (float t) => { return 1f - function(1f - t); };
        }

        public static Easing Combine(Easing function1, Easing function2)
        {
            return (float t) => { return t <= 0.5f ? function1(2 * t) / 2 : function2(2 * t - 1) / 2 + 0.5f; };
        }

        public static float Calculate(float elapsedTime, float duration, float start, float end, Easing function)
        {
            if (elapsedTime >= duration)
            {
                return end;
            }

            float t = elapsedTime / duration;
            float x = function(t);
            return (end - start) * x + start;
        }

        public static Vector2 Calculate(float elapsedTime, float duration, Vector2 start, Vector2 end, Easing function)
        {
            if (elapsedTime >= duration)
            {
                return end;
            }

            float t = elapsedTime / duration;
            float x = function(t);
            return (end - start) * x + start;
        }

        public static Vector3 Calculate(float elapsedTime, float duration, Vector3 start, Vector3 end, Easing function)
        {
            if (elapsedTime >= duration)
            {
                return end;
            }

            float t = elapsedTime / duration;
            float x = function(t);
            return (end - start) * x + start;
        }














    }
}
