using JamUtilities;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace JamTemplate.Tower
{
    public class Tile : IGameObject
    {

        public Sprite spr;
        public enum TileType
        {
            Street,
            Grass
        };

        private static string TT2FileName (TileType tt)
        {
            //T.TraceD(tt.ToString());
            if (tt == TileType.Grass)
                return "../GFX/grass.png";
            else if (tt == TileType.Street)
                return "../GFX/street.png";
            else
                return "";
        }

        public bool IsDead()
        {
            return false;
        }

        public void GetInput()
        {
            return;
        }

        public void Update(TimeObject to)
        {
            return;
        }

        public void Draw(RenderWindow rw)
        {
            rw.Draw(spr);
        }

        public Vector2f GetPosition()
        {
            return spr.Position;
        }

        public void SetPosition(Vector2f newPos)
        {
            spr.Position = newPos;
        }

        public TileType tt;
        public int tx;
        public int ty;

        public void SetTileType ( TileType t)
        {
            tt = t;
            spr = new Sprite(TextureManager.GetTextureFromFileName(TT2FileName(t)));
            spr.Scale = SmartSprite._scaleVector;
            SetPosition(new Vector2f(GP.WorldTileSizeInPixel * tx, GP.WorldTileSizeInPixel * ty));
        }

        public Tile(TileType t, int x, int y)
        {

            tx = x;
            ty = y;

            SetTileType(t);
        }            
    }
}
