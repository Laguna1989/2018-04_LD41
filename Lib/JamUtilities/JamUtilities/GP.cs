using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace JamUtilities
{
    public class GP
    {
        public static RenderWindow Window { get; set; } = null;
        public static View WindowGameView { get; set; } = null;
        public static Vector2u WindowSize { get { return Window.Size; } }

        public static string WindowGameName { get; private set; } = "$GameTitle$";

        
    }
}
