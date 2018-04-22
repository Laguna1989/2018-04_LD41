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

		//public Animation Coin;
		//private float animationTime;

		private bool fistINIT = false;

		private TextButton gainers_Farmer;

		public override void Init()
        {
            base.Init();

			if (fistINIT == false)
			{
				/*
				Coin = new Animation("../GFX/coin.png", new Vector2u(16, 16));
				Coin.Add("idle", new List<int>(new int[] { 0 }), 1f);
				Coin.Add("spin", new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }), 0.025f);

				Coin.Play("idle");

				Coin.Position = new Vector2f(GP.WindowSize.X / 2, 64);

				Add(Coin);

				fistINIT = true;
				*/

				//gainers_Farmer = new TextButton("Farmer", );
			}

		}

		public override void Draw(RenderWindow rw)
        {
            base.Draw(rw);
			//T.Trace("clicker");

			#region Text

			SmartText.DrawText("Money: " + Resources.money, TextAlignment.LEFT, new Vector2f(5f, 0f), rw);
			SmartText.DrawText("Money idle income: " + Resources.idleMoneyIncome, TextAlignment.LEFT, new Vector2f(5f, 25f), rw);

			#endregion

		}

		bool animationPlaying = false;
		public override void Update(TimeObject timeObject)
        {
            base.Update(timeObject);

			//if (!SFML.Window.Mouse.IsButtonPressed(SFML.Window.Mouse.Button.Left) && buttonPressed == true)
			//	buttonPressed = false;

			//if (MousePressedOverSprite(Coin._sprites[0].Sprite))
			//{
			//	Resources.ManualMoneyGain();
			//	Coin.Play("spin");
			//	animationPlaying = true;
			//}

			//if (animationPlaying)
			//	animationTime += timeObject.ElapsedGameTime;
			//if(animationPlaying && animationTime >= 0.25f)
			//{
			//	Coin.Play("idle");
			//	animationTime = 0f;
			//	animationPlaying = false;
			//}
		}
		/*
		private bool buttonPressed;
		private bool MousePressedOverSprite(Sprite sprite)
		{
			if (JamUtilities.Mouse.MousePositionInWorld.X > sprite.Position.X && JamUtilities.Mouse.MousePositionInWorld.X < sprite.Position.X + 32)
			{
				if (JamUtilities.Mouse.MousePositionInWorld.Y > sprite.Position.Y && JamUtilities.Mouse.MousePositionInWorld.Y < sprite.Position.Y + 32)
				{
					if (SFML.Window.Mouse.IsButtonPressed(SFML.Window.Mouse.Button.Left) && buttonPressed == false)
					{
						buttonPressed = true;
						return true;
					}

				}
			}

			return false;
		}
		*/


	}
}
