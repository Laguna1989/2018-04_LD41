using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities
{
    public class GameState : IGameObject
    {
        public List<IGameObject> _objects;

        protected Shape _overlay;
        

        public GameState()
        {           
            Create();
        }

        public void Add(IGameObject go)
        {
            _objects.Add(go);
        }

        /// <summary>
        /// function will be called upon construction Do only basic (lightwight) construction here
        /// </summary>
        public virtual void Create ()
        {
            _objects = new List<IGameObject>();
        }

        /// <summary>
        /// function will be called when this state becomes active 
        /// Load Resources here
        /// </summary>
        public virtual void Init()
        {
            _overlay = new RectangleShape(new Vector2f(GP.WindowSize.X, GP.WindowSize.Y));
            _overlay.FillColor = new Color(0, 0, 0, 255);

            Tweens.ShapeAlphaTween.createAlphaTween(_overlay);
        }

        virtual public void Draw(RenderWindow rw)
        {
            foreach (IGameObject go in _objects)
            {
                go.Draw(rw);
            }
        }

        virtual public void DrawOverlay(RenderWindow rw)
        {
            rw.Draw(_overlay);
        }

        virtual public void GetInput()
        {
            foreach(IGameObject go in _objects)
            {
                go.GetInput();
            }
        }

        public bool IsDead()
        {
            return false;
        }

        virtual public void Update(TimeObject to)
        {
            foreach (IGameObject go in _objects)
            {
                go.Update(to);
            }



        }

        public Vector2f GetPosition()
        {
            return new Vector2f(0, 0);
        }

        public void SetPosition(Vector2f newPos)
        {
#if DEBUG
            Console.WriteLine("cannot set position on GameState!");
#endif
        }
    }
}
