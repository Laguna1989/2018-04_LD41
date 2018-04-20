using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities.ScreenEffects
{
    class ScreenVignetteEffect : IScreenEffect
    {
        /// <summary>
        /// This is the texture for the radial Fade (vignette)
        /// </summary>
        private Texture _fadeRadialTexture;
        private Sprite _fadeRadialSprite;

        protected override void DoCreate()
        {
            Vector2u centerPosition = new Vector2u(_screenSize.X / 2, _screenSize.Y / 2);

            float distanceToCenterMax = (float)Math.Sqrt(centerPosition.X * centerPosition.X + centerPosition.Y * centerPosition.Y);

            Image fadeUpImage = new Image(_screenSize.X, _screenSize.Y);
            Image fadeDownImage = new Image(_screenSize.X, _screenSize.Y);
            Image fadeRadialImage = new Image(_screenSize.X, _screenSize.Y);

            Color newCol = Color.Black;
            for (uint i = 0; i != _screenSize.X; i++)
            {
                for (uint j = 0; j != _screenSize.Y; j++)
                {
                    Vector2u distanceToCenter = new Vector2u(centerPosition.X - i, centerPosition.Y - j);
                    float newAlpha = 255.0f * 0.75f * (float)Math.Sqrt(distanceToCenter.X * distanceToCenter.X + distanceToCenter.Y * distanceToCenter.Y) / distanceToCenterMax;
                    newCol.A = (byte)newAlpha;
                    fadeRadialImage.SetPixel(i, j, newCol);
                }
            }

            _fadeRadialTexture = new Texture(fadeRadialImage);
            _fadeRadialSprite = new Sprite(_fadeRadialTexture);
            _fadeRadialSprite.Scale = new Vector2f(1.0f, 1.5f);
            IsEffectActive = true;
        }

        protected override void DoUpdate(TimeObject timeObject)
        {
        }

        protected override void DoDraw(SFML.Graphics.RenderWindow rw)
        {
            _fadeRadialSprite.Position = new Vector2f(GP.WindowGameView.Center.X - GP.WindowSize.X / 2, GP.WindowGameView.Center.Y - GP.WindowSize.Y / 2);

            rw.Draw(_fadeRadialSprite);
        }

        protected override void DoStartEffect()
        {
        }

        protected override void DoStopEffect()
        {
        }

        protected override void DoResetEffect()
        {
        }
    }
}
