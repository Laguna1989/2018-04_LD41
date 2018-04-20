using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities.Particles
{
    public abstract class IParticle
    {
        public abstract void Draw(RenderWindow rw);

        public void Update(float deltaT)
        {
            DoAccelerationCalculation(deltaT);
            DoParticleMovement(deltaT);
            DoParticleAlpha();
            
            RemainingLifeTime -= deltaT;

            if (!IsImmortal)
                {
                if (RemainingLifeTime <= 0.0f)
                {
                    IsAlive = false;
                }
            }
            
            DoUpdate(deltaT);
        }

        private void DoAccelerationCalculation(float deltaT)
        {
            Acceleration = new Vector2f();
            if (AffectedByGravity)
            {
                Acceleration += ParticleManager.Gravity * 100.0f;
            }

            Acceleration += ParticleManager.GetAreaAcceleration(Position);


        }

        public abstract void SetAlpha(byte newAlpha);

        protected abstract void DoUpdate(float deltaT);

        private void DoParticleMovement (float deltaT)
        {
            Velocity = (Velocity + Acceleration * deltaT)*FrictionCoefficient;

            Position = Position + Velocity * deltaT;



            
        }

        private void DoParticleAlpha()
        {
            float time = TotalLifeTime - RemainingLifeTime;
            float value = 1.0f - (float)PennerDoubleAnimation.GetValue(AlphaChangeType,time, 0, 1, TotalLifeTime);
            if (value * (float)InitialAlpha <= 2)
            {
                IsAlive = false;
            }
            SetAlpha((byte)(value * (float)InitialAlpha ));
        }

        public Vector2f Position {get; protected set;}
        public Vector2f Velocity { get; protected set; }
        public Vector2f Acceleration { get; protected set; }

        public void OverridePosition (Vector2f newPos)
        {
            Position = newPos;
        }

        public float RemainingLifeTime { get; protected set; }
        public float TotalLifeTime { get; protected set; }

        public byte InitialAlpha { get; protected set; }

        public float FrictionCoefficient { get; set; }

        public bool AffectedByGravity { get; set; }

        public bool IsAlive { get; protected set; }
        public bool IsImmortal { get; protected set; }

        public ParticleManager.ParticleRotationType RotationType { get; set; }
        public float RotationSpeedFactor { get; set; }

        public void Die() { IsAlive = false; }

        public PennerDoubleAnimation.EquationType AlphaChangeType{get; set;}
       
    }
}
