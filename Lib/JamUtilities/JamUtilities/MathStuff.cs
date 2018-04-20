using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace JamUtilities
{
    public static class MathStuff
    {
        public static double DegreeToRadian(double angle) { return Math.PI * angle / 180.0; }
        public static double RadianToDegree(double angle) { return angle * (180.0 / Math.PI); }

        public static float GetLength (Vector2f vec) { return (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y); }
        public static float GetLengthSquared (Vector2f vec) { return (vec.X * vec.X + vec.Y * vec.Y); }
    }
}
