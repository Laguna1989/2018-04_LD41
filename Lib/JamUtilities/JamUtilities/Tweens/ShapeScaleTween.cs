using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamUtilities.Tweens
{
    public class ShapeScaleTween : Tween
    {

        private Shape _shp;

        private ShapeScaleTween(float mt, Shape shp, float end, Action done = null, PennerDoubleAnimation.EquationType e = PennerDoubleAnimation.EquationType.Linear)
        {
            maxTime = mt;
            _shp = shp;
            valueStart = shp.Scale.X;
            valueEnd = end;
            OnDone = done;
            ease = e;
        }

        public override void DoPerform()
        {
            base.DoPerform();
            DoScaleTween();
        }


        protected override void finish()
        {
            base.finish();
            _shp.Scale = new SFML.Window.Vector2f(valueEnd, valueEnd);
        }

        private void DoScaleTween()
        {
            float val = PennerDoubleAnimation.GetValue(ease, age, valueStart, valueEnd, maxTime);
            
            _shp.Scale = new SFML.Window.Vector2f(val,val);
        }

        public static ShapeScaleTween createShapeTween(Shape shp, float end = 1, float time = 1, Action done = null, PennerDoubleAnimation.EquationType e = PennerDoubleAnimation.EquationType.Linear)
        {
            ShapeScaleTween t = new ShapeScaleTween(time, shp, end, done, e);
            TweenManager.Add(t);
            return t;
        }
    }
}
