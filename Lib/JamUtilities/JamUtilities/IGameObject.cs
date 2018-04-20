using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities
{
    public interface IGameObject
    {
        bool IsDead();
        void GetInput();
        void Update(TimeObject to);
        void Draw(RenderWindow rw);

        Vector2f GetPosition();
        void SetPosition(Vector2f newPos);
    }
}
