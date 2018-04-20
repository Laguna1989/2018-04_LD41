using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities.ScreenEffects
{
    abstract public class IScreenEffect
    {

        /// <summary>
        /// This variable is the screen size and needed for creating textures and shapes in the right size
        /// </summary> 
        protected static Vector2u _screenSize;

        /// <summary>
        ///  Is The Effect active
        /// </summary>
        public bool IsEffectActive {get; protected set;}

        /// <summary>
        ///  The total time the Effect will be running
        /// </summary>
        public float EffectTotalTime {get; private set;}

        /// <summary>
        ///  The remaining Time
        /// </summary>
        public float EffectRemainingTime { get; private set; }

        /// <summary>
        ///  If the Effect does some Offset it can be tracked in this Variable
        /// </summary>
        public Vector2f GlobalSpriteOffset { get; protected set; }


        /// <summary>
        /// the Effect's Color
        /// </summary>
        protected Color _color;

        protected float _inverseFrequency;
        protected float _inverseFrequencyTotal;

        protected float _power;
        protected double _initialAlpha;
        protected ShakeDirection _direction;

        /// <summary>
        ///  Set Some default Values and call DoCreate
        /// </summary>
        /// <param name="screenSize"></param>
        public void Create(Vector2u screenSize)
        {
            _screenSize = screenSize;
            IsEffectActive = false;
            _power = 1;
            _inverseFrequency = 0;
            _inverseFrequencyTotal = 1;
            _initialAlpha = 255;

            DoCreate();
        }

        /// <summary>
        ///  Start the Effect and Call DoStartEffect
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="inverseFrequency"></param>
        /// <param name="col"></param>
        /// <param name="power"></param>
        /// <param name="shakeDirection"></param>
        public void StartEffect(float duration, float inverseFrequency, Color col, float power, ShakeDirection direction = ShakeDirection.AllDirections)
        {
            if ((duration < 0.0f) && (duration != -1.0f))
            {
                throw new ArgumentOutOfRangeException("duration", duration, "Duration for a screen effect must be non-negative or -1.0f ");
            }
            if (inverseFrequency < 0.0f)
            {
                throw new ArgumentOutOfRangeException("shakeTime", inverseFrequency, "Time for a shake must be non-negative");
            }
            
            EffectTotalTime = EffectRemainingTime = duration;
            _inverseFrequency = _inverseFrequencyTotal = inverseFrequency;
            _color = col;
            _initialAlpha = _color.A;
            _power = power;
            _direction = direction;
            IsEffectActive = true;

            DoStartEffect();
        }

        /// <summary>
        /// Stop the Effect and call DoStopEffect
        /// </summary>
        public void StopEffect()
        {
            IsEffectActive = false;
            EffectRemainingTime = 0.0f;
            _inverseFrequency = 0.0f;
            DoStopEffect();
        }

        /// <summary>
        /// Update The Effect if it is Active And call DoUpdate
        /// </summary>
        /// <param name="timeObject"></param>
        public void Update(TimeObject timeObject)
        {
            
            if (IsEffectActive)
            {
                EffectRemainingTime -= timeObject.ElapsedGameTime;
                _inverseFrequency -= timeObject.ElapsedGameTime;

                DoUpdate(timeObject);
                if (EffectRemainingTime <= 0.0f)
                {
                    StopEffect();
                }
            }


        }

        /// <summary>
        /// Draw the Effect by calling DoDraw
        /// </summary>
        /// <param name="rw"></param>
        public void Draw(RenderWindow rw)
        {
            if (IsEffectActive)
            {
                DoDraw(rw);
            }
        }

        /// <summary>
        ///  Reset The Effect
        /// </summary>
        public void ResetEffect()
        {
            StopEffect();
            DoResetEffect();

        }

        // Theese abstract methods have to be overwritten in the derived subclasses so everything works as expected

        protected abstract void DoCreate();
        protected abstract void DoUpdate(TimeObject timeObject);
        protected abstract void DoDraw(RenderWindow rw);
        protected abstract void DoStartEffect();
        protected abstract void DoStopEffect();
        protected abstract void DoResetEffect();
    }
}
