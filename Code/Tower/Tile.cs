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

        public Sprite spr1;
        public Sprite spr2;

        private float animTime = 0;

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
            animTime += to.ElapsedGameTime;
            if (animTime >= 1.0f)
                animTime -= 1.0f;
            return;
        }

        public void Draw(RenderWindow rw)
        {
            //spr.Draw(rw);
            if (animTime >= 0.5f)
                rw.Draw(spr1);
            else
                rw.Draw(spr2);
        }

        public Vector2f GetPosition()
        {
            return spr1.Position;
        }

        public void SetPosition(Vector2f newPos)
        {
            spr1.Position = newPos;
            spr2.Position = newPos;
        }

        public TileType tt;
        public int tx;
        public int ty;

        public void SetTileType ( TileType t)
        {
            tt = t;
            if (tt == TileType.Grass)
            {
                int idx = RandomGenerator.Int(0, 8);
                spr1 = new Sprite(TextureManager.GetTextureFromFileName("../GFX/grass.png"), new IntRect(idx * 16, 0, 16, 16));
                spr2 = new Sprite(TextureManager.GetTextureFromFileName("../GFX/grass.png"), new IntRect((idx + 8) * 16, 0, 16, 16));
            }
            else
            {
                spr1 = new Sprite(TextureManager.GetTextureFromFileName(TT2FileName(tt)));
                spr2 = new Sprite(TextureManager.GetTextureFromFileName(TT2FileName(tt)));
            }
            SetPosition(new Vector2f(GP.WorldTileSizeInPixel * tx, GP.WorldTileSizeInPixel * ty));
            spr1.Scale = SmartSprite._scaleVector;
            spr2.Scale = SmartSprite._scaleVector;
        }

        public Tile(TileType t, int x, int y)
        {
            tx = x;
            ty = y;

            SetTileType(t);
            animTime = (float)(RandomGenerator.Random.NextDouble());
        }            
    }
}
