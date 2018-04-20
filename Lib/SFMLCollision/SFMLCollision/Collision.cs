using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFMLCollision
{
    public class Collision
    {
        private static SFML.Window.Vector2f GetSpriteCenter (SFML.Graphics.Sprite Object)
	    {
		    SFML.Graphics.FloatRect AABB = Object.GetGlobalBounds();
            return new SFML.Window.Vector2f (AABB.Left + AABB.Width / 2.0f, AABB.Top + AABB.Height / 2.0f);
	    }
        private static SFML.Window.Vector2f GetSpriteCenter(SFML.Graphics.Shape Object)
        {
            SFML.Graphics.FloatRect AABB = Object.GetGlobalBounds();
            return new SFML.Window.Vector2f(AABB.Left + AABB.Width / 2.0f, AABB.Top + AABB.Height / 2.0f);
        }

        private static SFML.Window.Vector2f GetSpriteSize (SFML.Graphics.Sprite Object)
	    {
		     SFML.Graphics.IntRect OriginalSize = Object.TextureRect;
		    SFML.Window.Vector2f Scale = Object.Scale;
            return new SFML.Window.Vector2f(OriginalSize.Width * Scale.X, OriginalSize.Height * Scale.Y);
	    }

        private static SFML.Window.Vector2f GetSpriteSize(SFML.Graphics.Shape Object)
        {
            SFML.Graphics.IntRect OriginalSize = Object.TextureRect;
            SFML.Window.Vector2f Scale = Object.Scale;
            return new SFML.Window.Vector2f(OriginalSize.Width * Scale.X, OriginalSize.Height * Scale.Y);
        }

        public static bool CircleTest(SFML.Graphics.Shape Object1, SFML.Graphics.Shape Object2)
        {
            Vector2f Obj1Size = GetSpriteSize(Object1);
            Vector2f Obj2Size = GetSpriteSize(Object2);
            float Radius1 = (Obj1Size.X + Obj1Size.Y) / 4.0f;
            float Radius2 = (Obj2Size.X + Obj2Size.Y) / 4.0f;

            Vector2f Distance = GetSpriteCenter(Object1) - GetSpriteCenter(Object2);

            return (Distance.X * Distance.X + Distance.Y * Distance.Y <= (Radius1 + Radius2) * (Radius1 + Radius2));
        }

        public static bool CircleTest(SFML.Graphics.Sprite Object1, SFML.Graphics.Sprite Object2)
        {
            SFML.Window.Vector2f Obj1Size = GetSpriteSize(Object1);
            SFML.Window.Vector2f Obj2Size = GetSpriteSize(Object2);
		    float Radius1 = (Obj1Size.X + Obj1Size.Y) / 4.0f;
		    float Radius2 = (Obj2Size.X + Obj2Size.Y) / 4.0f;

            SFML.Window.Vector2f Distance = GetSpriteCenter(Object1) - GetSpriteCenter(Object2);

		    return (Distance.X * Distance.X + Distance.Y * Distance.Y <= (Radius1 + Radius2) * (Radius1 + Radius2));
	    }

        public static bool CircleTest(SFML.Graphics.Shape Object1, SFML.Graphics.Sprite Object2)
        {
            SFML.Window.Vector2f Obj1Size = GetSpriteSize(Object1);
            SFML.Window.Vector2f Obj2Size = GetSpriteSize(Object2);
            float Radius1 = (Obj1Size.X + Obj1Size.Y) / 4.0f;
            float Radius2 = (Obj2Size.X + Obj2Size.Y) / 4.0f;

            SFML.Window.Vector2f Distance = GetSpriteCenter(Object1) - GetSpriteCenter(Object2);

            return (Distance.X * Distance.X + Distance.Y * Distance.Y <= (Radius1 + Radius2) * (Radius1 + Radius2));
        }

        public static bool CircleTest(SFML.Graphics.Sprite Object1, SFML.Graphics.Shape Object2)
        {
            SFML.Window.Vector2f Obj1Size = GetSpriteSize(Object1);
            SFML.Window.Vector2f Obj2Size = GetSpriteSize(Object2);
            float Radius1 = (Obj1Size.X + Obj1Size.Y) / 4.0f;
            float Radius2 = (Obj2Size.X + Obj2Size.Y) / 4.0f;

            SFML.Window.Vector2f Distance = GetSpriteCenter(Object1) - GetSpriteCenter(Object2);

            return (Distance.X * Distance.X + Distance.Y * Distance.Y <= (Radius1 + Radius2) * (Radius1 + Radius2));
        }


        public static bool BoundingBoxTest(SFML.Graphics.Sprite Object1, SFML.Graphics.Sprite Object2)
        {
            OrientedBoundingBox OBB1 = new OrientedBoundingBox(Object1);
            OrientedBoundingBox OBB2 = new OrientedBoundingBox(Object2);

            return BoundingBoxTest(OBB1, OBB2);
        }

        public static bool BoundingBoxTest(SFML.Graphics.Sprite Object1, SFML.Graphics.Shape Object2)
        {
            OrientedBoundingBox OBB1 = new OrientedBoundingBox(Object1);
            OrientedBoundingBox OBB2 = new OrientedBoundingBox(Object2);

            return BoundingBoxTest(OBB1, OBB2);
        }

        public static bool BoundingBoxTest(SFML.Graphics.Shape Object1, SFML.Graphics.Sprite Object2)
        {
            OrientedBoundingBox OBB1 = new OrientedBoundingBox(Object1);
            OrientedBoundingBox OBB2 = new OrientedBoundingBox(Object2);

            return BoundingBoxTest(OBB1, OBB2);
        }
        
        public static bool BoundingBoxTest(SFML.Graphics.Shape Object1, SFML.Graphics.Shape Object2)
        {
            OrientedBoundingBox OBB1 = new OrientedBoundingBox(Object1);
            OrientedBoundingBox OBB2 = new OrientedBoundingBox(Object2);

            return BoundingBoxTest(OBB1, OBB2);
        }


        private static bool BoundingBoxTest(OrientedBoundingBox OBB1, OrientedBoundingBox OBB2)
        {
            // Create the four distinct axes that are perpendicular to the edges of the two rectangles
            SFML.Window.Vector2f[] Axes = new SFML.Window.Vector2f[4]
            {
                new SFML.Window.Vector2f (OBB1.Points[1].X-OBB1.Points[0].X,
                OBB1.Points[1].Y-OBB1.Points[0].Y),
                new SFML.Window.Vector2f (OBB1.Points[1].X-OBB1.Points[2].X,
                OBB1.Points[1].Y-OBB1.Points[2].Y),
                new SFML.Window.Vector2f (OBB2.Points[0].X-OBB2.Points[3].X,
                OBB2.Points[0].Y-OBB2.Points[3].Y),
                new SFML.Window.Vector2f (OBB2.Points[0].X-OBB2.Points[1].X,
                OBB2.Points[0].Y-OBB2.Points[1].Y)
            };

            for (int i = 0; i < 4; i++) // For each axis...
            {
                float MinOBB1 = 0.0f, MaxOBB1 = 0.0f, MinOBB2 = 0.0f, MaxOBB2 = 0.0f;

                // ... project the points of both OBBs onto the axis ...
                OBB1.ProjectOntoAxis(Axes[i], ref MinOBB1, ref MaxOBB1);
                OBB2.ProjectOntoAxis(Axes[i], ref MinOBB2, ref MaxOBB2);

                // ... and check whether the outermost projected points of both OBBs overlap.
                // If this is not the case, the Seperating Axis Theorem states that there can be no collision between the rectangles
                if (!((MinOBB2 <= MaxOBB1) && (MaxOBB2 >= MinOBB1)))
                    return false;
            }
            return true;
        }


        //public static bool PixelPerfectTest(SFML.Graphics.Sprite Object1, SFML.Graphics.Sprite Object2, byte AlphaLimit) 
        //{
        //    throw new System.NotImplementedException("missing bitmaskmanager");
        //}

    }
}
