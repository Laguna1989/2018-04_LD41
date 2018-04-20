using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFMLCollision
{
    class OrientedBoundingBox
    {

        public OrientedBoundingBox (SFML.Graphics.Sprite Object) // Calculate the four points of the OBB from a transformed (scaled, rotated...) sprite
		{
            Points = new SFML.Window.Vector2f[4];
            SFML.Graphics.Transform trans = Object.Transform;
            SFML.Graphics.IntRect local = Object.TextureRect;

			Points[0] = trans.TransformPoint(0.0f, 0.0f);
			Points[1] = trans.TransformPoint(local.Width, 0.0f);
			Points[2] = trans.TransformPoint(local.Width, local.Height);
			Points[3] = trans.TransformPoint(0.0f, local.Height);
		}
        public OrientedBoundingBox(SFML.Graphics.Shape Object) // Calculate the four points of the OBB from a transformed (scaled, rotated...) sprite
        {
            Points = new SFML.Window.Vector2f[4];
            SFML.Graphics.Transform trans = Object.Transform;
            
                

            Points[0] = trans.TransformPoint(0.0f, 0.0f);
            Points[1] = trans.TransformPoint(Object.GetLocalBounds().Width, 0.0f);
            Points[2] = trans.TransformPoint(Object.GetLocalBounds().Width, Object.GetLocalBounds().Height);
            Points[3] = trans.TransformPoint(0.0f, Object.GetLocalBounds().Height);
        }

        public void ProjectOntoAxis (SFML.Window.Vector2f Axis, ref float Min, ref float Max) // Project all four points of the OBB onto the given axis and return the dotproducts of the two outermost points
		{
			Min = (Points[0].X*Axis.X+Points[0].Y*Axis.Y);
			Max = Min;
			for (int j = 1; j<4; j++)
			{
				float Projection = (Points[j].X*Axis.X+Points[j].Y*Axis.Y);

				if (Projection<Min)
					Min=Projection;
				if (Projection>Max)
					Max=Projection;
			}
		}


        public SFML.Window.Vector2f[] Points;

    }
}
