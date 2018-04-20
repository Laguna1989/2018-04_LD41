using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities.Particles
{
    public class ParticleEmitter
    {

        private FloatRect _shape;
        private float _spawnTimeCurrent;
        private float _spawnTimeTotal;
        private ParticleProperties _properties;
        private FloatRect _initialParticleVelocity;

        public ParticleEmitter (FloatRect shape, ParticleProperties props, float spawnFrequency, FloatRect initialParticleVelocity = new FloatRect())
        {
            _shape = shape;
            _properties = props;
            _spawnTimeCurrent = 1.0f/spawnFrequency;
            _spawnTimeTotal = 1.0f / spawnFrequency;
            _initialParticleVelocity = initialParticleVelocity;
        }

        public void Update (float deltaT)
        {
            // we do spawn rare Particles (every few frames some droplets)
            if (_spawnTimeTotal >= deltaT)
            {
                _spawnTimeCurrent -= deltaT;
                if (_spawnTimeCurrent <= 0.0f)
                {
                    Spawn();
                    _spawnTimeCurrent += _spawnTimeTotal;
                }

            }
            // We spawn multiple Particles per Frame
            else
            {
                int numberOfParticlesToSpawn = (int)Math.Ceiling(deltaT / _spawnTimeTotal);
                for (int i = 0; i != numberOfParticlesToSpawn; i++)
                {
                    Spawn();
                }
            }
        }

        private void Spawn()
        {
            
            Vector2f position = RandomGenerator.GetRandomVector2fInRect(_shape);

            Vector2f velocity = new Vector2f();
            if(!_initialParticleVelocity.Equals(new FloatRect()))
            {
                 velocity = RandomGenerator.GetRandomVector2fInRect(_initialParticleVelocity);
            }

            ParticleManager.SpawnParticle(_properties, position, velocity);

        }


    }
}
