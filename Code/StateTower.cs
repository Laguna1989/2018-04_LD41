using JamUtilities;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JamTemplate.Tower;

namespace JamTemplate
{
    class StateTower : JamUtilities.GameState
    {
        public Map m;
        public EnemyGroup allEnemies;
        public TowerGroup allTowers;
        public ShotGroup allShots;
        public float spawnDeadTime = 0;

        public int wave = 1;

        public int health = 10;

        private Animation castle;


        
        public override void Init()
        {
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
            SpawnWave();
            Add(allEnemies);


            
            castle = new Animation("../GFX/castle.png", new Vector2u(144, 96));
            castle.Add("idle", new List<int>(new int[] { 0 }), 1);
            castle.Play("idle");
            castle.SetPosition(new Vector2f(490, 310));
            Add(castle);

            

            allShots = new ShotGroup();
            Add(allShots);
        }

        public void EnemyDead(Enemy e)
        {
            T.TraceD("i love it when a plan comes together");
            Resources.money += 2;
        }

        public override void Draw(RenderWindow rw)
        {
            base.Draw(rw);
            //rw.Draw(test);
            
            foreach(Tower.Tower t in allTowers)
            {
                t.DrawMenu(rw);
            }
        }

        public override void DrawOverlay(RenderWindow rw)
        {
            base.DrawOverlay(rw);
            SmartText.DrawText("Gold: " + Resources.money, new Vector2f(10, 4), rw);
            SmartText.DrawText("Lives: " + this.health , new Vector2f(10, 30), rw);
        }

        public override void Update(TimeObject timeObject)
        {
            base.Update(timeObject);
            spawnDeadTime -= timeObject.ElapsedGameTime;
            //T.Trace(allEnemies.Count.ToString());
            if (Input.justPressed[Keyboard.Key.N])
            {
                

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

                    Enemy e = new Enemy(m.allPaths[idx], this,  1.5f + pathcounter[idx] * 0.85f);
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
            health--;
            Color c = Color.Red;
            c.A = 200;
            castle.Flash(c, 0.75f);
        }
    }
}
