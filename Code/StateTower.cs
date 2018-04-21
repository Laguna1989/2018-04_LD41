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
           
        public override void Init()
        {
            base.Init();
            m = new Map();
            Add(m);

            allEnemies = new EnemyGroup();

            Enemy e = new Enemy(m.allPaths[0]);
            allEnemies.Add(e);

            Add(allEnemies);


        }

        public override void Draw(RenderWindow rw)
        {
            base.Draw(rw);
        
        }

        public override void Update(TimeObject timeObject)
        {
            base.Update(timeObject);

            
        }
    }
}
