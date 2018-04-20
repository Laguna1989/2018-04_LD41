using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace JamUtilities.ScreenEffects
{
    /// <summary>
    ///  Flasing from Initial Alpha to 0
    /// </summary>
    public class ScreenFlashInEffect : IScreenEffect
    {

        private static Texture _screenFlashTexture;
        private static Sprite _screenFlashSprite;

        protected override void DoCreate()
        {
            Image screenFlashImage = new Image(_screenSize.X, _screenSize.Y, Color.White);
            _screenFlashTexture = new Texture(screenFlashImage);
            _screenFlashSprite = new Sprite(_screenFlashTexture);
        }

        protected override void DoUpdate(TimeObject timeObject)
        {
            
        }

        protected override void DoDraw(SFML.Graphics.RenderWindow rw)
        {
            Color oldColor = _screenFlashSprite.Color;
            Color col = _color;
            col.A = (byte)(_initialAlpha * Math.Pow(((EffectTotalTime - EffectRemainingTime) / EffectTotalTime), _power));
            _screenFlashSprite.Color = col;

            rw.Draw(_screenFlashSprite);
        }

        protected override void DoStartEffect()
        {

        }

        protected override void DoStopEffect()
        {

        }

        protected override void DoResetEffect()
        {
            throw new NotImplementedException();
        }
    }
}
