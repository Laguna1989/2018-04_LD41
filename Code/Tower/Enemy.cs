using JamUtilities;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate.Tower
{
    class Enemy : Animation
    {
        private Path p;

        private int pathidx = 0;
        private float moveTimer = 0;

        private Vector2f velocity = new Vector2f(0, 0);

        private bool alive = true;
        public Enemy (Path pa) : base("../GFX/enemy.png", new Vector2u(16,16))
        {
            p = pa;
            Add("idle", new List<int>(new int[] { 0, 1 }), 0.25f);
            Play("idle");
            SetPosition(new Vector2f(p.start.X * GP.WorldTileSizeInPixel, p.start.Y * GP.WorldTileSizeInPixel));
        }

        public override bool IsDead()
        {
            return !alive;
        }

        public override void Update(TimeObject to)
        {
            base.Update(to);

            moveTimer -= to.ElapsedGameTime;
            if (moveTimer <= 0)
            {
                moveTimer = GP.EnemyMoveTimerMax;

                
                Path.Dir newdir = p.path[pathidx];
                velocity = new Vector2f(Path.Dir2Vec(newdir).X, Path.Dir2Vec(newdir).Y);
                pathidx++;
            }

            Vector2f newPos = GetPosition();

            float vscale = GP.WorldTileSizeInPixel / GP.EnemyMoveTimerMax;

            newPos += velocity * to.ElapsedGameTime * vscale;
            SetPosition(newPos);
            
        }
    }
}
