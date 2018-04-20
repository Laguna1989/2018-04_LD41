using JamUtilities;
using JamUtilities.Tweens;
using SFML;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate
{
    class StateIntro : JamUtilities.GameState
    {

        #region Fields

        private Sprite _logo;
        
        private Shader _pixelate;
        private RenderStates _states;
        private float _age;
        private float _introTime = 2.75f;    // 1 second fade in, 0.5 second stay, 1 second fade out
        
        public GameState nextState = new StateMenu();
        private bool ending = false;


        #endregion Fields

        public StateIntro(GameState next = null )
        {
            if (next != null)
                nextState = next;
        }

        public override void Init()
        {
            base.Init();
            _age = 0;
            
            try
            {
                LoadGraphics();
            }
            catch (LoadingFailedException e)
            {
                Console.WriteLine(e);
                throw;
            }

            Timer.Start(_introTime - 1.5f, () => ShapeAlphaTween.createAlphaTween(_overlay, 255, 1));
            Timer.Start(_introTime, () => End() );
        
        }


        private void End()
        {
            if (!ending)
            {
                ending = true;
                Game.SwitchState(nextState);
            }
        }

        public override void Draw(RenderWindow rw)
        {
            base.Draw(rw);

            if (Shader.IsAvailable)
            {
                rw.Draw(_logo, _states);
            }
            else
            {
                rw.Draw(_logo);
            }
        }

        public override void Update(TimeObject to)
        {
            base.Update(to);

            _age += to.ElapsedGameTime;
            
            if (Input.justPressed[Keyboard.Key.Escape] ||
                Input.justPressed[Keyboard.Key.Return] ||
                Input.justPressed[Keyboard.Key.Space])
            {
                End();
            }

            if (_age >= 1)
            {
                float v = _age - 1;
                if (Shader.IsAvailable)
                {
                    _pixelate.SetParameter("strength", v * 50.0f);
                }
            }
        }

        private void LoadGraphics()
        {
            _logo = new Sprite(new Texture("../GFX/runvs.png"));
            

            if (Shader.IsAvailable)
            {
                _pixelate = new Shader(null, "../GFX/pixelate.frag");
                _pixelate.SetParameter("tex", _logo.Texture);
                _pixelate.SetParameter("strength", 0.0f);
                _states = new RenderStates(_pixelate);
            }
        }

        
}
}
