using JamUtilities;
using SFML.Audio;
using SFML.Graphics;
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
        
        public float health = 1;
        
        private float startDelay = 0;
        private StateTower state;

        private float freezeTimer = 0;
        private Vector2f oldVelocity = new Vector2f(0, 0);

        static private SoundBuffer sndbufHit1 = null;
        static private SoundBuffer sndbufHit2 = null;
        static private SoundBuffer sndbufHit3 = null;

        private Sound sndHit;

        private float velocityFactor = 1;

        public Enemy (Path pa, StateTower  s, float delay = 0, float lv = 1.0f, float vscale = 1.0f) : base("../GFX/enemy.png", new Vector2u(16,16))
        {
            p = pa;
            state = s;
            velocityFactor = vscale;
            if (velocityFactor >= 2)
                velocityFactor = 2;
            Add("walkB", new List<int>(new int[] { 0, 1, 2, 3 }), 0.25f);
            Add("walkLR", new List<int>(new int[] { 4,5,6,7}), 0.25f);
            Add("walkT", new List<int>(new int[] { 8, 9, 10, 11 }), 0.25f);
            Play("walkB");
            Origin = new Vector2f(8,8);

            health *= lv;
            
            Position = new Vector2f(p.start.X * GP.WorldTileSizeInPixel + 14, p.start.Y * GP.WorldTileSizeInPixel + 8);
            startDelay = delay;

            if (sndbufHit1 == null)
            {
                sndbufHit1 = new SoundBuffer("../SFX/hit1.wav");
                sndbufHit2 = new SoundBuffer("../SFX/hit2.wav");
                sndbufHit3 = new SoundBuffer("../SFX/hit3.wav");
            }
            int idx = RandomGenerator.Int(0, 3);
            if (idx == 0)
                sndHit = new Sound(sndbufHit1);
            else if (idx == 1)
                sndHit = new Sound(sndbufHit2);
            else
                sndHit = new Sound(sndbufHit3);
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

                if (freezeTimer > 0)
                {
                    // in freeze mode
                    moveTimer -= 0;
                    velocity = new Vector2f(0, 0);
                    freezeTimer -= to.ElapsedGameTime;
                    //T.TraceD(freezeTimer.ToString());
                    if (freezeTimer <= 0)
                    {
                        T.TraceD("unfreeze");
                        velocity = oldVelocity;
                        foreach (SmartSprite spr in _sprites)
                        {
                            spr.Sprite.Color = Color.White;
                        }
                    }
                }
                else
                {
                    // not in freeze mode   
                    moveTimer -= to.ElapsedGameTime;
                    oldVelocity = velocity;
                }

                //moveTimer -= to.ElapsedGameTime * ((freezeTimer > 0) ? 0 : 1);
                if (moveTimer <= 0)
                {
                    moveTimer = GP.EnemyMoveTimerMax/velocityFactor;

                    //check if end is reached
                    if (pathidx >= p.path.Count)
                    {
                        ReachEnd();
                        return;
                    }

                    // set to absolute position
                    Vector2i pi = p.getPosAt(pathidx);
                    Position = (new Vector2f(pi.X * GP.WorldTileSizeInPixel + 14, pi.Y * GP.WorldTileSizeInPixel + 8));
                    //T.TraceD(Position.ToString());


                    // get new direction
                    Path.Dir newdir = p.path[pathidx];

                    // get new velocity and set new animation
                    float vscale = GP.WorldTileSizeInPixel / (GP.EnemyMoveTimerMax/velocityFactor) ;
                    velocity = new Vector2f(Path.Dir2Vec(newdir).X * vscale, Path.Dir2Vec(newdir).Y * vscale);

                    SetWalkingAnimation(newdir);

                    pathidx++;

                }
            }

        }

        public void Freeze (float duration)
        {
            if (duration > freezeTimer)
                freezeTimer = duration;
            foreach (SmartSprite spr in _sprites)
            {
                spr.Sprite.Color = Color.Blue;
            }
        }

        private void SetWalkingAnimation(Path.Dir newdir)
        {
            if (newdir == Path.Dir.T)
            {
                Play("walkT");
                this.SetScale(1, 1);
            }
            else if (newdir == Path.Dir.B)
            {
                Play("walkB");
                this.SetScale(1, 1);
            }
            else if (newdir == Path.Dir.L)
            {
                Play("walkLR");
                this.SetScale(1, 1);
            }
            else if (newdir == Path.Dir.R)
            {
                Play("walkLR");
                this.SetScale(-1, 1);
            }
        }

        private void ReachEnd()
        {
            state.looseLife();
            alive = false;
        }

        

        public void hit(float dmg)
        {
            Flash(Color.Red, 0.25f);
            health -= dmg;

            sndHit.Play();

            if (health <= 0)
                alive = false;

            //T.Trace(health.ToString());   
        }
    }
}
