using JamUtilities;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate
{
    class StateClicker : JamUtilities.GameState
    {


		private SmartSprite button;
		private bool buttonPressed;
        
        public override void Init()
        {
            base.Init();

			button = new SmartSprite(new Texture("../GFX/box.png"));
			button.Position = new Vector2f(GP.WindowSize.X / 2, GP.WindowSize.Y / 2);
        }

        public override void Draw(RenderWindow rw)
        {
            base.Draw(rw);
			//T.Trace("clicker");
			
			DrawUI(rw);

		}

		public override void Update(TimeObject timeObject)
        {
            base.Update(timeObject);

			CheckManualButton();

		}




		private void CheckManualButton()
		{
			if (JamUtilities.Mouse.MousePositionInWorld.X > button.Position.X - button.Size.X && JamUtilities.Mouse.MousePositionInWorld.X < button.Position.X + button.Size.X)
			{
				if (JamUtilities.Mouse.MousePositionInWorld.Y > button.Position.Y - button.Size.Y && JamUtilities.Mouse.MousePositionInWorld.Y < button.Position.Y + button.Size.Y)
				{
					if (SFML.Window.Mouse.IsButtonPressed(SFML.Window.Mouse.Button.Left) && buttonPressed == false)
					{
						buttonPressed = true;

						Resources.UpdateMoney();
					}
					if (!SFML.Window.Mouse.IsButtonPressed(SFML.Window.Mouse.Button.Left) && buttonPressed == true)
						buttonPressed = false;
				}
			}
		}
		private void DrawUI(RenderWindow rw)
		{
			rw.Draw(button.Sprite);

			SmartText.DrawText("Money: " + Resources.money, TextAlignment.LEFT, new Vector2f(5f, 0f), rw);
		}
    }
}
