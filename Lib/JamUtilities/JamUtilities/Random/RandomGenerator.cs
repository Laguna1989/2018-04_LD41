using System;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities
{
    public static class RandomGenerator
    {
        static public Random Random { get { if (_random == null) { _random = new Random(); } return _random; } }
        static private Random _random;

        static public Vector2f GetRandomVector2fSquare(float max)
        {
            return new Vector2f((float)(Random.NextDouble() - 0.5f) * 2.0f * max, (float)(Random.NextDouble() - 0.5f) * 2.0f * max);
        }
        static public Vector2f GetRandomVector2f(Vector2f xrange, Vector2f yrange)
        {
            CheckRanges(ref xrange);
            CheckRanges(ref yrange);

            float xDistance = xrange.Y - xrange.X;
            float yDistance = yrange.Y - yrange.X;

            return new Vector2f(((float)Random.NextDouble() * xDistance) + xrange.X, ((float)Random.NextDouble() * yDistance) + yrange.X);
        }

        static public Vector2i GetRandomVector2iInRect (IntRect rect )
        {
            // the plus 1 is because otherwise the upper boundary would be excluded
            return new Vector2i(Random.Next(rect.Left, rect.Left + rect.Width + 1), Random.Next(rect.Top, rect.Top + rect.Height + 1));
        }

        static public Vector2f GetRandomVector2fInRect(RectangleShape shape)
        {
            Vector2f ret = GetRandomVector2f(
                new Vector2f(shape.GetGlobalBounds().Left, shape.GetGlobalBounds().Left + shape.GetGlobalBounds().Width),
                new Vector2f(shape.GetGlobalBounds().Top, shape.GetGlobalBounds().Top + shape.GetGlobalBounds().Height));
            return ret;
        }
        static public Vector2f GetRandomVector2fInRect(FloatRect shape)
        {
            Vector2f ret = GetRandomVector2f(
                new Vector2f(shape.Left, shape.Left + shape.Width),
                new Vector2f(shape.Top, shape.Top + shape.Height));
            return ret;
        }

        public static T GetRandomEnumValue<T>(T enumeration)
        {
            var values = Enum.GetValues(enumeration.GetType());

            return (T)values.GetValue(_random.Next(values.Length));
        }

        public static int Int(int min = 0, int max = 100)
        {
            return Random.Next(min, max);
        }
        public static int Int(int max = 100)
        {
            return Random.Next(max);
        }

        static public Vector2f GetRandomVector2fInCircle(float radius)
        {
            double angle = Random.NextDouble() * 2 * Math.PI;
            return new Vector2f((float)(Math.Cos(angle) * Random.NextDouble() * radius), (float)(Math.Sin(angle) * Random.NextDouble() * radius));
        }
        static public Vector2f GetRandomVector2fOnCircle(float radius)
        {
            double angle = Random.NextDouble() * 2 * Math.PI;
            return new Vector2f((float)Math.Cos(angle) * radius, (float)Math.Sin(angle) * radius);
        }

        static public Vector2f GetRandomVector2fInEllipse(float radiusX, float radiusY)
        {
            double angle = Random.NextDouble() * 2 * Math.PI;
            return new Vector2f((float)(Math.Cos(angle) * Random.NextDouble() * radiusX), (float)(Math.Sin(angle) * Random.NextDouble() * radiusY));
        }
        static public Vector2f GetRandomVector2fOnEllipse(float radiusX, float radiusY)
        {
            double angle = Random.NextDouble() * 2 * Math.PI;
            return new Vector2f((float)Math.Cos(angle) * radiusX, (float)Math.Sin(angle) * radiusY);
        }


        static private void CheckRanges(ref Vector2f range)
        {
            //if (range == null)
            //{
            //    throw new ArgumentNullException("range", "To create Random Numbers therange must exist." );
            //}
            if (range.X == range.Y)
            {
                throw new ArgumentOutOfRangeException("range", "To create Random Numbers the range must be existing.");
            }

            if (range.X > range.Y)
            {
                float tmp = range.X;
                range.X = range.Y;
                range.Y = tmp;
            }
        }
    }
}
