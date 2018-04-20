using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamUtilities
{
    namespace Tweens
    {
        public class ShapeAlphaTween : Tween
        {
            private Shape _shp;

            private ShapeAlphaTween(float mt, Shape shp, float end, Action done = null, PennerDoubleAnimation.EquationType e = PennerDoubleAnimation.EquationType.Linear)
            {
                maxTime = mt;
                _shp = shp;
                valueStart = shp.FillColor.A;
                valueEnd = end;
                OnDone = done;
                ease = e;
            }

            protected override void finish()
            {
                base.finish();
                Color newCol = new Color(_shp.FillColor);
                newCol.A = (byte)(valueEnd);
                _shp.FillColor = newCol;
            }

            public void DoAlphaTween()
            {
                if (!alive)
                    return;

                float val = PennerDoubleAnimation.GetValue(ease, age, valueStart, valueEnd, maxTime);
                //Console.WriteLine("do alpha tween" + val.ToString());
                Color newCol = new Color(_shp.FillColor);
                newCol.A = (byte)(val);
                _shp.FillColor = newCol;
            }

            public override void DoPerform()
            {
                base.DoPerform();
                DoAlphaTween();
            }


            public static ShapeAlphaTween createAlphaTween(Shape shp, float end = 0, float time = 1, Action done = null, PennerDoubleAnimation.EquationType e = PennerDoubleAnimation.EquationType.Linear)
            {
                ShapeAlphaTween t = new ShapeAlphaTween(time, shp, end, done,e);
                TweenManager.Add(t);
                return t;
            }




        }
    }
}