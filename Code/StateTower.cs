using JamUtilities;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JamTemplate.Tower;
using SFML.Audio;

namespace JamTemplate
{
    class StateTower : JamUtilities.GameState
    {
        public Map m;
        public EnemyGroup allEnemies;
        public TowerGroup allTowers;
        public ShotGroup allShots;
        public float spawnDeadTime = 0;

        private Animation coin;
        private Animation heart;

        public int wave = 1;

        public int health = 10;

        private Animation castle;

        private CloudLayer cl;

        private bool hasBeenInit = false;

        private TextButton siegeButton;

        private SoundBuffer sndbufLoose;
        private Sound sndLoose;

        public override void Init()
        {
            if (!hasBeenInit)
            {
                hasBeenInit = true;
                base.Init();
                m = new Map();
                Add(m);

                allTowers = new TowerGroup();
                Add(allTowers);

                allTowers.Add(new Tower.Tower(3, 2, this));
                allTowers.Add(new Tower.Tower(6, 6, this));
                allTowers.Add(new Tower.Tower(8, 12, this));

                allTowers.Add(new Tower.Tower(11, 12, this));


                allEnemies = new EnemyGroup();
                allEnemies.DeleteCallback += EnemyDead;
                //SpawnWave();
                Add(allEnemies);

                castle = new Castle();
                Add(castle);
                
                allShots = new ShotGroup();
                Add(allShots);

                siegeButton = new TextButton(" Start Siege", SpawnWave);
                siegeButton.SetPosition(new Vector2f(400-96, 10));
                Add(siegeButton);


                coin = new Animation("../GFX/coin.png", new Vector2u(16, 16));
                coin.SetPosition(new Vector2f(4, 11));
                coin.SetScale(0.75f, 0.75f);
                coin.Add("idle", new List<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }), 0.125f);
                coin.Play("idle");

                heart = new Animation("../GFX/heart.png", new Vector2u(16, 16));
                heart.SetPosition(new Vector2f(4, 46));
                heart.SetScale(0.75f, 0.75f);
                heart.Add("idle", new List<int>(new int[] { 0,1,2,3 }),0.125f);
                heart.Play("idle");

                sndbufLoose = new SoundBuffer("../SFX/loose.wav");
                sndLoose = new Sound(sndbufLoose);

                

#if !DEBUG
            cl = new CloudLayer();
#endif
            }
        }

        public void EnemyDead(Enemy e)
        {
            T.TraceD("i love it when a plan comes together");
            Resources.money += 1;

        }

        public override void Draw(RenderWindow rw)
        {
            base.Draw(rw);
            
            foreach(Tower.Tower t in allTowers)
            {
                t.DrawMenu(rw);
            }
        }

        public override void DrawOverlay(RenderWindow rw)
        {
            base.DrawOverlay(rw);
#if !DEBUG
            cl.Draw(rw);
#endif
            coin.Draw(rw);
            heart.Draw(rw);
            SmartText.DrawText("  " + Resources.money, new Vector2f(10, 0), rw);
            SmartText.DrawText("  " + this.health , new Vector2f(10, 36), rw);
        }

        public override void Update(TimeObject to)
        {
            base.Update(to);
            spawnDeadTime -= to.ElapsedGameTime;

#if !DEBUG
            cl.Update(to);
#endif

            siegeButton.active = (allEnemies.Count == 0);


            coin.Update(to);
            heart.Update(to);
            if (Input.justPressed[Keyboard.Key.N])
            {
                Input.justPressed[Keyboard.Key.N] = false;
                Input.pressed[Keyboard.Key.N] = false;

                SpawnWave();
            }
                

            foreach(Shot s in allShots)
            {
                
                foreach(Enemy e in allEnemies)
                {
                    if (s.IsDead())
                        continue;

                    if (SFMLCollision.Collision.BoundingBoxTest(s._sprites[0].Sprite, e._sprites[0].Sprite))
                    {

                        e.hit(s.dmg);
                        s.hit();

                        // check for crit
                        {
                            float rng = (float)(RandomGenerator.Random.NextDouble());
                            if (rng <= ResearchManager.CritChance / 100.0f)
                            {
                                T.Trace("crit");
                                e.hit(s.dmg + ResearchManager.CritFactor);
                            }
                        }
                        // check for freeze
                        {
                            float rng = (float)(RandomGenerator.Random.NextDouble());
                            if (rng <= ResearchManager.FreezeChance/ 100.0f)
                            {
                                T.Trace("freeze");
                                //e.hit(s.dmg + ResearchManager.CritFactor);
                                e.Freeze(ResearchManager.FreezeDuration);
                            }
                        }

                        // check for goldDrop
                        {
                            float rng = (float)(RandomGenerator.Random.NextDouble());
                            if (rng <= ResearchManager.GoldChance / 100.0f)
                            {
                                T.Trace("gold");
                                Resources.money++;
                            }
                        }

                    }
                }
            }
        }

        internal void CloseAllMenus()
        {
            foreach(Tower.Tower t in allTowers)
            {
                t.showMenu = false;
            }
        }

        public void SpawnWave()
        {
            if (spawnDeadTime <= 0)
            {
                //Enemy e = new Enemy(m.allPaths[0], this,  0.85f);
                //allEnemies.Add(e);

                spawnDeadTime = 1.0f;
                int count = 3 + 5 * wave;

                List<int> pathcounter = new List<int>();
                for (int i = 0; i != m.allPaths.Count; ++i)
                {
                    pathcounter.Add(0);
                }

                for (int i = 0; i != count; ++i)
                {
                    int idx = RandomGenerator.Int(0, m.allPaths.Count);

                    Enemy e = new Enemy(m.allPaths[idx], this, 1.5f + pathcounter[idx] * 0.85f, (float)(Math.Sqrt(wave+1)));
                    allEnemies.Add(e);
                    pathcounter[idx]++;
                }
                wave++;
            }
        }

        public void SpawnShot(Shot s)
        {
            if (s == null)
                throw new ArgumentNullException();

            allShots.Add(s);
        }


        public void looseLife ()
        {
            sndLoose.Play();

            health--;
            Color c = Color.Red;
            c.A = 200;
            castle.Flash(c, 0.75f);
        }
    }
}
