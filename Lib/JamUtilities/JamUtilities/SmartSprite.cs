using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities
{
    public class SmartSprite : IGameObject
    {

        private void generalSetup()
        {
            _sprite.Scale = _scaleVector;
            IsInFade = false;
            Alpha = 255;
            Offset = new Vector2f(0,0);
            scrollFactor = 1.0f;
            CreateFlashOverlay();
        }
        public SmartSprite(string filepath)
        {
            _texture = TextureManager.GetTextureFromFileName(filepath);
            _sprite = new Sprite(_texture);


            generalSetup();


        }
        public SmartSprite(Texture texture)
        {
            _texture = texture;
            _sprite = new Sprite(_texture);

            generalSetup();
        }

        public SmartSprite(Texture texture, IntRect rect)
        {
            _texture = texture;
            _sprite = new Sprite(_texture, rect);

            generalSetup();
        }


        private void CreateFlashOverlay()
        {
            Image _flashImage = _sprite.Texture.CopyToImage();   
            
            for (uint i = 0; i != _flashImage.Size.X; i++)
            {
                for (uint j = 0; j != _flashImage.Size.Y; j++)
                {
                    Color oc = _flashImage.GetPixel(i, j);
                    _flashImage.SetPixel(i, j, new Color(255,255,255,oc.A));   
                }
            }

            _flashTexture = new Texture(_flashImage);
            _flashOverlay = new Sprite(_flashTexture, _sprite.TextureRect);
            _totalTimeFlash = 0;
            _timeSinceStartFlash = 0;
            _flashOverlay.Color = new Color(255, 255, 255, 0);
        }


        public void Flash(Color col, float duration)
        {
            if (duration < 0.0f)
            {
                throw new ArgumentOutOfRangeException("duration", duration, "Time for a flash must be non-negative");
            }
            _timeSinceStartFlash = 0.0f;
            _totalTimeFlash = duration;
            _flashColor = col;
            _flashOverlay.Color = col;

        }


        public void Fade(float duration)
        {
            if (duration < 0.0f)
            {
                throw new ArgumentOutOfRangeException("duration", duration, "Time for a fade must be non-negative");
            }

            IsInFade = true;
            _remainingTimeFade = duration;
            _totalTimeFade = duration;
        }

        /// <summary>
        ///  currently buggy
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="shakeTime"></param>
        /// <param name="power"></param>
        /// <param name="shakeDirection"></param>
        public void Shake(float duration, float shakeTime, float power, ShakeDirection shakeDirection = ShakeDirection.AllDirections)
        {
            if (duration < 0.0f)
            {
                throw new ArgumentOutOfRangeException("duration", duration, "Duration for a shake must be non-negative");
            }
            if (shakeTime < 0.0f)
            {
                throw new ArgumentOutOfRangeException("shakeTime", shakeTime, "Time for a shake must be non-negative");
            }
            _shakePower = power;
            _shakeTimer = 0.0f;
            _shakeTimerMax = shakeTime;

            _shakeDirection = shakeDirection;
        }

        

        public void Scale(float factorX, float factorY)
        {
            _sprite.Scale = new Vector2f(factorX * _scaleVector.X, factorY * _scaleVector.X);

        }

        public void BreakPixels(float duration, List<Color> colorList, Vector2f direction, float initialPower)
        {
            _remainingTimeBrokenPixels = duration;
            IsInBrokenPixelMode = true;
            if (colorList.Count == 0)
            {
                throw new Exception("Color list may not be empty!");
            }
            _brokenPixelColorList = colorList;
            float length = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
            if (length <= 1e-5 && length >= -1e-5)
            {
                throw new DivideByZeroException();
            }
            BrokenPixelDirection = direction / length;
            BrokenPixelPower = initialPower;

        }

        public virtual void Update(TimeObject timeObject)
        {
            Update(timeObject.ElapsedGameTime);
        }

        public void Update(float deltaT)
        {
            if (IsInFlash)
            {
                _flashOverlay.TextureRect = _sprite.TextureRect;


                byte va = 0;
                if (_totalTimeFlash != 0)
                {
                    float v = 1 - _timeSinceStartFlash / _totalTimeFlash;   // between 1 and 0
                    va = (byte)(255 * v);
                }

                _flashOverlay.Color = new Color(_flashColor.R, _flashColor.G, _flashColor.B, va);

                _flashOverlay.Scale = _scaleVector;
                _flashOverlay.Rotation = Rotation;
                _flashOverlay.Origin = Origin;
                _flashOverlay.Position = Position;

                _timeSinceStartFlash += deltaT;
                if (_timeSinceStartFlash >= _totalTimeFlash)
                {
                    _flashOverlay.Color = Color.Transparent;
                }

            }
            

            if (IsInFade)
            {
                _remainingTimeFade -= deltaT;
                Color col = _sprite.Color;
                if (_remainingTimeFade >= 0)
                {
                    col.A = (byte)(_remainingTimeFade / _totalTimeFade * 255.0f);
                }
                else
                {
                    col.A = 0;
                    IsInFade = false;
                }
                Alpha = col.A;
                //Console.WriteLine(col.A);
            }

            
            
            if (IsInShake)
            {
                if (_shakeDirection == ShakeDirection.AllDirections)
                {
                    Offset = RandomGenerator.GetRandomVector2fSquare(_shakePower);
                }
                else if (_shakeDirection == ShakeDirection.UpDown)
                {
                    Offset = new Vector2f(0.0f, (float)(RandomGenerator.Random.NextDouble() - 0.5f) * 2.0f * _shakePower);
                }
                else if (_shakeDirection == ShakeDirection.UpDown)
                {
                    Offset = new Vector2f((float)(RandomGenerator.Random.NextDouble() - 0.5f) * 2.0f * _shakePower, 0.0f);
                }
                _shakeTimer += deltaT;
                if (_shakeTimer >= _shakeTimerMax)
                {
                    Offset = new Vector2f(0, 0);
                }


            }
            
            _sprite.Position = Position;

            if (IsInBrokenPixelMode)
            {
                _remainingTimeBrokenPixels -= deltaT;
                if (_remainingTimeBrokenPixels <= 0.0f)
                {
                    IsInBrokenPixelMode = false;
                }
            }
        }

        public virtual void Draw(RenderWindow rw)
        {
            if (IsInBrokenPixelMode)
            {
                // getting the original values
                Color oldColor = _sprite.Color;
                Vector2f oldpos = _sprite.Position;


                float counter = 0.0f;
                float positionIncrement = BrokenPixelPower / (float)(_brokenPixelColorList.Count - 1);
                Vector2f positionStart = _sprite.Position - (0.5f * BrokenPixelDirection * BrokenPixelPower);
                foreach (var c in _brokenPixelColorList)
                {
                    _sprite.Position = positionStart + (positionIncrement * counter) * BrokenPixelDirection;
                    _sprite.Color = c;
                    rw.Draw(_sprite);
                    counter += 1.0f;
                }

                // resetting the original values
                _sprite.Color = oldColor;
                _sprite.Position = oldpos;
            }
            else
            {
                Color col = _sprite.Color;
                byte oldAlpha = col.A;
                col.A = Alpha;
                _sprite.Color = col;
                rw.Draw(_sprite);
                col.A = oldAlpha;
                _sprite.Color = col;
            }
            rw.Draw(_flashOverlay);
        }

        public virtual  bool IsDead()
        {
            return false;
        }

        public virtual void GetInput()
        {
            return;
        }

        public Vector2f GetPosition()
        {
            return Position;
        }
        public void SetPosition(Vector2f newPos)
        {
            Position = newPos;
        }


        #region FIELDS

        private Texture _texture;
        private Sprite _sprite;
        public static Vector2f _scaleVector;

        public Vector2f Position
        {
            get { return _sprite.Position - Offset - (ScreenEffects.ScreenEffects.GlobalSpriteOffset + ScreenEffects.ScreenEffects._dragPosition) * scrollFactor; }
            set { _sprite.Position = value + Offset + (ScreenEffects.ScreenEffects.GlobalSpriteOffset + ScreenEffects.ScreenEffects._dragPosition) * scrollFactor; }
        }
        public float Rotation { get { return _sprite.Rotation; } set { _sprite.Rotation = value; } }
        public Vector2f Origin { get { return _sprite.Origin; } set { _sprite.Origin = value; } }
        public byte Alpha { get; set; }
        public Vector2f Size
        {
            get
            {
                var rect = _sprite.GetGlobalBounds();
                return new Vector2f(rect.Width, rect.Width);
            }
        }


        public Vector2f Offset { get; private set; }
        public bool IsInShake { get { return _shakeTimer < _shakeTimerMax; } }
        private float _shakeTimer;
        private float _shakeTimerMax;
        private float _shakePower;
        private ShakeDirection _shakeDirection;

        public Sprite Sprite { get { return _sprite; } }

        public bool IsInFade { get; private set; }
        private float _remainingTimeFade;
        private float _totalTimeFade;

        public bool IsInFlash { get { return _timeSinceStartFlash < _totalTimeFlash; } }
        private float _timeSinceStartFlash;
        private float _totalTimeFlash;
        private Color _flashColor;

        public bool IsInBrokenPixelMode { get; private set; }
        private float _remainingTimeBrokenPixels;
        public Vector2f BrokenPixelDirection { get; private set; }
        public float BrokenPixelPower { get; set; }
        private List<Color> _brokenPixelColorList;

        // this Factor works on the global Sprite Offset Vector from the ScreenEffects class
        public float scrollFactor { get; set; }

        private Sprite _flashOverlay = null;
        private Texture _flashTexture = null;
        #endregion FIELDS
    }
}
