using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;
using System.Collections;

namespace JamUtilities
{
    public class GameObjectGroup : IGameObject
    {
        public List<IGameObject> members { get; private set; }

        public GameObjectGroup()
        {
            members = new List<IGameObject>();
        }


        public void Draw(RenderWindow rw)
        {
            foreach (IGameObject go in members)
            {
                go.Draw(rw);
            }
        }

        public void GetInput()
        {
            foreach (IGameObject go in members)
            {
                go.GetInput();
            }
        }

        public bool IsDead()
        {
            return false;
        }

        public void Update(TimeObject to)
        {
            foreach (IGameObject go in members)
            {
                go.Update(to);
            }
        }

        public Vector2f GetPosition()
        {
            return new Vector2f(0,0);
        }

        public void SetPosition(Vector2f newPos)
        {
            T.TraceD("cannot set position on GameObjectGroup!");
        }

        public void Add (IGameObject go)
        {
            if (go != null)
            {
                members.Add(go);
            }
        }

        // The IEnumerable interface requires implementation of method GetEnumerator.
        public IEnumerator GetEnumerator()
        {
            return members.GetEnumerator();
        }

        public int Count { get { return members.Count; } }
    }
}
