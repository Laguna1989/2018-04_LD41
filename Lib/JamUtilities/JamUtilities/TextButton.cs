using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities
{
    public class TextButton : IGameObject
    {
        private Sprite sprNormal;
        private Sprite sprOver;
        private Sprite sprPressed;

        private int mode = 0;   // 0 normal, 1 over, 2 pressed

        public String text;

        public bool active = true;

        public Action PressCallback = null;

        public TextButton(String t, Action cb)
        {
            text = t;
            PressCallback = cb;
            sprNormal = new Sprite(TextureManager.GetTextureFromFileName("../GFX/btn_normal.png"));
            sprOver = new Sprite(TextureManager.GetTextureFromFileName("../GFX/btn_over.png"));
            sprPressed = new Sprite(TextureManager.GetTextureFromFileName("../GFX/btn_down.png"));
            sprNormal.Scale = sprOver.Scale = sprPressed.Scale = new Vector2f(2, 2);
        }

        public void Draw(RenderWindow rw)
        {
            if (mode == 0)
                rw.Draw(sprNormal);
            else if (mode == 1)
                rw.Draw(sprOver);
            else
                rw.Draw(sprPressed);
            SmartText.DrawText(text, new Vector2f(sprNormal.Position.X + 5, sprNormal.Position.Y + 5), rw);
        }

        public void GetInput()
        {
            if (active)
            {
                mode = 0;
                if (containsPoint(new Vector2f(Mouse.MousePositionInWindow.X, Mouse.MousePositionInWindow.Y)))
                {
                    mode = 1;
                    if (Mouse.pressed)
                    {
                        mode = 2;
                    }
                }
                if (mode == 1 && Mouse.justReleased)
                {
                    if (PressCallback != null)
                        PressCallback();                    
                }
                
            }
            else
            {
                mode = 1;
            }
        }

        public bool containsPoint(Vector2f p)
        {
            if (p.X > GetPosition().X && p.Y > GetPosition().Y)
                if (p.X < GetPosition().X + sprNormal.GetLocalBounds().Width * sprNormal.Scale.X && p.Y < GetPosition().Y + sprNormal.GetLocalBounds().Height * sprNormal.Scale.Y)
                    return true;

            return false;
        }

        public Vector2f GetPosition()
        {
            return sprNormal.Position;
        }

        public bool IsDead()
        {
            return false;
        }

        public void SetPosition(Vector2f newPos)
        {
            sprNormal.Position = sprOver.Position = sprPressed.Position = newPos;
        }

        public void Update(TimeObject to)
        {
            sprNormal.Color = sprOver.Color = sprPressed.Color = ((active)? Color.White: new Color(100,100,100));
            return;
        }
    }
}
