using JamUtilities;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate.Tower
{
    class ShotGroup : IGameObject
    {
        public List<Shot> members { get; private set; }

        public ShotGroup()
        {
            members = new List<Shot>();
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
            foreach (Shot go in members)
            {
                go.Update(to);
            }

            CleanUp();
        }

        private void CleanUp()
        {
            List<Shot> li = new List<Shot>();

            foreach (Shot e in members)
            {
                if (!e.IsDead())
                    li.Add(e);
            }
            members = li;
        }

        public Vector2f GetPosition()
        {
            return new Vector2f(0, 0);
        }

        public void SetPosition(Vector2f newPos)
        {
            T.TraceD("cannot set position on GameObjectGroup!");
        }

        public void Add(Shot go)
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
