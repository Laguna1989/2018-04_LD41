using JamUtilities;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace JamTemplate.Tower
{
    class Castle : Animation
    {
        public Castle () : base("../GFX/castle.png", new SFML.Window.Vector2u(144,96))
        {
            Add("idle", new List<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }), 0.125f);
            Play("idle");
            SetPosition(new Vector2f(496, 310));
        }
        public override void Draw(RenderWindow rw)
        {
            base.Draw(rw);
            
        }
    }
}
