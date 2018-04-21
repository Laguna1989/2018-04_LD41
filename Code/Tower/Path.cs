using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate.Tower
{
    class Path
    {
        public enum Dir
        {
            L,
            T,
            R,
            B
        }

        public Vector2i start;

        public List<Dir> path;

        public Path()
        {
            path = new List<Dir>();
        }

        public static Vector2i Dir2Vec (Dir d)
        {
            if (d == Dir.B)
                return new Vector2i(0, 1);
            else if (d == Dir.T)
                return new Vector2i(0, -1);
            else if (d == Dir.R)
                return new Vector2i(1,0);
            else
                return new Vector2i(-1,0);
        }

        public Vector2i getPosAt(int idx)
        {
            if (idx < 0 | idx >= path.Count)
                throw new IndexOutOfRangeException();

            Vector2i v = new Vector2i(start.X, start.Y);

            for (int i = 0; i != idx; ++i)
            {
                v += Dir2Vec(path[i]);
            }

            return v;

        }

        public void Add(Dir d, int c = 1)
        {
            for (int i = 0; i != c; ++i)
            {
                path.Add(d);
            }
        }

    }
}
