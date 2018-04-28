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

		private Animation researchFlask;

		public Animation Coin;
		private float animationTime;

		private bool fistINIT = false;

		private TextButton amount1;
		private TextButton amount10;
		private TextButton amount100;

		private List<TextButton> resourceGainers = new List<TextButton>();

		private TextButton alchemy;
		private List<TextButton> upgrades = new List<TextButton>();
        private bool buttonPressed;

        private float BuildingsBuilt = 0;

        private int NextAttackIncoming = 10;
        private int OldAttackIncoming = 0;

        private float age = 0;

        private ParticleSystem coinParticles;

        private HudBar hb;
        bool animationPlaying = false;

        


        public override void Init()
        {
            base.Init();

			if (fistINIT == false)
			{
                coinParticles = new ParticleSystem("../GFX/coin.png", new IntRect(0, 0, 16, 16), 16);
                Add(coinParticles);
                coinParticles.acceleration = new Vector2f(0, 20);

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

				amount1.SetPosition(new Vector2f(GP.WindowSize.X - amount100.getSize().X - 2.5f - amount10.getSize().X - 2.5f - amount1.getSize().X - 2.5f - 15f, 2.5f));
				amount1.SetTextOffset(new Vector2f(15f, -3f));

				amount10.SetPosition(new Vector2f(GP.WindowSize.X - amount100.getSize().X - 2.5f - amount10.getSize().X - 2.5f - 15f, 2.5f));
				amount10.SetTextOffset(new Vector2f(15f, -3f));

				amount100.SetPosition(new Vector2f(GP.WindowSize.X - amount100.getSize().X - 2.5f - 15f, 2.5f));
				amount100.SetTextOffset(new Vector2f(15f, -3f));


				amount1.active = false;

				Add(amount1);
				Add(amount10);
				Add(amount100);
				#endregion

				float rightWidth = -(GP.WindowSize.X - amount100.getSize().X - 2.5f - amount10.getSize().X - 2.5f - amount1.getSize().X - 2.5f - GP.WindowSize.X) + 40f;
				float rightHeight = GP.WindowSize.Y - 2.5f - amount1.getSize().Y - 50f;


				#region Resource Gainers

				Vector2f resourceGainerScale = new Vector2f(2.75f, 3f);
				Vector2f resourceGainerTextOffset = new Vector2f(50f, 0f);

				resourceGainers.Add(new TextIconButton("Squire", "../GFX/ic_squire.png", Resources.resourceGainers[ResourceGainer.Type.Squire].Add, resourceGainerScale));
				resourceGainers.Add(new TextIconButton("Farmer", "../GFX/ic_farmer.png", Resources.resourceGainers[ResourceGainer.Type.Farmer].Add, resourceGainerScale));
				resourceGainers.Add(new TextIconButton("Knight", "../GFX/ic_knight.png", Resources.resourceGainers[ResourceGainer.Type.Knight].Add, resourceGainerScale));
				resourceGainers.Add(new TextIconButton("Feudal Lord", "../GFX/ic_lord.png", Resources.resourceGainers[ResourceGainer.Type.Feudal_Lord].Add, resourceGainerScale));
				resourceGainers.Add(new TextIconButton("Church", "../GFX/ic_priest.png", Resources.resourceGainers[ResourceGainer.Type.Church].Add, resourceGainerScale));
				resourceGainers.Add(new TextIconButton("Gold Mine", "../GFX/ic_miner_gold.png", Resources.resourceGainers[ResourceGainer.Type.Gold_Mine].Add, resourceGainerScale));
				resourceGainers.Add(new TextIconButton("Diamond Mine", "../GFX/ic_miner_diamond.png", Resources.resourceGainers[ResourceGainer.Type.Diamond_Mine].Add, resourceGainerScale));

				for (int i = 1; i < 8; i++)
				{
					resourceGainers[i - 1].SetPosition(new Vector2f(GP.WindowSize.X - rightWidth, (rightHeight / 7 ) * i - 15f));
					resourceGainers[i - 1].SetTextOffset(resourceGainerTextOffset);

					Add(resourceGainers[i - 1]);
				}
				#endregion

				#region Research

				Vector2f leftButtonScale = new Vector2f(3.1f, 3f);



				alchemy = new TextIconButton("Alchemy Lab", "../GFX/research-button_lastFrame.png", Resources.resourceGainers[ResourceGainer.Type.Alchemy_Lab].Add, leftButtonScale);
				alchemy.SetPosition(new Vector2f(0, 65f));
				alchemy.SetTextOffset(new Vector2f(50f, 0f));

				Add(alchemy);

				#region Research Flask
				researchFlask = new Animation("../GFX/research-button.png", new Vector2u(16, 16));
				researchFlask.Position = new Vector2f(275f, 5f);

				researchFlask.Add("idle", new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }, 0.075f);
				researchFlask.Play("idle");

				Add(researchFlask);
				#endregion

				float leftHeightCeiling = 140;
				float leftHeight = (GP.WindowSize.Y - leftHeightCeiling) / 7;

				Vector2f leftTextOffset = new Vector2f(50f, 0f);

				upgrades.Add(new TextIconButton("+Critical Chance", "../GFX/ic_crtperc.png", Tower.ResearchManager.IncreaseCritChance, leftButtonScale));
				upgrades.Add(new TextIconButton("+Critical Factor", "../GFX/ic_crtfct.png", Tower.ResearchManager.IncreaseCritFactor, leftButtonScale));
				upgrades.Add(new TextIconButton("+Freeze Chance", "../GFX/ic_frperc.png", Tower.ResearchManager.IncreaseFreezeChance, leftButtonScale));
				upgrades.Add(new TextIconButton("+Freeze Duration", "../GFX/ic_frzdur.png", Tower.ResearchManager.IncreaseFreezeDuration, leftButtonScale));
				upgrades.Add(new TextIconButton("+Drop Chance", "../GFX/ic_goldperc.png", Tower.ResearchManager.IncreaseGoldChance, leftButtonScale));
				upgrades.Add(new TextIconButton("+Castle Health", "../GFX/heart_firstFrame.png", AddCastleHealth, leftButtonScale));




				for (int i = 1; i < 7; i++)
				{
					upgrades[i - 1].SetPosition(new Vector2f(0,  16 +leftHeightCeiling + (leftHeight + 4) * (i - 1)));
					upgrades[i - 1].SetTextOffset(leftTextOffset);

					Add(upgrades[i - 1]);
				}

                #endregion

                hb = new HudBar(Coin.GetPosition() + new Vector2f(0, 300));
                hb.bg1.FillColor = new Color(100, 100, 100);
                hb.bg2.FillColor = new Color(30, 30, 30);
                hb.fg.FillColor = new Color(158, 16, 0);
                Add(hb);

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

			SmartText.DrawText("Research points: " + Resources.research + "\nper second: " + Resources.idleResearchIncome, TextAlignment.LEFT, new Vector2f(5f, 5f), rw);
			#endregion



			foreach (var item in resourceGainers)
				item.active = false;

			foreach (var item in upgrades)
				item.active = false;

			if (Resources.CheckMoney(Resources.resourceGainers[ResourceGainer.Type.Squire].nextCost() * Resources.amountSelected)) resourceGainers[0].active = true;
			if (Resources.CheckMoney(Resources.resourceGainers[ResourceGainer.Type.Farmer].nextCost() * Resources.amountSelected)) resourceGainers[1].active = true;
			if (Resources.CheckMoney(Resources.resourceGainers[ResourceGainer.Type.Knight].nextCost() * Resources.amountSelected)) resourceGainers[2].active = true;
			if (Resources.CheckMoney(Resources.resourceGainers[ResourceGainer.Type.Feudal_Lord].nextCost() * Resources.amountSelected)) resourceGainers[3].active = true;
			if (Resources.CheckMoney(Resources.resourceGainers[ResourceGainer.Type.Church].nextCost() * Resources.amountSelected)) resourceGainers[4].active = true;
			if (Resources.CheckMoney(Resources.resourceGainers[ResourceGainer.Type.Gold_Mine].nextCost() * Resources.amountSelected)) resourceGainers[5].active = true;
			if (Resources.CheckMoney(Resources.resourceGainers[ResourceGainer.Type.Diamond_Mine].nextCost() * Resources.amountSelected)) resourceGainers[6].active = true;

			if (Resources.CheckResearch(Tower.ResearchManager.CritChanceCost)) upgrades[0].active = true;
			if (Resources.CheckResearch(Tower.ResearchManager.CritFactorCost) && Tower.ResearchManager.CritChance > 0) upgrades[1].active = true;
			if (Resources.CheckResearch(Tower.ResearchManager.FreezeChanceCost)) upgrades[2].active = true;
			if (Resources.CheckResearch(Tower.ResearchManager.FreezeDurationCost) && Tower.ResearchManager.FreezeChance > 0) upgrades[3].active = true;
			if (Resources.CheckResearch(Tower.ResearchManager.GoldChanceCost)) upgrades[4].active = true;


			resourceGainers[0].text = "Squire"+string.Format("[{0}]", Resources.resourceGainers[ResourceGainer.Type.Squire].amount) +"\nCost: " + Resources.resourceGainers[ResourceGainer.Type.Squire].nextCost() * Resources.amountSelected + "G";
			resourceGainers[1].text = "Farmer" + string.Format("[{0}]", Resources.resourceGainers[ResourceGainer.Type.Farmer].amount) + "\nCost: " + Resources.resourceGainers[ResourceGainer.Type.Farmer].nextCost() * Resources.amountSelected + "G";
			resourceGainers[2].text = "Knight" + string.Format("[{0}]", Resources.resourceGainers[ResourceGainer.Type.Knight].amount) + "\nCost: " + Resources.resourceGainers[ResourceGainer.Type.Knight].nextCost() * Resources.amountSelected + "G";
			resourceGainers[3].text = "Feudal Lord" + string.Format("[{0}]", Resources.resourceGainers[ResourceGainer.Type.Feudal_Lord].amount) + "\nCost: " + Resources.resourceGainers[ResourceGainer.Type.Feudal_Lord].nextCost() + "G";
			resourceGainers[4].text = "Church" + string.Format("[{0}]", Resources.resourceGainers[ResourceGainer.Type.Church].amount) + "\nCost: " + Resources.resourceGainers[ResourceGainer.Type.Church].nextCost() * Resources.amountSelected + "G";
			resourceGainers[5].text = "Gold Mine" + string.Format("[{0}]", Resources.resourceGainers[ResourceGainer.Type.Gold_Mine].amount) + "\nCost: " + Resources.resourceGainers[ResourceGainer.Type.Gold_Mine].nextCost() * Resources.amountSelected + "G";
			resourceGainers[6].text = "Diamond Mine" + string.Format("[{0}]", Resources.resourceGainers[ResourceGainer.Type.Diamond_Mine].amount) + "\nCost: " + Resources.resourceGainers[ResourceGainer.Type.Diamond_Mine].nextCost() * Resources.amountSelected + "G";


			upgrades[0].text = "+Critical Chance\nCost: " + Tower.ResearchManager.CritChanceCost + "R";
			upgrades[1].text = "+Critical Factor\nCost: " + Tower.ResearchManager.CritFactorCost + "R";
			upgrades[2].text = "+Freeze Chance\nCost: " + Tower.ResearchManager.FreezeChanceCost + "R";
			upgrades[3].text = "+Freeze Duration\nCost: " + Tower.ResearchManager.FreezeDurationCost + "R";
			upgrades[4].text = "+Drop Chance\nCost: " + Tower.ResearchManager.GoldChanceCost + "R";

			if (Resources.castleHealth < 10)
			{
				if (Resources.CheckMoney(1000))
					upgrades[5].active = true;
				else
					upgrades[5].active = false;

				upgrades[5].text = "+Castle Health\nCost: 1000G";
			}
			else
			{
				upgrades[5].active = false;
				upgrades[5].text = "+Castle Health\nHEALTH FULL";
			}

			if (Resources.CheckMoney(Resources.resourceGainers[ResourceGainer.Type.Alchemy_Lab].nextCost())) alchemy.active = true;
			else alchemy.active = false;


			alchemy.text = "Alchemy Lab\nCost: " + Resources.resourceGainers[ResourceGainer.Type.Alchemy_Lab].nextCost() * Resources.amountSelected + "G";

            if (BuildingsBuilt >= 0.8 * NextAttackIncoming)
                SmartText.DrawText("Invaders are gathering at the borders!",TextAlignment.MID, new Vector2f(400,300), new Color(158, 16, 0), rw);
		}

		



		public override void Update(TimeObject to)
        {


            if (true)
            {
                age += to.ElapsedGameTime;
                base.Update(to);

#if DEBUG
                if (Input.justPressed[Keyboard.Key.F9])
                {
                    Resources.money += 1000;
                }
#endif

                if (!SFML.Window.Mouse.IsButtonPressed(SFML.Window.Mouse.Button.Left) && buttonPressed == true)
                    buttonPressed = false;

                if (MousePressedOverSprite(Coin._sprites[0].Sprite))
                {
                    Resources.ManualMoneyGain();
                    Coin.Play("spin");
                    animationPlaying = true;
                    coinParticles.Start( coinParticleSetup );
                }

                if (animationPlaying)
                    animationTime += to.ElapsedGameTime;
                if (animationPlaying && animationTime >= 0.25f)
                {
                    Coin.Play("idle");
                    animationTime = 0f;
                    animationPlaying = false;
                }

                //T.TraceD(getValue().ToString());
                //T.TraceD(NextAttackIncoming.ToString());


                double t = getValue() - OldAttackIncoming;
                double d = NextAttackIncoming - OldAttackIncoming;
                hb.health = (float)((t) / (d));

                //T.Trace(t.ToString());
                //T.Trace(d.ToString());
                //T.Trace(hb.health.ToString());
                //T.Trace("");


                if (BuildingsBuilt < NextAttackIncoming && getValue() >= NextAttackIncoming)
                {


                    float incr =  6 + NextAttackIncoming * (0.2f);
                    if (incr > 15) incr = 15;
                    OldAttackIncoming = NextAttackIncoming;
                    NextAttackIncoming += (int)incr;
                    T.Trace(incr.ToString());

                    Game.switchToTower();
                    

                    
                }
                BuildingsBuilt = ResourceGainer.totalBuildingsBuilt;

            }
		}

        private void coinParticleSetup(SmartSprite s)
        {

            s.Alpha = 255;
            float angle = (float)(RandomGenerator.Random.NextDouble()*2*Math.PI);
            float r = (float)(RandomGenerator.Random.NextDouble() * 5*16);

            float x = (float)(Math.Cos(angle) * r) + Coin.Position.X + 80;
            float y = (float)(Math.Sin(angle) * r) + Coin.Position.Y + 80;

            float vx = (float)(Math.Cos(angle) * 200);
            float vy = (float)(((Math.Sin(angle) * 200)));

            s.SetPosition(new Vector2f(x,y));
            s.velocity = new Vector2f(vx, vy);

            JamUtilities.Tweens.SpriteAlphaTween.createAlphaTween(s, 0, 0.75f,null, PennerDoubleAnimation.EquationType.CubicEaseOut);
        }


        private float getValue ()
        {
            return ResourceGainer.totalBuildingsBuilt + age / 25.0f;
        }

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

		#region Upgrade buton functions
		private void AddCastleHealth()
		{
			if(Resources.DecreaseMoney(1000))
				Resources.AddCastleHealth(1);
		}
		#endregion
	}
}
