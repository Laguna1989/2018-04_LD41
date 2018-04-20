using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamUtilities
{
    public class TimeManager
    {
        private static List<Timer> _allTimers = null;

        

        private static bool clearMe = false;

        private static void Initialize()
        {
            if (_allTimers == null)
                _allTimers = new List<Timer>();
        }

        public static void Update(TimeObject to)
        {
            Initialize();
            foreach(Timer t in _allTimers)
            {
                t.Update(to.ElapsedGameTime);
            }
            CleanUp();
            if (clearMe)
            {
                _allTimers.Clear();
                clearMe = false;
            }

        }

        private static void CleanUp()
        {
            Initialize();
            List<Timer> nt = new List<Timer>();
            foreach (Timer t in _allTimers)
            {
                if (t.alive)
                {
                    nt.Add(t);
                }
            }
            _allTimers = nt;
        }

        internal static void Add(Timer t)
        {
            Initialize();
            if (t != null)
            {
                _allTimers.Add(t);
            }
        }

        public static void Clear()
        {
            Initialize();
            clearMe = true;
        }
    }
}
