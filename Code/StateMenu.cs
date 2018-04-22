using JamUtilities;
using JamUtilities.ScreenEffects;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate
{
    class StateMenu : GameState
    {

        private bool exiting = false;

        private TextButton tb_startGame;
        private TextButton tb_mute;

        private Animation heart1;
        private Animation heart2;


        public override void Init()
        {
            base.Init();

            tb_startGame = new TextButton(" Start Game", StartGame);
            tb_startGame.SetPosition(new Vector2f(400-96, 220));
            Add(tb_startGame);

            tb_mute = new TextButton(" Mute Music", MuteMusic);
            tb_mute.SetPosition(new Vector2f(400 - 96, 220 + 24*2 + 8));
            Add(tb_mute);

            heart1 = new Animation("../GFX/heart.png", new Vector2u(16, 16));
            heart1.SetPosition(new Vector2f(400-96-32, 220 + 8));
            heart1.SetScale(0.75f, 0.75f);
            heart1.Add("idle", new List<int>(new int[] { 0, 1, 2, 3 }), 0.127f);
            heart1.Play("idle");
            Add(heart1);

            heart2 = new Animation("../GFX/heart.png", new Vector2u(16, 16));
            heart2.SetPosition(new Vector2f(400 + 96 +6, 220 + 8));
            heart2.SetScale(0.75f, 0.75f);
            heart2.Add("idle", new List<int>(new int[] { 0, 1, 2, 3 }), 0.127f);
            heart2.Play("idle", 2);
            Add(heart2);
        }

        private void MuteMusic()
        {
            if (GP.music.Volume == 0)
            {
                GP.music.Volume = 60;
            }
            else
                GP.music.Volume = 0;
        }

        public override void Draw(RenderWindow rw)
        {
            base.Draw(rw);

            
            SmartText.DrawText(GP.WindowGameName, TextAlignment.MID, new Vector2f(402.0f, 152.0f), new Vector2f(1.5f, 1.5f), Palette.color1, rw);
            SmartText.DrawText(GP.WindowGameName, TextAlignment.MID, new Vector2f(400.0f, 150.0f), new Vector2f(1.5f, 1.5f), Palette.color2, rw);
            //SmartText.DrawText(GP.WindowGameName, TextAlignment.MID, new Vector2f(398.0f, 148.0f), new Vector2f(1.5f, 1.5f), Palette.color1, rw);

            SmartText.DrawTextWithLineBreaks("Created by @LongarMD, @xXBloodyOrange and @Laguna_999 for LD41", TextAlignment.LEFT, new Vector2f(10, 565), new Vector2f(0.5f, 0.5f), Palette.color2, rw);
            SmartText.DrawTextWithLineBreaks("Music CCBY Doctor Turtle ", TextAlignment.LEFT, new Vector2f(10, 580), new Vector2f(0.5f, 0.5f), Palette.color2, rw);



        }

        public override void Update(TimeObject timeObject)
        {

            tb_startGame.active = !exiting;
            base.Update(timeObject);
        
            if (Input.justPressed[Keyboard.Key.Return])
            {
                StartGame();

            }
        }

        private void StartGame()
        {
            if (!exiting)
            {
                JamUtilities.Tweens.ShapeAlphaTween.createAlphaTween(_overlay, 255, 0.5f, () => Game.SwitchState(Game.clicker));
                exiting = true;
            }
        }
    }
}
