using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamUtilities
{
    public class Timer
    {

        public bool alive { get; private set; } = true;
        private Action callback = null;
        public float maxTime { get; private set; }

        public float age { get; private set; } = 0;
        
        public static Timer Start (float t, Action cb)
        {
            Timer ti = new Timer(t,cb);
            TimeManager.Add(ti);

            return ti;
        }

        internal void Update(float elapsedGameTime)
        {
            if (!alive)
                return;

            age += elapsedGameTime;
            if (age >= maxTime)
            {
                alive = false;
                callback();
            }
        }

        internal Timer ( float t, Action cb)
        {
            maxTime = t;
            callback = cb;
        }


    }
}
