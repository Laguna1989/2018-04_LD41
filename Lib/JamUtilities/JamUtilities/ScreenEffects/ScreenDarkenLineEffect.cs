using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities.ScreenEffects
{
    public class ScreenDarkenLineEffect : IScreenEffect
    {
        private System.Collections.Generic.List<Shape> _darkenShapeList;
        private System.Collections.Generic.List<float> _positions;

        int _numberOfSpritesToDraw;
        float _yScale;

        protected override void DoCreate()
        {
            _darkenShapeList = new List<Shape>();
            _positions = new List<float>();
        }

        protected override void DoUpdate(TimeObject timeObject)
        {
            while (_inverseFrequency <= 0.0f)
            {
                SpawnDarkenObject();
                //_inverseFrequency *= 0.5f;
                _inverseFrequency += _inverseFrequencyTotal;
            }

        }

        protected override void DoDraw(SFML.Graphics.RenderWindow rw)
        {
            foreach (var s in _darkenShapeList)
            {
                rw.Draw(s);
            }
        }

        protected override void DoStartEffect()
        {
            _positions = new List<float>();

            _numberOfSpritesToDraw = (int)Math.Ceiling((double)EffectTotalTime / _inverseFrequencyTotal) -1;
            if (_numberOfSpritesToDraw <= 0)
            {
                _numberOfSpritesToDraw = 1;
            }
            _yScale = _screenSize.Y / _numberOfSpritesToDraw;
            for (float posY = 0; posY < _screenSize.Y; posY += _yScale)
            {
                _positions.Add(posY);
            }
        }

        protected override void DoStopEffect()
        {
            _darkenShapeList.Clear();
        }

        protected override void DoResetEffect()
        {
        }

        private void SpawnDarkenObject()
        {
            if (_positions.Count != 0)
            {
                SFML.Graphics.RectangleShape rect = new RectangleShape(new Vector2f(_screenSize.X, _yScale));
                int positionInPositionList = RandomGenerator.Random.Next(0, _positions.Count);
                rect.Position = new Vector2f(0, _positions[positionInPositionList]);
                _positions.RemoveAt(positionInPositionList);
                rect.FillColor = SFML.Graphics.Color.Black;
                _darkenShapeList.Add(rect);
            }
            System.Console.WriteLine(_positions.Count);
        }
    }
}
