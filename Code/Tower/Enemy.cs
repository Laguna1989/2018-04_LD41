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

        public float health = 1;
        private bool alive = true;

        private float startDelay = 0;
        private StateTower state;

        public Enemy (Path pa, StateTower  s) : base("../GFX/enemy.png", new Vector2u(16,16))
        {
            p = pa;
            state = s;
            Add("idle", new List<int>(new int[] { 0, 1 }), 0.25f);
            Play("idle");
            SetPosition(new Vector2f(p.start.X * GP.WorldTileSizeInPixel, p.start.Y * GP.WorldTileSizeInPixel));
            startDelay = (float)(RandomGenerator.Random.NextDouble() * 1.6f);
        }

        public override bool IsDead()
        {
            return !alive;
        }

        public override void Update(TimeObject to)
        {
            base.Update(to);
            startDelay -= to.ElapsedGameTime;
            if (startDelay <= 0)
            {
                moveTimer -= to.ElapsedGameTime;
                if (moveTimer <= 0)
                {
                    moveTimer = GP.EnemyMoveTimerMax;

                    if (pathidx >= p.path.Count)
                    {
                        ReachEnd();
                        return;
                    }

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

        private void ReachEnd()
        {
            state.looseLife();
            alive = false;
        }

        public void hit(float dmg)
        {
            health -= dmg;
            if (health <= 0)
                alive = false;
            
        }
    }
}
