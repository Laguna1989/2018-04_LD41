﻿using JamUtilities;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate.Tower
{
    class TowerGroup : IGameObject
    {
        public List<Tower> members { get; private set; }

        public TowerGroup()
        {
            members = new List<Tower>();
        }

        public void Clear()
        {
            members.Clear();
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
            foreach (Tower go in members)
            {
                go.Update(to);
            }

            CleanUp();
        }

        private void CleanUp()
        {
            List<Tower> li = new List<Tower>();

            foreach (Tower e in members)
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

        public void Add(Tower go)
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
