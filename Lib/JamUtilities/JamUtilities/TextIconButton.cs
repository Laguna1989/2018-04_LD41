using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamUtilities
{
    public class TextIconButton : TextButton
    {
        public Sprite icon;
        public TextIconButton (string t, string i, Action cb ) : base (t,cb)
        {
            icon = new Sprite(TextureManager.GetTextureFromFileName(i));
            icon.Scale = new SFML.Window.Vector2f(2, 2);
            textOffset = new SFML.Window.Vector2f( 5 + 32 + 8 , 5);
        }

        public override void Update(TimeObject to)
        {
            base.Update(to);
            icon.Color = tc;
            icon.Position = new SFML.Window.Vector2f(sprNormal.Position.X + 8, sprNormal.Position.Y + 8);
        }

        public override void Draw(RenderWindow rw)
        {
            base.Draw(rw);
            rw.Draw(icon);
        }




    }
}
