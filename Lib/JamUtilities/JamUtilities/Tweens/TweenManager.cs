using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamUtilities
{
    namespace Tweens
    {
        public class TweenManager
        {
            private static List<Tween> _alltweens = null;
            private static bool clearMe = false;

            internal static List<Action> _onDone = null;

            private static void Initialize()
            {
                if (_alltweens == null)
                {
                    _alltweens = new List<Tween>();
                    _onDone = new List<Action>();
                }
            }

            public static void Update(TimeObject to)
            {
                Initialize();
                foreach (Tween t in _alltweens)
                {
                    t.Update(to.ElapsedGameTime);
                }
                CleanUp();
                foreach(Action a in _onDone)
                {
                    if (a != null)
                        a();
                }
                _onDone.Clear();

                if (clearMe)
                {
                    _alltweens.Clear();
                    clearMe = false;
                }
            }

            private static void CleanUp()
            {
                Initialize();
                List<Tween> nt = new List<Tween>();
                foreach (Tween t in _alltweens)
                {
                    if (t.alive)
                    {
                        nt.Add(t);
                    }
                    else
                    {
                        _onDone.Add(t.OnDone);
                    }
                }
                _alltweens = nt;
            }

            internal static void Add(Tween t)
            {
                Initialize();
                if (t != null)
                {
                    _alltweens.Add(t);
                }
            }
            public static void Clear()
            {
                Initialize();
                clearMe = true;
            }
        }
    }
}