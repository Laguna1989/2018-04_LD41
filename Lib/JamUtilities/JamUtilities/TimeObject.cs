using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamUtilities
{
    public class TimeObject
    {
        public float ElapsedGameTime { get; private set; }
        public float ElapsedRealTime { get; private set; }
        public bool IsPaused { get; private set; }
        public TimeObject(float gameTime, float realTime, bool isPaused)
        {
            ElapsedGameTime = gameTime;
            ElapsedRealTime = realTime;
            IsPaused = isPaused;
        }
    }
}
