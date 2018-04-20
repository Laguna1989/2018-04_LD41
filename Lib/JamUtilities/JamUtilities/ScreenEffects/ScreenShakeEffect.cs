using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace JamUtilities.ScreenEffects
{
    public class ScreenShakeEffect : IScreenEffect
    {

        protected override void DoCreate()
        {
            
        }

        protected override void DoUpdate(TimeObject timeObject)
        {
            if (_inverseFrequency <= 0.0f)
            {
                if (_direction == ShakeDirection.AllDirections)
                {
                    GlobalSpriteOffset = RandomGenerator.GetRandomVector2fSquare(_power);
                }
                else if (_direction == ShakeDirection.UpDown)
                {
                    GlobalSpriteOffset = new Vector2f(0.0f, (float)(RandomGenerator.Random.NextDouble() - 0.5f) * 2.0f * _power);
                }
                else if (_direction == ShakeDirection.UpDown)
                {
                    GlobalSpriteOffset = new Vector2f((float)(RandomGenerator.Random.NextDouble() - 0.5f) * 2.0f * _power, 0.0f);
                }
                _inverseFrequency = _inverseFrequencyTotal;
            }
        }

        protected override void DoDraw(SFML.Graphics.RenderWindow rw)
        {

        }

        protected override void DoStartEffect()
        {

        }

        protected override void DoStopEffect()
        {
            GlobalSpriteOffset = new Vector2f(0.0f, 0.0f);
        }

        protected override void DoResetEffect()
        {
        
        }
    }
}
