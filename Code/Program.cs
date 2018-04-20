using JamUtilities;
using JamUtilities.Particles;
using JamUtilities.ScreenEffects;
using SFML.Graphics;
using SFML.Window;
using System;

namespace JamTemplate
{
    class Program
    {
        #region Event handlers

        static void OnClose(object sender, EventArgs e)
        {
            // Close the window when OnClose event is received
            SFML.Graphics.RenderWindow window = (SFML.Graphics.RenderWindow)sender;
            window.Close();
        }

        #endregion Event handlers

        static void Main(string[] args)
        {
            GP.Window = new RenderWindow(new VideoMode(800, 600, 32), GP.WindowGameName);
            GP.Window.Display();

            
            try
            {
                SmartText._font = new Font("../GFX/font.ttf");

                SmartText._lineLengthInChars = 18;
                SmartText._lineSpread = 1.2f;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            //////////////////////////////////////////////////////////////////////////////
            //


            GP.Window.SetFramerateLimit(60);
            GP.Window.SetVerticalSyncEnabled(true);

            GP.Window.Closed += new EventHandler(OnClose);

            Game myGame = new Game(new StateIntro());
            GP.Window.SetView(GP.WindowGameView);

            JamUtilities.Mouse.Window = GP.Window;

            int startTime = Environment.TickCount;
            int endTime = startTime;
            float time = 16.7f/1000; // 60 fps -> 16.7 ms per frame

            while (GP.Window.IsOpen())
            {
                GP.Window.DispatchEvents();

                if (startTime != endTime)
                {
                    time = (float)(endTime - startTime) / 1000.0f;
                }
                startTime = Environment.TickCount;

                

                
                

                myGame.GetInput();
                if (myGame.CanBeQuit)
                {
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    {
                        GP.Window.Close();
                    }
                }


                JamUtilities.Mouse.Update();

                myGame.Update(time);
                GP.Window.SetView(GP.WindowGameView);

                myGame.Draw(GP.Window);

                GP.Window.Display();
                endTime = Environment.TickCount;
            }
        }
    }
}
