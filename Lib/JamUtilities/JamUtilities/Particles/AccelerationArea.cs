using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities.Particles
{
    public class AccelerationArea
    {
        public FloatRect Area { get; set; }
        public Vector2f Acceleration { get; set; }

        public AccelerationArea (FloatRect rect, Vector2f acceleration)
        {
            Area = rect;
            Acceleration = acceleration;
        }
    }
}
