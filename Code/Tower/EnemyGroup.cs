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
    class EnemyGroup : IGameObject
    {
        public List<Enemy> members { get; private set; }

        public delegate void DelteCallBackType(Enemy e);
        public DelteCallBackType DeleteCallback = null;

        public  void Clear()
        {
            members.Clear();
        }


        public EnemyGroup()
        {
            members = new List<Enemy>();
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
            foreach (Enemy go in members)
            {
                go.Update(to);
            }

            CleanUp();
        }

        private void CleanUp()
        {
            List<Enemy> li = new List<Enemy>();

            foreach(Enemy e in members)
            {
                if (!e.IsDead())
                    li.Add(e);
                else
                {
                    DeleteCallback.Invoke(e);
                }
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

        public void Add(Enemy go)
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
