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

        public int wave = 1;

        public override void Init()
        {
            base.Init();
            m = new Map();
            Add(m);

            allEnemies = new EnemyGroup();
            allEnemies.DeleteCallback += EnemyDead;
            SpawnWave();
            Add(allEnemies);


            allTowers = new TowerGroup();
            Add(allTowers);



            Tower.Tower t = new Tower.Tower(7, 9, this);
            allTowers.Add(t);

            allShots = new ShotGroup();
            Add(allShots);

        }

        public void EnemyDead(Enemy e)
        {
            T.TraceD("i love it when a plan comes together");
        }

        public override void Draw(RenderWindow rw)
        {
            base.Draw(rw);
        
        }

        public override void Update(TimeObject timeObject)
        {
            base.Update(timeObject);

            foreach(Shot s in allShots)
            {
                
                foreach(Enemy e in allEnemies)
                {
                    if (s.IsDead())
                        continue;

                    if (SFMLCollision.Collision.CircleTest(s.Sprite, e._sprites[0].Sprite))
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
            int count = 3 + 5 * wave;
            for(int i = 0; i != count; ++i)
            {
                Enemy e = new Enemy(m.allPaths[0]);
                allEnemies.Add(e);
            }
            wave++;
        }

        public void SpawnShot(Shot s)
        {
            if (s == null)
                throw new ArgumentNullException();

            allShots.Add(s);
        }
    }
}
