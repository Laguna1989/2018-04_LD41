using JamUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamTemplate.Tower
{
    class Map : IGameObject
    {
        public List<Tile> allTiles;
        public List<Path> allPaths;

        public Map()
        {
            CreateWorld();
            CreatePath();
        }

        private void CreatePath()
        {
            allPaths = new List<Path>();

            Path p = new Path();
            p.start = new Vector2i(1, 10);
            p.Add(Path.Dir.R, 5);
            p.Add(Path.Dir.B, 2);
            p.Add(Path.Dir.R, 3);
            p.Add(Path.Dir.T, 2);
            allPaths.Add(p);



            foreach(Path pp in allPaths)
            {
                for(int i = 0; i != pp.path.Count; ++i)
                {
                    Vector2i pos = pp.getPosAt(i);
                    //T.TraceD(pos.ToString());
                    GetTileAt(pos.X, pos.Y).SetTileType(Tile.TileType.Street);
                }
            }
        }

        private void CreateWorld()
        {
            allTiles = new List<Tile>();
            for (int i = 0; i != 50; ++i)
            {
                for (int j = 0; j != 30; ++j)
                {
                    Tile t = new Tile(Tile.TileType.Grass, i, j);
                    AddTile(t);
                }
            }
        }

        public void Draw(RenderWindow rw)
        {
            foreach (Tile t in allTiles)
                t.Draw(rw);
        }

        public void GetInput()
        {
            return;
        }

        public Vector2f GetPosition()
        {
            return new Vector2f(0, 0);
        }

        public bool IsDead()
        {
            return false;
        }

        public void SetPosition(Vector2f newPos)
        {
            return;
        }

        public void Update(TimeObject to)
        {
            foreach(Tile t in allTiles)
            {
                t.Update(to);
            }
        }

        public void AddTile (Tile t)
        {
            if (t == null)
            {
                T.TraceD("not adding null tile");
                return;
            }
            allTiles.Add(t);
        }

        /// <summary>
        /// warning, can be null
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile GetTileAt(int x, int y)
        {
            Tile ret = null;

            foreach (Tile t in allTiles)
            {
                if (t.tx == x && t.ty == y)
                {
                    ret = t;
                    break;
                }
            }

            return ret;
        }

    }
}
