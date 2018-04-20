using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities
{
    public class AreatricCloud
    {

        private System.Collections.Generic.List<Vector2f> _offsetList;
        public static uint NumberOfCloudParticles = 450;
        private float _cloudRadius;
        private float _puffsize;
        private Color _cloudColor;
        public Vector2f Position { get; set; }
        private CircleShape circ;

        private RenderTexture _cloudTexture;
        private Sprite _cloudSprite;

        // use this for Camera offset
        public static Vector2f GlobalPositionOffset;

        public AreatricCloud (Vector2f position, Color col,Color backgroundColor, float puffSize = 9, float cloudRadius = 45)
        {
            _offsetList = new List<Vector2f>();
            _puffsize = puffSize;
            _cloudRadius = cloudRadius;
            _cloudColor = col;
            _cloudColor.A = 15;
            Position = position;
            circ = new CircleShape(_puffsize);
            circ.FillColor = _cloudColor;

            // determine minmax values for texture creation
            Vector2f minValues = new Vector2f();
            Vector2f maxvalues = new Vector2f();
            for (uint i = 0; i != NumberOfCloudParticles; i++)
            {
                // calculate positions
                Vector2f offset = RandomGenerator.GetRandomVector2fInEllipse(cloudRadius*1.5f, cloudRadius/1.25f);
                _offsetList.Add(offset);

                
                if (offset.X > maxvalues.X)
                {
                    maxvalues.X = offset.X;
                }
                else if (offset.X < minValues.X)
                {
                    minValues.X = offset.X;
                }

                if (offset.Y > maxvalues.Y)
                {
                    maxvalues.Y = offset.Y;
                }
                if (offset.Y < minValues.Y)
                {
                    minValues.Y = offset.Y;
                }
            }

            _cloudTexture = new RenderTexture((uint)(maxvalues.X - minValues.X + 4*puffSize), (uint)(maxvalues.Y - minValues.Y + 4 * puffSize), false);
            Vector2f center = new Vector2f(minValues.X + maxvalues.X / 2.0f - puffSize/2, minValues.Y + maxvalues.Y / 2.0f - puffSize/2);
            //Console.WriteLine(maxvalues.X - minValues.X);
            //Console.WriteLine(center.X);
            backgroundColor.A = 0;
            _cloudTexture.Clear(backgroundColor);
            foreach (var o in _offsetList)
            {
                circ.Position = o - 2.0f*center;
                _cloudTexture.Draw(circ, new RenderStates(BlendMode.Alpha));
            }
            _cloudSprite = new Sprite(_cloudTexture.Texture);

        }

        public void Draw(RenderWindow rw)
        {
            //foreach (var o in _offsetList)
            //{
            //    circ.Position = Position + o;
            //    rw.Draw(circ);
            //}
            _cloudSprite.Position = Position - GlobalPositionOffset;
            rw.Draw(_cloudSprite, new RenderStates(BlendMode.Alpha));

        }
    }
}
