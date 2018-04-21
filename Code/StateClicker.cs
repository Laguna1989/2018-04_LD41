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
		private SmartSprite button2;

		private bool buttonPressed;
        
        public override void Init()
        {
            base.Init();

			button = new SmartSprite(new Texture("../GFX/box.png"));
			button.Position = new Vector2f(GP.WindowSize.X / 2, GP.WindowSize.Y / 2);

			button2 = new SmartSprite(new Texture("../GFX/box.png"));
			button2.Position = new Vector2f(GP.WindowSize.X / 2, GP.WindowSize.Y / 2 + 64);
			button2.Sprite.Color = Color.Blue;
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
			CheckManualButton2();
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

		private void CheckManualButton2()
		{
			if (JamUtilities.Mouse.MousePositionInWorld.X > button2.Position.X - button2.Size.X && JamUtilities.Mouse.MousePositionInWorld.X < button2.Position.X + button2.Size.X)
			{
				if (JamUtilities.Mouse.MousePositionInWorld.Y > button2.Position.Y - button2.Size.Y && JamUtilities.Mouse.MousePositionInWorld.Y < button2.Position.Y + button2.Size.Y)
				{
					if (SFML.Window.Mouse.IsButtonPressed(SFML.Window.Mouse.Button.Left) && buttonPressed == false)
					{
						buttonPressed = true;

						Resources.resourceGainers[ResourceGainer.Type.Squire].Add(1);
					}
					if (!SFML.Window.Mouse.IsButtonPressed(SFML.Window.Mouse.Button.Left) && buttonPressed == true)
						buttonPressed = false;
				}
			}
		}
		private void DrawUI(RenderWindow rw)
		{
			rw.Draw(button.Sprite);
			rw.Draw(button2.Sprite);

			SmartText.DrawText("Money: " + Resources.money, TextAlignment.LEFT, new Vector2f(5f, 0f), rw);
			SmartText.DrawText("Money idle income: " + Resources.idleMoneyIncome, TextAlignment.LEFT, new Vector2f(5f, 25f), rw);

		}
	}
}
