using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities
{
    public static class SpriteTrail
    {
        private static float _totalTime;

        private static System.Collections.Generic.List<SmartSprite> _sprites;

        public static void CheckInitialized()
        {
            if (_sprites == null)
            {
                _sprites = new List<SmartSprite>();
            }
        }


        public static void AddTrailPosition(SmartSprite sprite, float duration)
        {
            CheckInitialized();
            SmartSprite spr = new SmartSprite(sprite.Sprite.Texture);
            spr.Sprite.Origin = sprite.Sprite.Origin;
            spr.Rotation = sprite.Rotation;
            spr.Fade(duration);
            spr.Position = sprite.Position;
            
            _sprites.Add(spr);
        }

        public static void Update(TimeObject timeObject)
        {
            Update(timeObject.ElapsedGameTime);
        }
        
        public static void Update (float deltaT)
        {
            CheckInitialized();
            _totalTime += deltaT;
            System.Collections.Generic.List<float> newCreationTimes = new List<float>();
            System.Collections.Generic.List<SmartSprite> newSprites = new List<SmartSprite>();

            foreach (var s in _sprites)
            {
                if (s.IsInFade)
                {
                    s.Update(deltaT);
                    newSprites.Add(s);
                }

                
            }
            _sprites = newSprites;

        }

        public static void Draw (RenderWindow rw)
        {
            CheckInitialized();
            foreach (var s in _sprites)
            {
                s.Draw(rw);
            }
        }

    }
}
