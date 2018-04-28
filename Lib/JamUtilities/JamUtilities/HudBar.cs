using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities
{
     public class HudBar : IGameObject
    {
        public Shape bg1;
        public Shape bg2;
        public Shape fg;

        public float health;    // between 0 and 1

        public HudBar( Vector2f pos)
        {
            bg1 = new RectangleShape(new Vector2f(196 + 8, 24 + 8));
            bg2 = new RectangleShape(new Vector2f(196, 24));
            fg = new RectangleShape(new Vector2f(196, 24));

            SetPosition(pos);
            health = 0;
        }

        public void Draw(RenderWindow rw)
        {
            rw.Draw(bg1);
            rw.Draw(bg2);
            rw.Draw(fg);
        }

        public void GetInput()
        {
            return;
        }

        public Vector2f GetPosition()
        {
            return bg1.Position;
        }

        public bool IsDead()
        {
            return false;
        }

        public void SetPosition(Vector2f newPos)
        {
            bg1.Position = newPos;
            bg2.Position = newPos + new Vector2f(4, 4);
            fg.Position = newPos + new Vector2f(4, 4);
        }

        public void Update(TimeObject to)
        {
            float v = health;
            if (v > 1) v = 1;
            if (v < 0) v = 0;

            fg.Scale = new Vector2f(v, 1);

        }

        
    }
}
