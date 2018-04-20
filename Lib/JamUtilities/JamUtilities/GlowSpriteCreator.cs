using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace JamUtilities
{
    public static class GlowSpriteCreator
    {
        public static void CreateRadialGlow(out Texture targetTexture, uint size, Color col, float opacity = 0.2f, PennerDoubleAnimation.EquationType type = PennerDoubleAnimation.EquationType.Linear )
        {
            Image targetImage = new Image(size, size);
            
            Vector2u centerPosition = new Vector2u(size / 2, size / 2);
            float distanceToCenterMax = (float)Math.Sqrt(centerPosition.X * centerPosition.X + centerPosition.Y * centerPosition.Y)/1.5f;
            for (uint i = 0; i != size; i++)
            {
                for (uint j = 0; j != size; j++)
                {
                    Color pixelCol = col;

                    Vector2u distanceToCenter = new Vector2u(centerPosition.X - i, centerPosition.Y - j);
                    float distance = (float)Math.Sqrt(distanceToCenter.X * distanceToCenter.X + distanceToCenter.Y * distanceToCenter.Y);
                    float newAlpha = 255.0f * opacity * (1.0f - (float)PennerDoubleAnimation.GetValue(type,distance,0, 1, distanceToCenterMax));
                    if (newAlpha < 0.0f)
                    {
                        newAlpha = 0.0f;
                    }
                    //Console.WriteLine(newAlpha);
                    pixelCol.A = (byte)newAlpha;
                    targetImage.SetPixel(i, j, pixelCol);
                }
            }

            targetTexture = new Texture(targetImage);
        }



        public static void CreateLinearAssymetricGlowInOut(out Texture targetTexture, uint sizeX, uint sizeY, Color col, float opacity = 0.2f,PennerDoubleAnimation.EquationType type1 = PennerDoubleAnimation.EquationType.Linear,  PennerDoubleAnimation.EquationType type2 = PennerDoubleAnimation.EquationType.Linear, ShakeDirection direction = ShakeDirection.UpDown)
        {
            Image targetImage = new Image(sizeX, sizeY);

            float centerPosition = 0.0f;
            if (direction == ShakeDirection.UpDown)
            {
                centerPosition = sizeY / 2.0f;
            }
            else if (direction == ShakeDirection.LeftRight)
            {
                centerPosition = sizeX / 2.0f;
            }

            for (uint i = 0; i != sizeX; i++)
            {
                for (uint j = 0; j != sizeY; j++)
                {
                    Color pixelCol = col;

                    float distanceToCenter = 0.0f;
                    if (direction == ShakeDirection.UpDown)
                    {
                        distanceToCenter = (float)(centerPosition - j);
                    }
                    else if (direction == ShakeDirection.LeftRight)
                    {
                        distanceToCenter = (float)(centerPosition - i);
                    }
                    float newAlpha;
                    if (distanceToCenter > 0.0f)
                    {
                        newAlpha = 255.0f * opacity * (1.0f - (float)PennerDoubleAnimation.GetValue(type1, distanceToCenter, 0, 1, centerPosition));
                    }
                    else
                    {
                        newAlpha = 255.0f * opacity * (1.0f - (float)PennerDoubleAnimation.GetValue(type2, distanceToCenter, 0, 1, centerPosition));
                    }
                    if (newAlpha < 0.0f)
                    {
                        newAlpha = 0.0f;
                    }
                    //Console.WriteLine(newAlpha);
                    pixelCol.A = (byte)newAlpha;
                    targetImage.SetPixel(i, j, pixelCol);
                }
            }

            targetTexture = new Texture(targetImage);
        }



        public static void CreateLinearGlowInOut(out Texture targetTexture,  uint sizeX, uint sizeY, Color col, float opacity = 0.2f, PennerDoubleAnimation.EquationType type = PennerDoubleAnimation.EquationType.Linear, ShakeDirection direction = ShakeDirection.UpDown)
        {
            Image targetImage = new Image(sizeX, sizeY);

            float centerPosition = 0.0f;
            if (direction == ShakeDirection.UpDown)
            {
                centerPosition = sizeY / 2.0f;
            }
            else if (direction == ShakeDirection.LeftRight)
            {
                centerPosition = sizeX / 2.0f;
            }
           
            for (uint i = 0; i != sizeX; i++)
            {
                for (uint j = 0; j != sizeY; j++)
                {
                    Color pixelCol = col;

                    float distanceToCenter = 0.0f;
                    if(direction == ShakeDirection.UpDown)
                    {
                        distanceToCenter = (float)Math.Abs(centerPosition - j);
                    }
                    else if (direction == ShakeDirection.LeftRight)
                    {
                        distanceToCenter = (float)Math.Abs(centerPosition - i);
                    }
                    float newAlpha = 255.0f * opacity * (1.0f - (float)PennerDoubleAnimation.GetValue(type, distanceToCenter, 0, 1, centerPosition));
                    if (newAlpha < 0.0f)
                    {
                        newAlpha = 0.0f;
                    }
                    //Console.WriteLine(newAlpha);
                    pixelCol.A = (byte)newAlpha;
                    targetImage.SetPixel(i, j, pixelCol);
                }
            }

            targetTexture = new Texture(targetImage);
        }
    }
}
