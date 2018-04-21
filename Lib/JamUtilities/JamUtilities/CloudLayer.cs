using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace JamUtilities
{
    public class CloudLayer : IGameObject
    {
        Sprite layer1;
        Sprite layer2;
        //Sprite layer3;

        Vector2f velocity = new Vector2f(-20, -25);

        public CloudLayer()
        {
            layer1 = new Sprite(TextureManager.GetTextureFromFileName("../GFX/clouds1.png"));
            layer2 = new Sprite(TextureManager.GetTextureFromFileName("../GFX/clouds2.png"));
            //layer3 = new Sprite(TextureManager.GetTextureFromFileName("../GFX/clouds3.png"));

            layer1.Scale = new Vector2f(3, 3);
            layer2.Scale = new Vector2f(3, 3);
            //layer3.Scale = new Vector2f(3, 3);


            layer1.Color = new Color(255, 255, 255, 20);
            layer2.Color = new Color(255, 255, 255, 20);
            //layer3.Color = new Color(255, 255, 255, 20);
        }

        public bool IsDead()
        {
            return false;
        }

        public void GetInput()
        {
            return;
        }

        public void Update(TimeObject to)
        {
            float dt = to.ElapsedGameTime;
            layer1.Position = new Vector2f(layer1.Position.X + velocity.X * dt, layer1.Position.Y + velocity.Y * dt);
            layer2.Position = new Vector2f(layer2.Position.X + velocity.X * dt* 2, layer2.Position.Y + velocity.Y * dt * 2);
            //layer3.Position = new Vector2f(layer3.Position.X + velocity.X * dt * 3, layer3.Position.Y + velocity.Y * dt * 3);

            WarpImage(layer1);
            WarpImage(layer2);
            //WarpImage(layer3);

        }

        private void WarpImage(Sprite spr)
        {
            if (spr.Position.X <= -spr.GetLocalBounds().Width * spr.Scale.X && spr.Position.Y <= -spr.GetLocalBounds().Height * spr.Scale.Y)
            {
                spr.Position = new Vector2f(spr.GetLocalBounds().Width * spr.Scale.X / 2, spr.GetLocalBounds().Height * spr.Scale.X / 2);
            }
        }

        public void Draw(RenderWindow rw)
        {
            rw.Draw(layer1);
            rw.Draw(layer2);
            //rw.Draw(layer3);
        }

        public Vector2f GetPosition()
        {
            return new Vector2f(0,0);
        }

        public void SetPosition(Vector2f newPos)
        {
            return;
        }
    }
}
