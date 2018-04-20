using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamUtilities
{
    public static class Timing
    {
        public static float TimeFactor { get { return _timeFactor; } set {_timeFactor = value; } }
        private static float _timeFactor = 1.0f;

        public static float ElapsedGameTime { get; private set; }
        public static float ElapsedRealTime { get; private set; }
        

        public static bool IsInPause {get; private set;}
        private static float _pauseTimer;

        private static float MaxFrameTime = 1.0f;


        public static TimeObject Update (float deltaT)
        {
            if(IsInPause)
            {
                _pauseTimer -= deltaT;
                if(_pauseTimer<= 0)
                {
                    _pauseTimer = 0.0f;
                    IsInPause = false;
                }
            }

            ElapsedRealTime = deltaT;
            if (ElapsedGameTime > MaxFrameTime)
                ElapsedGameTime = MaxFrameTime;
            ElapsedGameTime = ElapsedRealTime * TimeFactor * ((IsInPause) ? 0.0f:1.0f);
            TimeObject to = new TimeObject(ElapsedGameTime, ElapsedRealTime, IsInPause);
            return to;
        }

        public static void Pause (float realTimeDuration)
        {
            if(realTimeDuration < 0.0f)
            {
                throw new ArgumentOutOfRangeException("Cannot Pause for a negative Time.");
            }

            IsInPause = true;
            _pauseTimer += realTimeDuration;

        }
    }
}
