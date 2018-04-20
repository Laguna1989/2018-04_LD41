using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace JamUtilities
{
    public class ColorList
    {
        static public List<Color> GetColorList (params Color[] colors)
        {
            List<Color> ret = new List<Color>();

            foreach(var c in colors)
            {
                ret.Add(c);
            }

            return ret;
        }

        static public List<Color> GetColorListWithConstantAlpha(byte alpha, params Color[] colors)
        {
            List<Color> ret = new List<Color>();

            for (int i = 0; i < colors.Length; i++)
            {
                Color c = colors[i];
                c.A = alpha;
                ret.Add(c);
            }
            return ret;
        }
    }
}
