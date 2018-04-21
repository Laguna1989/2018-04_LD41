using JamUtilities;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate.Tower
{
    class Shot : Animation
    {

        private float age = 0;

        public float dmg = 1;


        public Shot(Tower t, Enemy e) : base("../GFX/arrow.png",new Vector2u(16,16))
        {
            Add("0", new List<int>(new int[] { 0 }), 1);
            Add("1", new List<int>(new int[] { 1 }), 1);
            if (RandomGenerator.Int(0, 2) == 0)
                Play("0");
            else
                Play("1");



            


            SetPosition(t.GetPosition());
            float tx = t.GetPosition().X;
            float ty = t.GetPosition().Y;
            float dx = e.GetPosition().X - tx;
            float dy = e.GetPosition().Y - ty;

            if (dx  < 0 && dy > 0)
                SetScale(1.0f, 1.0f);
            else if (dx > 0 && dy > 0)
                SetScale(-1.0f, 1.0f);
            else if (dx < 0 && dy < 0)
                SetScale(1.0f, -1.0f);
            else if (dx > 0 && dy < 0)
                SetScale(-1.0f, -1.0f);

            Vector2f dir = new Vector2f(dx,dy);
            float l = MathStuff.GetLength(dir);
            dir = new Vector2f(dir.X / l, dir.Y / l);


            //this.angle = MathStuff.RadianToDegree(Math.Atan2(dir.Y, dir.Y));
            velocity = new Vector2f(dir.X * GP.ShotSpeed, dir.Y * GP.ShotSpeed);

        }

        public override void Update(TimeObject to)
        {
            base.Update(to);
            age += to.ElapsedGameTime;
            if (age >= 1)
                alive = false;
        }

        internal void hit()
        {
            alive = false;
        }
    }
}
