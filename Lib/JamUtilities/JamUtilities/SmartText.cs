using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities
{
    public static class SmartText
    {
        public static Font _font;
        private static Text _text;
        public static int _lineLengthInChars = 18;
        public static float _lineSpread = 1.2f;


        public static void DrawText(string text, Vector2f position, RenderWindow rw)
        {
            DrawText(text, TextAlignment.LEFT, position, new Vector2f(1.0f, 1.0f), Color.White, rw);
        }

        public static void DrawText(string text, Vector2f position, Color col, RenderWindow rw)
        {
            DrawText(text, TextAlignment.LEFT, position, new Vector2f(1.0f, 1.0f), col, rw);
        }
        
        public static void DrawText(string text, Vector2f position, Color col, Vector2f scale, RenderWindow rw)
        {
            DrawText(text, TextAlignment.LEFT, position, scale, col, rw);
        }

        public static void DrawText(string text, TextAlignment ta, Vector2f position, Color col, RenderWindow rw)
        {
            DrawText(text, ta, position, new Vector2f(1.0f, 1.0f), col, rw);
        }

        public static void DrawText(string text, TextAlignment ta, Vector2f position, RenderWindow rw)
        {
            DrawText(text, ta, position, new Vector2f(1.0f, 1.0f), Color.White, rw);
        }
        public static void DrawText(string text, TextAlignment ta, Vector2f position, float scale,  RenderWindow rw)
        {
            DrawText(text, ta, position, new Vector2f(scale, scale), Color.White, rw);

        }

        public static void DrawText (string text, TextAlignment ta, Vector2f position, Vector2f scale, Color col , RenderWindow rw)
        {
           
            if (String.IsNullOrWhiteSpace(text))
            {
                return;
            }
            if (_text == null)
            {
                _text = new Text("",_font);
            }
            _text.Scale = scale; 
            _text.DisplayedString = text;
            _text.Position = GetPostionFromAlignment(ta, position);
            _text.Color = col;
            rw.Draw(_text);
        }

        public static void DrawTextWithLineBreaks(string text, TextAlignment ta, Vector2f position, Vector2f scale, Color col,  RenderWindow rw)
        {
            if (_font == null)
            {
                throw new Exception("font is null, cannot draw text.");
            }

            if (_text == null)
            {
                _text = new Text("", _font);
            }

            if (text.Length >= _lineLengthInChars)
            {
                int spacePos = text.IndexOf(" ", _lineLengthInChars/2, text.Length - (_lineLengthInChars/2));
                if (spacePos == -1)
                {
                    spacePos = _lineLengthInChars -1 ;
                }
                DrawTextWithLineBreaks(text.Substring(0, spacePos).TrimEnd(), ta, position, scale, col, rw);
                position.Y += _text.GetGlobalBounds().Height * _lineSpread;
                DrawTextWithLineBreaks(text.Substring(spacePos).TrimStart(), ta, position, scale, col, rw);
                return;
            }

            _text.DisplayedString = text;
            _text.Scale = scale;
            _text.Position = GetPostionFromAlignment(ta, position);
            
            _text.Color = col;
            rw.Draw(_text);
        }



        private static Vector2f GetPostionFromAlignment(TextAlignment ta, Vector2f position )
        {
            Vector2f textPosition = position;

            if (ta == TextAlignment.LEFT)
            {
                // nothing to do here
            }
            else if (ta == TextAlignment.MID)
            {
                textPosition.X -= _text.GetGlobalBounds().Width / 2.0f;
            }
            else if (ta == TextAlignment.RIGHT)
            {
                textPosition.X -= _text.GetGlobalBounds().Width;
            }
            return textPosition;
        }


    }

    

    public enum TextAlignment
    {
        LEFT,
        MID,
        RIGHT
    }
}
