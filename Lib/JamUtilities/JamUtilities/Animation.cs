﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamUtilities
{
    public class Animation : IGameObject
    {
        private List<AnimationProperties> _storedAnimations;
        private AnimationProperties currentAnimation = null;

        private float _animTime = 0; // how long the current frame has been displayed
        private int _animIdx = 0;    // a simple counter
        private int _frameIdx = 0;   // the actual index of the currently displayed sprite in _sprites

        public List<SmartSprite> _sprites;
        
        public bool alive = true;

        public Animation(string fileName, Vector2u spriteSize)
        {
            _sprites = new List<SmartSprite>();
            
            uint maxIdx = TextureManager.GetTextureFromFileName(fileName).Size.X / spriteSize.X;

            for (uint i = 0; i != maxIdx; ++i)
            {
                IntRect rect = new IntRect((int)(i * spriteSize.X), 0, (int)(spriteSize.X), (int)(spriteSize.Y));
                SmartSprite spr = new SmartSprite(TextureManager.GetTextureFromFileName(fileName), rect);
                _sprites.Add(spr);
            }

            _storedAnimations = new List<AnimationProperties>();
        }

        

        public void Add (string name, List<int> frames, float frameTime)
        {
            AnimationProperties ap = new AnimationProperties();
            ap.frames = frames;
            ap.frametime = frameTime;
            ap.name = name;
            _storedAnimations.Add(ap);
        }

        public void Play(string name, int startFrame  = 0)
        {
            currentAnimation = null;

            _animIdx = startFrame;
            _animTime = 0;
            _frameIdx = 0;

            foreach(var ap in _storedAnimations)
            {
                if (ap.name == name)
                {
                    currentAnimation = ap;
                    break;
                }
            }
        }


        public virtual void Update(TimeObject to)
        {
            if (currentAnimation == null)
                return;
            if (currentAnimation.frames.Count == 0)
                return;

            _animTime += to.ElapsedGameTime;
            
            while (_animTime >= currentAnimation.frametime)
            {
                _animTime -= currentAnimation.frametime;
                _animIdx++;
                if (_animIdx >= currentAnimation.frames.Count)
                {
                    _animIdx = 0;
                }
            }
            foreach(var _s in _sprites)
            {
                _s.Update(to);
            }
            
        }
        
        public virtual void Draw(RenderWindow rw)
        {
            if (currentAnimation == null)
                return;
            if (currentAnimation.frames.Count == 0)
                return;

            _frameIdx = currentAnimation.frames[_animIdx];
            if (_frameIdx >= _sprites.Count)
            {
                _frameIdx = 0;
                return;
            }
            _sprites[_frameIdx].Draw(rw);
        }

        public virtual bool IsDead()
        {
            return !alive;
        }

        public virtual void GetInput()
        {
            return;
        }

        public Vector2f velocity
        {
            get
            {
                return _sprites[_frameIdx].velocity;
            }
            set
            {
                foreach (var _sprite in _sprites)
                {
                    _sprite.velocity = value;
                }
            }
        }

        public Vector2f Position
        {
            get
            {
                return _sprites[_frameIdx].Position;
            }
            set
            {
                foreach (var _sprite in _sprites)
                {
                    _sprite.Position = value;
                }
            }
        }
        public float Rotation
        {
            get
            {
                return _sprites[_frameIdx].Rotation;
            }
            set
            {
                foreach (var _sprite in _sprites) _sprite.Rotation = value;
            }
        }

        public Vector2f Origin
        {
            get
            {
                return _sprites[_frameIdx].Origin;
            }
            set
            {
                foreach (var _sprite in _sprites)
                    _sprite.Origin = value;
            }
        }
        
        public Vector2f Scale
        {
            get
            {
                return _sprites[_frameIdx].Sprite.Scale;
            }
        }
        public void SetScale(float sx, float sy )
        {
            foreach(var _s in _sprites)
            {
                _s.Scale(sx, sy);
            }
        }

        public void Flash(Color col, float duration)
        {
            foreach(var _s in _sprites)
            { _s.Flash(col, duration); }
        }

        public void Shake(float duration, float shakeTime, float power, ShakeDirection shakeDirection = ShakeDirection.AllDirections)
        {
            foreach (var _s in _sprites)
            { _s.Shake(duration,shakeTime,power, shakeDirection); }
        }

        public Vector2f GetPosition()
        {
            return Position;
        }

        public void SetPosition(Vector2f newPos)
        {
            Position = newPos;
        }
    }
}
