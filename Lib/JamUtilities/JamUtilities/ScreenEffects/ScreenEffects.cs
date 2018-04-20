using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities.ScreenEffects
{
    public static class ScreenEffects
    {
        /// <summary>
        /// This variable is the screen size and needed for creating textures and shapes in the right size
        /// </summary> 
        private static Vector2u _screenSize;

        /// <summary>
        /// This is the color to which the screen fades
        /// </summary>
        public static Color _fadeColor = Color.Black;
        /// <summary>
        /// This is the texture for the linear Fade up
        /// </summary>
        private static Texture _fadeUpTexture;
        private static Sprite _fadeUpSprite;
        /// <summary>
        /// This is the Texture for the linear Fade down
        /// </summary>
        private static Texture _fadeDownTexture;
        private static Sprite _fadeDownSprite;

        /// <summary>
        /// Effects in this dynamic Effect list will be updated and drawn by the ScreenEffects class
        /// </summary>
        private static System.Collections.Generic.Dictionary<string, IScreenEffect> _dynamicEffectList;
        /// <summary>
        /// Effects in this static class must be drawn manually. No Update needed
        /// </summary>
        private static System.Collections.Generic.Dictionary<string, IScreenEffect> _staticEffectList;

        public static IScreenEffect GetDynamicEffect(string str)
        {
            IScreenEffect effect = _dynamicEffectList[str];
            return effect;
        }
        public static IScreenEffect GetStaticEffect(string str)
        {
            IScreenEffect effect = _staticEffectList[str];
            return effect;
        }

        public static Vector2f _dragPosition;
        private static Vector2f _dragVelocity;
        private static float _drag = 0.1f;
        private static float _elasticity = 0.10f;

        // can be used for camera movement
        public static Vector2f GlobalSpriteOffset { get; private set; }

        public static void Init(Vector2u screenSize)
        {
            _screenSize = screenSize;

            _dynamicEffectList = new System.Collections.Generic.Dictionary<string, IScreenEffect>();
            _staticEffectList = new System.Collections.Generic.Dictionary<string, IScreenEffect>();

            _dynamicEffectList.Add("shake", new ScreenShakeEffect());
            _dynamicEffectList.Add("fadeIn", new ScreenFlashInEffect());
            _dynamicEffectList.Add("fadeOut", new ScreenFlashOutEffect());
            _dynamicEffectList.Add("darkenLines", new ScreenDarkenLineEffect());

            _staticEffectList.Add("scanlines", new ScreenScanLinesEffect());
            _staticEffectList.Add("vignette", new ScreenVignetteEffect());


            foreach (var kvp in _dynamicEffectList)
            {
                kvp.Value.Create(_screenSize);
            }
            foreach (var kvp in _staticEffectList)
            {
                kvp.Value.Create(_screenSize);
            }

            GlobalSpriteOffset = new Vector2f(0.0f, 0.0f);

            CreateFadeSprites();
        }

        private static void CreateFadeSprites()
        {
            Vector2u centerPosition = new Vector2u(_screenSize.X / 2, _screenSize.Y / 2);

            float distanceToCenterMax = (float)Math.Sqrt(centerPosition.X * centerPosition.X + centerPosition.Y * centerPosition.Y);

            Image fadeUpImage = new Image(_screenSize.X, _screenSize.Y);
            Image fadeDownImage = new Image(_screenSize.X, _screenSize.Y);
            Image fadeRadialImage = new Image(_screenSize.X, _screenSize.Y);

            for (uint i = 0; i != _screenSize.X; i++)
            {
                for (uint j = 0; j != _screenSize.Y; j++)
                {
                    Color newCol = _fadeColor;
                    float newAlpha = 255.0f * 0.35f * (float)(_screenSize.Y - j) / (float)(_screenSize.Y) + 0.0f;
                    newCol.A = (byte)newAlpha;
                    fadeUpImage.SetPixel(i, j, newCol);

                    newAlpha = 255.0f * 0.25f * (float)(j) / (float)(_screenSize.Y) + 0.0f;
                    newCol.A = (byte)newAlpha;
                    fadeDownImage.SetPixel(i, j, newCol);

                    Vector2u distanceToCenter = new Vector2u(centerPosition.X - i, centerPosition.Y - j);
                    newAlpha = 255.0f * 0.75f * (float)Math.Sqrt(distanceToCenter.X * distanceToCenter.X + distanceToCenter.Y * distanceToCenter.Y) / distanceToCenterMax;
                    newCol.A = (byte)newAlpha;
                    fadeRadialImage.SetPixel(i, j, newCol);
                }
            }
            _fadeUpTexture = new Texture(fadeUpImage);
            _fadeUpSprite = new Sprite(_fadeUpTexture);

            _fadeDownTexture = new Texture(fadeDownImage);
            _fadeDownSprite = new Sprite(_fadeDownTexture);
        }

        public static void Update(TimeObject timeObject)
        {
            GlobalSpriteOffset = new Vector2f();

            foreach (var kvp in _dynamicEffectList)
            {
                kvp.Value.Update(timeObject);
                GlobalSpriteOffset += kvp.Value.GlobalSpriteOffset;
            }

            #region Drag Stuff

            //Console.WriteLine(_dragVelocity);

            _dragVelocity -= _dragVelocity * _drag * timeObject.ElapsedGameTime;
            _dragVelocity -= (_dragPosition) * _elasticity * timeObject.ElapsedGameTime;
            _dragPosition += _dragVelocity * timeObject.ElapsedGameTime;

            #endregion Drag Stuff

        }


        public static void DragScreen(Vector2f drag)
        {
            _dragVelocity += drag;
        }

        public static void DrawFadeUp(RenderWindow rw)
        {
            rw.Draw(_fadeUpSprite);
        }

        public static void DrawFadeDown(RenderWindow rw)
        {
            rw.Draw(_fadeDownSprite);
        }

        public static void Draw(RenderWindow rw)
        {
            foreach (var kvp in _dynamicEffectList)
            {
                kvp.Value.Draw(rw);
            }
        }

        public static void ResetScreenEffects()
        {
            foreach (var kvp in _dynamicEffectList)
            {
                kvp.Value.ResetEffect();
            }
        }
    }
}

