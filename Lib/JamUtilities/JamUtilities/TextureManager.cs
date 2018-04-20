using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace JamUtilities
{
    public class TextureManager
    {
        private static Dictionary<string, Texture> _textureDictionary;

        public static SFML.Graphics.Texture GetTextureFromFileName(string filepath)
        {
            Texture ret = null;

            if (_textureDictionary == null)
            {
                _textureDictionary = new Dictionary<string, Texture>();
            }
            if (_textureDictionary.ContainsKey(filepath))
            {
                ret = _textureDictionary[filepath];
            }
            else
            {
                ret = new Texture(filepath);
                _textureDictionary.Add(filepath, ret);
            }

            return ret;
        }
        
    }
}
