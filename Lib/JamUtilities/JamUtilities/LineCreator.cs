using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities
{
    public static class LineCreator
    {
        public static void CreateLine (out RectangleShape shape, Vector2f startPos, Vector2f endPos, float thickness = 1, bool translate = true)
        {
            float difX = endPos.X - startPos.X;
            float difY = endPos.Y - startPos.Y;
            float length = (float)(Math.Sqrt(difX*difX + difY * difY));

            shape = new RectangleShape(new Vector2f(length, thickness));
            shape.Rotation = (float)MathStuff.RadianToDegree(Math.Atan2(difY, difX));
            if(translate)
                shape.Position = startPos;
        }
    }
}
