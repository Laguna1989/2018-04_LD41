using SFML.Window;

namespace JamUtilities
{
    public static class Mouse
    {
        public static SFML.Graphics.RenderWindow Window { get; set; }

        /// <summary>
        /// This Method gets the absolute MousePosition on your Screen/Desktop
        /// </summary>
        public static Vector2i MousePositionOnScreen { get; private set; }

        /// <summary>
        /// absolute Mouse Position in the Window from 0 to GP.WindowSize
        /// </summary>
        public static Vector2i MousePositionInWindow { get; private set; }

        /// <summary>
        /// mouse position in world Coordinates
        /// </summary>
        public static Vector2f MousePositionInWorld { get; private set; }

        public static bool IsMouseInWindow { get; private set; }


        public static bool pressed = false;
        public static bool released = false;
        public static bool justPressed = false;
        public static bool justReleased = false;
        


        public static void Update()
        {
            MousePositionOnScreen = SFML.Window.Mouse.GetPosition();
            
            if (Window != null)
            {
                MousePositionInWindow = SFML.Window.Mouse.GetPosition(Window);

                IsMouseInWindow = (MousePositionInWindow.X > 0 && MousePositionInWindow.X < GP.WindowSize.X &&
                                    MousePositionInWindow.Y > 0 && MousePositionInWindow.Y < GP.WindowSize.Y);
                MousePositionInWorld =  GP.Window.MapPixelToCoords(MousePositionInWindow);

            }
            else
            {
                MousePositionInWindow = MousePositionOnScreen;
            }
            justPressed = false;
            justReleased = false;
            if(SFML.Window.Mouse.IsButtonPressed(SFML.Window.Mouse.Button.Left))
            {
                 if (pressed == false)
                     justPressed = true;
                else
                    justPressed = false;
                pressed = true;
                released = false;
            }
            else
            {
                if (pressed == true)
                    justReleased = true;
                else
                    justReleased = false;
                pressed = false;
                released = true;
            }
        }
    }
}
