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

        public Map()
        {
            allTiles = new List<Tile>();
            for (int i = 0; i != 50; ++i)
            {
                for (int j = 0; j != 30; ++j)
                {
                    int idx = RandomGenerator.Int(0, 2);
                    T.TraceD(idx.ToString());
                    Tile.TileType tt = Tile.TileType.Grass;
                    if (idx == 1)
                    {
                        tt = Tile.TileType.Street;
                    }
                    Tile t = new Tile(tt, i, j);
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
