using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamUtilities
{
    namespace Tweens
    {
        public class SpriteAlphaTween : Tween
        {
            private SmartSprite _spr;

            private SpriteAlphaTween(float mt, SmartSprite spr, float end, Action done = null, PennerDoubleAnimation.EquationType e = PennerDoubleAnimation.EquationType.Linear)
            {
                maxTime = mt;
                _spr = spr;
                valueStart = spr.Alpha;
                valueEnd = end;
                OnDone = done;
                ease = e;
            }

            protected override void finish()
            {
                base.finish();
                _spr.Alpha = (byte)valueEnd;
            }

            public void DoAlphaTween()
            {
                if (!alive)
                    return;

                float val = PennerDoubleAnimation.GetValue(ease, age, valueStart, valueEnd, maxTime);
                _spr.Alpha = (byte)val;
            }

            public override void DoPerform()
            {
                base.DoPerform();
                DoAlphaTween();
            }


            public static SpriteAlphaTween createAlphaTween(SmartSprite spr, float end = 0, float time = 1, Action done = null, PennerDoubleAnimation.EquationType e = PennerDoubleAnimation.EquationType.Linear)
            {
                SpriteAlphaTween t = new SpriteAlphaTween(time, spr, end, done,e);
                TweenManager.Add(t);
                return t;
            }




        }
    }
}