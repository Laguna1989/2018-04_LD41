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
		public Resources resources;

		private SmartSprite button;
		private bool buttonPressed;
        
        public override void Init()
        {
            base.Init();
			resources = new Resources();

			button = new SmartSprite(new Texture("../GFX/box.png"));
			button.Position = new Vector2f(GP.WindowSize.X / 2, GP.WindowSize.Y / 2);
        }

        public override void Draw(RenderWindow rw)
        {
            base.Draw(rw);
			//T.Trace("clicker");

			rw.Draw(button.Sprite);
        }

		public override void Update(TimeObject timeObject)
        {
            base.Update(timeObject);

			UpdateManualButton();

		}




		private void UpdateManualButton()
		{
			if (JamUtilities.Mouse.MousePositionInWorld.X > button.Position.X - button.Size.X && JamUtilities.Mouse.MousePositionInWorld.X < button.Position.X + button.Size.X)
			{
				if (JamUtilities.Mouse.MousePositionInWorld.Y > button.Position.Y - button.Size.Y && JamUtilities.Mouse.MousePositionInWorld.Y < button.Position.Y + button.Size.Y)
				{
					if (SFML.Window.Mouse.IsButtonPressed(SFML.Window.Mouse.Button.Left) && buttonPressed == false)
					{
						buttonPressed = true;

						resources.money++;
						T.Trace(resources.money.ToString());
					}
					if (!SFML.Window.Mouse.IsButtonPressed(SFML.Window.Mouse.Button.Left) && buttonPressed == true)
						buttonPressed = false;
				}
			}
		}
    }
}
