﻿using JamUtilities;
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
#if DEBUG
            GP.Window = new RenderWindow(new VideoMode(800, 600, 32), GP.WindowGameName);
#else
            GP.Window = new RenderWindow(new VideoMode(800, 600, 32), GP.WindowGameName,Styles.Fullscreen);
#endif
            GP.Window.Display();

            GP.music = new SFML.Audio.Music("../SFX/LD41OST.ogg");
            GP.music.Loop = true;
            GP.music.Volume = 60;
            GP.music.Play();

            try
            {
                SmartText._font = new Font("../GFX/font.ttf");
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

            StateIntro i = new StateIntro();
                
            Game myGame = new Game(i);
            i.setNextState(new StateMenu());

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





                Input.Update();
                JamUtilities.Mouse.Update();
                myGame.GetInput();
                if (myGame.CanBeQuit)
                {
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    {
                        GP.Window.Close();
                    }
                }


                

                myGame.Update(time);
                GP.Window.SetView(GP.WindowGameView);

                myGame.Draw(GP.Window);

                GP.Window.Display();
                endTime = Environment.TickCount;
            }
        }
    }
}
