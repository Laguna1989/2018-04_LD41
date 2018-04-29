using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace JamUtilities
{
    public class ParticleSystem : IGameObject
    {
        public List<SmartSprite> members;
        public Vector2f acceleration = new Vector2f(0, 0);

        public delegate void StartCallbackType(SmartSprite s);


        public ParticleSystem(string fileName, IntRect r  , int count)
        {
            members = new List<SmartSprite>();
            for (int i = 0; i != count; ++i)
            {
                SmartSprite s = new SmartSprite(TextureManager.GetTextureFromFileName(fileName), r);
                s.Position = new Vector2f(-500, 0);
                members.Add(s);
            }
        }

        public void Draw(RenderWindow rw)
        {
            foreach (SmartSprite s in members)
            {
                //T.TraceD(s.Position.ToString());
                s.Draw(rw);
            }
        }

        public void GetInput()
        {
            return;
        }

        public Vector2f GetPosition()
        {
            return new Vector2f(0, 0);
        }

        public bool IsDead()
        {
            return false;
        }

        public void SetPosition(Vector2f newPos)
        {
            return;
        }

        public virtual void Start(StartCallbackType startcb)
        {
            foreach(SmartSprite s in members)
            {
                startcb(s);
            }
        }

        public void Update(TimeObject to)
        {
            foreach (SmartSprite s in members)
            {
                s.Update(to);
                s.velocity += acceleration * to.ElapsedGameTime;
            }
        }
    }
}
