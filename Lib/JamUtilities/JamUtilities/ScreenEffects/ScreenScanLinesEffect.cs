using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace JamUtilities.ScreenEffects
{
    class ScreenScanLinesEffect : IScreenEffect
    {
        /// <summary>
        /// This is the texture for the scanline Effect
        /// </summary>
        private Texture _scanLinesTexture;
        private Sprite _scanLinesSprite;

        protected override void DoCreate()
        {
            Image screenLineImage = new Image(_screenSize.X, _screenSize.Y);
            Color screenLineColor = new Color(10, 10, 10, 0);
            Color transparentColor = new Color(10, 10, 10, 40);
            for (uint i = 0; i != _screenSize.X; i++)
            {
                for (uint j = 0; j != _screenSize.Y; j++)
                {
                    if (j % 5 == 0 || j % 5 == 1)
                    {
                        screenLineImage.SetPixel(i, j, transparentColor);
                    }
                    else
                    {
                        screenLineImage.SetPixel(i, j, screenLineColor);

                    }
                }
            }
            _scanLinesTexture = new Texture(screenLineImage);
            _scanLinesSprite = new Sprite(_scanLinesTexture);
            IsEffectActive = true;
        }

        protected override void DoUpdate(TimeObject timeObject)
        {
        }

        protected override void DoDraw(SFML.Graphics.RenderWindow rw)
        {
            rw.Draw(_scanLinesSprite);
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
