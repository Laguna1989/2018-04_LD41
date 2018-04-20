using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFMLCollision
{
    class BitmaskManager
    {
        public byte GetPixel(byte[] mask, ref SFML.Graphics.Texture tex, int x, int y) 
        {
			if (x>tex.Size.X||y>tex.Size.Y)
				return 0;

			return mask[x+y*tex.Size.X];
		}


    }
}
