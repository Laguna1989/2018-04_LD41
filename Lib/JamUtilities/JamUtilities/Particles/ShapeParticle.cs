using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities.Particles
{
    public class ShapeParticle : IParticle
    {

        public ShapeParticle(Shape shape, float lifeTime, Vector2f position, Vector2f velocity)
        {
            
            SetShape(shape);
            Position = position;
            Velocity = velocity;

            RotationType = ParticleManager.ParticleRotationType.PRT_None;
            RotationSpeedFactor = 1.0f;

            InitialAlpha = shape.FillColor.A;
            AlphaChangeType = PennerDoubleAnimation.EquationType.QuadEaseOut;
            
            RemainingLifeTime =  TotalLifeTime = lifeTime;
            IsAlive = true;
            IsImmortal = false;
            
        }

        Shape _particleShape;

        public void SetShape (Shape shape)
        {
            _particleShape = shape;
            _particleShape.Origin = new Vector2f(shape.GetLocalBounds().Width / 2, shape.GetLocalBounds().Height / 2);
        }

        public override void SetAlpha(byte newAlpha)
        {
            Color col = _particleShape.FillColor;
            col.A = newAlpha;
            _particleShape.FillColor = col;
        }

        public override void Draw(SFML.Graphics.RenderWindow rw)
        {
            _particleShape.Position -= ParticleManager.GlobalPositionOffset;
            rw.Draw(_particleShape);
        }

        protected override void DoUpdate(float deltaT)
        {
            _particleShape.Position = Position;

            if (RotationType == ParticleManager.ParticleRotationType.PRT_Velocity)
            {
                _particleShape.Rotation = (float)MathStuff.RadianToDegree(Math.Atan(Velocity.Y / Velocity.X)) * RotationSpeedFactor;
            }
            else if (RotationType == ParticleManager.ParticleRotationType.PRT_Const)
            {
                _particleShape.Rotation += deltaT * RotationSpeedFactor;
            }
        }



    }
}
