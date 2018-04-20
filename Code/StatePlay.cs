using JamUtilities;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate
{
    class StatePlay : JamUtilities.GameState
    {

        public Shape shp;
        public override void Init()
        {
            base.Init();


            for (int i = 0; i != 10; ++i)
            {
                Animation an = new Animation("../GFX/Soldier.png", new Vector2u(24, 24));
                an.Add("idle", new List<int>(new int[] { 0, 1, 2, 3, 4, 5 }), 0.3f);
                an.Play("idle");
                an.Position = RandomGenerator.GetRandomVector2fInRect(new FloatRect(0,0,GP.WindowSize.X, GP.WindowSize.Y));
                Add(an);
            }

            shp = new RectangleShape(new Vector2f(50, 50));
            shp.Position = new Vector2f(100, 100);
        }

        public override void Draw(RenderWindow rw)
        {
            base.Draw(rw);

            rw.Draw(shp);
        }

        public override void Update(TimeObject timeObject)
        {
            base.Update(timeObject);

            //Console.WriteLine("gs: " + shp.Scale.X.ToString());

            if (Input.pressed[Keyboard.Key.D])
            {
                GP.WindowGameView.Move(new Vector2f(100 * timeObject.ElapsedGameTime, 0));
            }
            else if (Input.pressed[Keyboard.Key.A])
            {
                GP.WindowGameView.Move(new Vector2f(-100 * timeObject.ElapsedGameTime, 0));
            }

            if (Input.justPressed[Keyboard.Key.K])
            {
                JamUtilities.Tweens.ShapeScaleTween.createShapeTween(shp, 2, 2,null, PennerDoubleAnimation.EquationType.CubicEaseIn);
            }

            if (Input.justPressed[Keyboard.Key.L])
            {
                JamUtilities.Tweens.ShapeScaleTween.createShapeTween(shp, -0.5f, 2, null, PennerDoubleAnimation.EquationType.SineEaseOut);
            }
            
        }
    }
}
