using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

namespace JamUtilities
{
    public class TextButton : IGameObject
    {
        protected Sprite sprNormal;
        protected Sprite sprOver;
        protected Sprite sprPressed;
        protected Color tc;

        public int mode = 0;   // 0 normal, 1 over, 2 pressed

        public String text;

        public bool active = true;
        public bool visible = true;
        public bool hasBeenVisible = true;

        public Action PressCallback = null;

        protected Vector2f textOffset = new Vector2f(3, 3);


        static private SoundBuffer sndbufClick = null;
        private Sound sndClick;

        public TextButton(String t, Action cb)
        {
            text = t;
            PressCallback = cb;
            sprNormal = new Sprite(TextureManager.GetTextureFromFileName("../GFX/btn_normal.png"));
            sprOver = new Sprite(TextureManager.GetTextureFromFileName("../GFX/btn_over.png"));
            sprPressed = new Sprite(TextureManager.GetTextureFromFileName("../GFX/btn_down.png"));
            sprNormal.Scale = sprOver.Scale = sprPressed.Scale = new Vector2f(2, 2);
            tc = Color.White;

            if (sndbufClick == null)
                sndbufClick = new SoundBuffer("../SFX/click.wav");
            sndClick = new Sound(sndbufClick);
        }

		public TextButton(String t, Action cb, Vector2f scale)
		{
			text = t;
			PressCallback = cb;
			sprNormal = new Sprite(TextureManager.GetTextureFromFileName("../GFX/btn_normal.png"));
			sprNormal.Scale = scale;

			sprOver = new Sprite(TextureManager.GetTextureFromFileName("../GFX/btn_over.png"));
			sprOver.Scale = scale;

			sprPressed = new Sprite(TextureManager.GetTextureFromFileName("../GFX/btn_down.png"));
			sprPressed.Scale = scale;

			tc = Color.White;


			if (sndbufClick == null)
				sndbufClick = new SoundBuffer("../SFX/click.wav");
			sndClick = new Sound(sndbufClick);
		}

		public virtual void Draw(RenderWindow rw)
        {
            if (!visible)
                return;

            if (mode == 0)
                rw.Draw(sprNormal);
            else if (mode == 1)
                rw.Draw(sprOver);
            else
                rw.Draw(sprPressed);
            SmartText.DrawText(text, new Vector2f(sprNormal.Position.X + textOffset.X, sprNormal.Position.Y + textOffset.Y), tc, rw);
        }

        public void GetInput()
        {
            mode = 0;
            if (active && visible)
            {
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
                    {
                        PressCallback();
                        sndClick.Play();
                    }

                        
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

        public virtual void SetPosition(Vector2f newPos)
        {
            sprNormal.Position = sprOver.Position = sprPressed.Position = newPos;
        }

		public Vector2f getSize()
		{
			return new Vector2f(sprNormal.Texture.Size.X * sprNormal.Scale.X, sprNormal.Texture.Size.Y * sprNormal.Scale.Y);
		}

		public void SetTextOffset(Vector2f offset)
		{
			textOffset = offset;
		}

        public virtual void Update(TimeObject to)
        {
            sprNormal.Color = sprOver.Color = sprPressed.Color = tc = ((active)? Color.White: new Color(100,100,100));
            
            return;
        }
    }
}
