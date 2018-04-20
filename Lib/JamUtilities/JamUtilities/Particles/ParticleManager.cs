using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities.Particles
{
    public static class ParticleManager
    {

        private static FloatRect ValidPositions = new FloatRect(0, 0, 0, 0);
        public static bool DoPositionCheck { get { return _doPositionCheck; } private set { _doPositionCheck = value; } }

        /// <summary>
        /// This Vector can be used for a global Offset, e.g. for Camera movement for all particles
        /// </summary>
        static public Vector2f GlobalPositionOffset { get; set; }

        public static void SetPositionRect(FloatRect rect)
        {
            _doPositionCheck = true;
            ValidPositions = rect;
        }

        private static System.Collections.Generic.List<IParticle> _particleList;
        private static System.Collections.Generic.List<ParticleEmitter> _emitterList;
        private static System.Collections.Generic.List<AccelerationArea> _areaList;

        public static uint NumberOfSmokeCloudParticles = 27;

        public static uint NumberOfDebrisParticles = 40;

        public static Vector2f Gravity = new Vector2f(0, 1.0f);
        private static bool _doPositionCheck = false;

        private static void CheckInitialized()
        {
            if (_particleList == null)
            {
                _particleList = new List<IParticle>();
            }
            if (_emitterList == null)
            {
                _emitterList = new List<ParticleEmitter>();
            }
            if (_areaList == null)
            {
                _areaList = new List<AccelerationArea>();
            }
        }


        public static void Update(TimeObject timeObject)
        {
            Update(timeObject.ElapsedGameTime);
        }

        public static void Update(float deltaT)
        {
            CheckInitialized();
            System.Collections.Generic.List<IParticle> newList = new List<IParticle>();
            foreach (var p in _particleList)
            {
                p.Update(deltaT);
                if (_doPositionCheck)
                {
                    if (!ValidPositions.Contains(p.Position.X, p.Position.Y))
                    {
                        p.Die();
                    }
                }
                if (p.IsAlive)
                {
                    newList.Add(p);
                }
            }

            _particleList = newList;


            foreach (var e in _emitterList)
            {
                e.Update(deltaT);
            }
        }



        public static void Draw(RenderWindow rw)
        {
            CheckInitialized();
            foreach (var p in _particleList)
            {
                p.Draw(rw);
            }
        }

        public static void SpawnSingleDebris(Vector2f position, Vector2f velocity, Color col, float size, float lifeTime = 0.5f)
        {
            CheckInitialized();

            Shape newShape = new RectangleShape(new Vector2f(size * 1.5f, size * 0.4f));
            newShape.Origin = new Vector2f(size / 2.0f, size / 2.0f);
            newShape.Rotation = 20.0f * (float)(Math.PI * Math.Atan(velocity.Y / velocity.X));
            Color newColor = col;
            newColor.A = 125;
            newShape.FillColor = newColor;

            ShapeParticle particle = new ShapeParticle(newShape, lifeTime * ((float)RandomGenerator.Random.NextDouble() + 0.25f), position, velocity);
            particle.FrictionCoefficient = 0.98f;
            particle.AlphaChangeType = PennerDoubleAnimation.EquationType.QuintEaseIn;
            particle.AffectedByGravity = true;
            particle.RotationType = ParticleRotationType.PRT_Velocity;
            _particleList.Add(particle);
        }

        public static void SpawnMultipleDebris(Vector2f position, float maxVelocity, Color col, float size, float lifeTime = 0.5f)
        {
            CheckInitialized();

            for (uint i = 0; i != NumberOfDebrisParticles; i++)
            {
                Vector2f velocity = RandomGenerator.GetRandomVector2fOnCircle(maxVelocity);
                SpawnSingleDebris(position, velocity, col, size, lifeTime);
            }

        }


        public static void SpawnSmokePuff(Vector2f position, Vector2f velocity, Color col, float smokePuffSize, float lifeTime = 1.5f)
        {
            CheckInitialized();
            Shape newShape = new CircleShape(smokePuffSize);
            newShape.Origin = new Vector2f(smokePuffSize / 2.0f, smokePuffSize / 2.0f);
            Color newColor = col;
            newColor.A = 25;
            newShape.FillColor = newColor;

            ShapeParticle particle = new ShapeParticle(
                newShape,
                lifeTime * (0.25f + (float)RandomGenerator.Random.NextDouble() + 0.5f),
                position, velocity);
            particle.FrictionCoefficient = 0.98f;
            _particleList.Add(particle);
        }

        public static void SpawnSmokeCloud(Vector2f position, float cloudSize, float smokePuffSize, Color col, float lifeTime = 1.8f)
        {
            CheckInitialized();
            for (uint i = 0; i != NumberOfSmokeCloudParticles; i++)
            {
                SpawnSmokePuff(
                    position + RandomGenerator.GetRandomVector2fSquare(cloudSize),
                    RandomGenerator.GetRandomVector2fSquare(10),
                    col, smokePuffSize, lifeTime);
            }
        }

        public static void SpawnRainDrop(Vector2f position, Vector2f velocity, ParticleProperties props)
        {
            CheckInitialized();

            Shape newShape = new RectangleShape(new Vector2f(props.sizeSingle, 5));
            //newShape.Rotation = 20.0f * (float)(Math.PI * Math.Atan(velocity.Y / velocity.X));
            Color newColor = props.col;
            newColor.A = 100;
            newShape.FillColor = newColor;

            ShapeParticle particle = new ShapeParticle(newShape, props.lifeTime, position, velocity);
            particle.FrictionCoefficient = 1.0f - 0.01f * (float)RandomGenerator.Random.NextDouble();
            particle.AlphaChangeType = PennerDoubleAnimation.EquationType.None;
            particle.AffectedByGravity = props.AffectedByGravity;
            particle.RotationType = props.RotationType;
            _particleList.Add(particle);
        }



        public static void SpawnParticle(ParticleProperties props, Vector2f position, Vector2f velocity)
        {
            if (props.Type == ParticleType.PT_SmokePuff)
            {
                SpawnSmokePuff(position, velocity, props.col, props.sizeSingle, props.lifeTime);
            }
            else if (props.Type == ParticleType.PT_SmokeCloud)
            {
                SpawnSmokeCloud(position, props.sizeMultiple, props.sizeSingle, props.col, props.lifeTime);
            }
            else if (props.Type == ParticleType.PT_DebrisSingle)
            {
                SpawnSingleDebris(position, velocity, props.col, props.sizeSingle, props.lifeTime);
            }
            else if (props.Type == ParticleType.PT_Debris_Multiple)
            {
                SpawnMultipleDebris(position, (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y), props.col, props.sizeSingle, props.lifeTime);
            }
            else if (props.Type == ParticleType.PT_RainDrop)
            {
                SpawnRainDrop(position, velocity, props);
            }
            else
            {
                throw new NotImplementedException();
            }
        }


        public static void CreateRainArea(FloatRect rect, Color col, float spawnFrequency)
        {
            ParticleProperties props = new ParticleProperties();
            props.Type = ParticleManager.ParticleType.PT_RainDrop;
            props.col = col;
            props.lifeTime = 10.0f;
            props.sizeMultiple = 0.0f;
            props.sizeSingle = 8;
            props.RotationType = ParticleRotationType.PRT_Velocity;
            props.AffectedByGravity = true;

            ParticleEmitter emitter = new ParticleEmitter(rect, props, spawnFrequency);

            ParticleManager.AddEmitter(emitter);
        }

        public static void CreateChimney(FloatRect rect, Color col, float spawnFrequency)
        {
            ParticleProperties props = new ParticleProperties();
            props.Type = ParticleManager.ParticleType.PT_SmokePuff;
            props.col = col;
            props.lifeTime = 14.0f;
            props.sizeMultiple = 0.0f;
            props.sizeSingle = 5;
            props.RotationType = ParticleRotationType.PRT_None;
            props.AffectedByGravity = false;

            ParticleEmitter emitter = new ParticleEmitter(rect, props, spawnFrequency, new FloatRect(-5, -50, 10, 5));

            ParticleManager.AddEmitter(emitter);
        }

        public static void CreateSparksSpawner(FloatRect rect, Color col, float spawnFrequency)
        {
            ParticleProperties props = new ParticleProperties();
            props.Type = ParticleManager.ParticleType.PT_Debris_Multiple;
            props.col = col;
            props.lifeTime = 2.0f;
            props.sizeMultiple = 0.0f;
            props.sizeSingle = 6;
            props.RotationType = ParticleRotationType.PRT_Velocity;
            props.AffectedByGravity = true;

            ParticleEmitter emitter = new ParticleEmitter(rect, props, spawnFrequency, new FloatRect(-40, -250, 80, 40));

            ParticleManager.AddEmitter(emitter);
        }

        private static void AddEmitter(ParticleEmitter e)
        {
            CheckInitialized();
            _emitterList.Add(e);
        }

        public static void AddAccelerationArea(AccelerationArea area)
        {
            CheckInitialized();
            _areaList.Add(area);
        }



        public static Vector2f GetAreaAcceleration(Vector2f position)
        {
            Vector2f ret = new Vector2f();
            foreach (var a in _areaList)
            {
                if (a.Area.Contains(position.X, position.Y))
                {
                    ret += a.Acceleration;
                }
            }
            return ret;
        }



        public static void ResetParticleSystem()
        {
            _particleList = new List<IParticle>();
            _areaList = new List<AccelerationArea>();
            _emitterList = new List<ParticleEmitter>();
        }


        public enum ParticleType
        {
            PT_SmokePuff,
            PT_SmokeCloud,
            PT_DebrisSingle,
            PT_Debris_Multiple,
            PT_RainDrop
        }
        public enum ParticleRotationType
        {
            PRT_None,
            PRT_Velocity,
            PRT_Const
        }
    }
}
