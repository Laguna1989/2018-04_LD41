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

		public Animation Coin;
		private float animationTime;

		private bool fistINIT = false;

		private TextButton amount1;
		private TextButton amount10;
		private TextButton amount100;

		private List<TextButton> resourceGainers = new List<TextButton>();

		public override void Init()
        {
            base.Init();

			if (fistINIT == false)
			{
				#region Coin
				Coin = new Animation("../GFX/coin.png", new Vector2u(16, 16));
				Coin.Add("idle", new List<int>(new int[] { 0 }), 1f);
				Coin.Add("spin", new List<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }), 0.025f);


				Coin.SetScale(5f, 5f);


				Coin.Play("idle");

				Coin.Position = new Vector2f(GP.WindowSize.X / 2 - 8*Coin.Scale.X, GP.WindowSize.Y / 2 - 8 * Coin.Scale.Y);

				Add(Coin);
				#endregion

				#region Amount buttons
				amount1 = new TextButton("1", selectAmount1, new Vector2f(0.75f, 1.5f));
				amount10 = new TextButton("10", selectAmount10, new Vector2f(0.75f, 1.5f));
				amount100 = new TextButton("100", selectAmount100, new Vector2f(0.75f, 1.5f));

				amount1.SetPosition(new Vector2f(GP.WindowSize.X - amount100.getSize().X - 2.5f - amount10.getSize().X - 2.5f - amount1.getSize().X - 2.5f, 2.5f));
				amount1.SetTextOffset(new Vector2f(15f, -3f));

				amount10.SetPosition(new Vector2f(GP.WindowSize.X - amount100.getSize().X - 2.5f - amount10.getSize().X - 2.5f, 2.5f));
				amount10.SetTextOffset(new Vector2f(15f, -3f));

				amount100.SetPosition(new Vector2f(GP.WindowSize.X - amount100.getSize().X - 2.5f, 2.5f));
				amount100.SetTextOffset(new Vector2f(15f, -3f));


				amount1.active = false;

				Add(amount1);
				Add(amount10);
				Add(amount100);
				#endregion

				float rightWidth = -(GP.WindowSize.X - amount100.getSize().X - 2.5f - amount10.getSize().X - 2.5f - amount1.getSize().X - 2.5f - GP.WindowSize.X);
				float rightHeight = GP.WindowSize.Y - 2.5f - amount1.getSize().Y - 50f;


				#region Resource Gainers

				Vector2f resourceGainerScale = new Vector2f(2.25f, 2.25f);

				resourceGainers.Add(new TextButton("Squire", Resources.resourceGainers[ResourceGainer.Type.Squire].Add, resourceGainerScale));
				resourceGainers.Add(new TextButton("Farmer", Resources.resourceGainers[ResourceGainer.Type.Farmer].Add, resourceGainerScale));
				resourceGainers.Add(new TextButton("Knight", Resources.resourceGainers[ResourceGainer.Type.Knight].Add, resourceGainerScale));
				resourceGainers.Add(new TextButton("Feudal Lord", Resources.resourceGainers[ResourceGainer.Type.Feudal_Lord].Add, resourceGainerScale));
				resourceGainers.Add(new TextButton("Church", Resources.resourceGainers[ResourceGainer.Type.Church].Add, resourceGainerScale));
				resourceGainers.Add(new TextButton("Gold Mine", Resources.resourceGainers[ResourceGainer.Type.Gold_Mine].Add, resourceGainerScale));
				resourceGainers.Add(new TextButton("Diamond Mine", Resources.resourceGainers[ResourceGainer.Type.Diamond_Mine].Add, resourceGainerScale));

				for (int i = 1; i < 8; i++)
				{
					resourceGainers[i - 1].SetPosition(new Vector2f(GP.WindowSize.X - rightWidth, (rightHeight / 7 - 15f) * i - 15f));
					resourceGainers[i - 1].SetTextOffset(new Vector2f(7.5f, 5f));

					Add(resourceGainers[i - 1]);
				}
				#endregion

				#region Research
				#endregion

				fistINIT = true;
			}

		}

		public override void Draw(RenderWindow rw)
        {
            base.Draw(rw);
			//T.Trace("clicker");

			#region Text

			SmartText.DrawText("Gold: " + Resources.money, TextAlignment.MID, new Vector2f(GP.WindowSize.X / 2, 0f), rw);
			SmartText.DrawText("per second: " + Resources.idleMoneyIncome, TextAlignment.MID, new Vector2f(GP.WindowSize.X / 2, 25f), rw);

			#endregion

		}

		bool animationPlaying = false;
		public override void Update(TimeObject timeObject)
        {
            base.Update(timeObject);

			if (!SFML.Window.Mouse.IsButtonPressed(SFML.Window.Mouse.Button.Left) && buttonPressed == true)
				buttonPressed = false;

			if (MousePressedOverSprite(Coin._sprites[0].Sprite))
			{
				Resources.ManualMoneyGain();
				Coin.Play("spin");
				animationPlaying = true;
			}

			if (animationPlaying)
				animationTime += timeObject.ElapsedGameTime;
			if (animationPlaying && animationTime >= 0.25f)
			{
				Coin.Play("idle");
				animationTime = 0f;
				animationPlaying = false;
			}
		}
		
		private bool buttonPressed;
		private bool MousePressedOverSprite(Sprite sprite)
		{
			if (JamUtilities.Mouse.MousePositionInWorld.X > sprite.Position.X && JamUtilities.Mouse.MousePositionInWorld.X < sprite.Position.X + 16 * sprite.Scale.X)
			{
				if (JamUtilities.Mouse.MousePositionInWorld.Y > sprite.Position.Y && JamUtilities.Mouse.MousePositionInWorld.Y < sprite.Position.Y + 16 * sprite.Scale.Y)
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

		#region Amount button functions
		private void selectAmount1()
		{
			Resources.amountSelected = 1;
			amount1.active = false;
			amount10.active = true;
			amount100.active = true;
		}
		private void selectAmount10()
		{
			Resources.amountSelected = 10;
			amount1.active = true;
			amount10.active = false;
			amount100.active = true;
		}
		private void selectAmount100()
		{
			Resources.amountSelected = 100;
			amount1.active = true;
			amount10.active = true;
			amount100.active = false;
		}

		#endregion

	}
}
