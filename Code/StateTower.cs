using JamUtilities;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate
{
    class StateTower : JamUtilities.GameState
    {

        
        public override void Init()
        {
            base.Init();
            
        }

        public override void Draw(RenderWindow rw)
        {
            base.Draw(rw);
            T.Trace("tower");
        
        }

        public override void Update(TimeObject timeObject)
        {
            base.Update(timeObject);

            
        }
    }
}
