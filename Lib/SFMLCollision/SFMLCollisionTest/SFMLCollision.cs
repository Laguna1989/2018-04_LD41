using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SFMLCollision;

namespace SFMLCollisionTest
{
    [TestClass]
    public class SFMLCollision
    {
        [TestMethod]
        public void CircleTest()
        {

            System.Console.Out.WriteLine("Running Circle Test");
            SFML.Graphics.RenderTexture MyTexture = new SFML.Graphics.RenderTexture(100, 100);

            MyTexture.Clear(SFML.Graphics.Color.Black);
            

            SFML.Graphics.Sprite TestSprite1 = new SFML.Graphics.Sprite();
            TestSprite1.Texture = MyTexture.Texture;

            TestSprite1.Position = new SFML.Window.Vector2f(0,0);

            SFML.Graphics.Sprite TestSprite2 = new SFML.Graphics.Sprite();
            TestSprite2.Texture = MyTexture.Texture;
            TestSprite2.Position = new SFML.Window.Vector2f(10,10);


            Assert.IsTrue(Collision.CircleTest(TestSprite1, TestSprite2));

            TestSprite2.Position = new SFML.Window.Vector2f(0, 200);
            Assert.IsFalse(Collision.CircleTest(TestSprite1, TestSprite2));

            TestSprite2.Position = new SFML.Window.Vector2f(0, 100);
            Assert.IsTrue(Collision.CircleTest(TestSprite1, TestSprite2));
        }


        [TestMethod]
        public void BoundingBoxTest()
        {

            System.Console.Out.WriteLine("Running bounding box Test");
            SFML.Graphics.RenderTexture MyTexture = new SFML.Graphics.RenderTexture(100, 100);

            MyTexture.Clear(SFML.Graphics.Color.White);

            SFML.Graphics.Sprite TestSprite1 = new SFML.Graphics.Sprite();
            TestSprite1.Texture = MyTexture.Texture;

            TestSprite1.Position = new SFML.Window.Vector2f(0, 0);

            SFML.Graphics.Sprite TestSprite2 = new SFML.Graphics.Sprite();
            TestSprite2.Texture = MyTexture.Texture;
            TestSprite2.Position = new SFML.Window.Vector2f(10, 10);


            Assert.IsTrue(Collision.BoundingBoxTest(TestSprite1, TestSprite2));

            TestSprite2.Position = new SFML.Window.Vector2f(0, 200);
            Assert.IsFalse(Collision.BoundingBoxTest(TestSprite1, TestSprite2));

            TestSprite2.Position = new SFML.Window.Vector2f(0, 100);
            Assert.IsTrue(Collision.BoundingBoxTest(TestSprite1, TestSprite2));

            TestSprite2.Position = new SFML.Window.Vector2f(-50, -50);
            Assert.IsTrue(Collision.BoundingBoxTest(TestSprite1, TestSprite2));
        }
    }
}
