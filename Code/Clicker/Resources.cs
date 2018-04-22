using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate
{
	public static class Resources
	{
		public const float INCOME_TICK = 1f; // The amount of time in seconds it takes for the idleResearchIcome and idleMoneyIncome to be added in Game.cs

		public static float moneyLastTickRemainder { get; set; } = 0;
		public static float researchLastTickRemainder { get; set; } = 0;

		public static long money { get; set; } = 0;
		public static long research { get; set; } = 0;

		public static long moneyPerClick { get; set; } = 1;
		public static float idleMoneyIncome { get; set; } = 0f;

		public static float idleResearchIncome { get; set; } = 0;

		public static int amountSelected = 1;


		public static void UpdateIdleIncome()
		{
			UpdateIncomeAmount();

			float moneyRemainder = idleMoneyIncome - (float)((int)idleMoneyIncome);
			moneyLastTickRemainder += moneyRemainder;
			if((int)moneyLastTickRemainder > 0)
			{
				money += (int)moneyLastTickRemainder;
				moneyLastTickRemainder -= (int)moneyLastTickRemainder;
			}
			money += (int)idleMoneyIncome;



			float researchRemainder = idleResearchIncome - (float)((int)idleResearchIncome);
			researchLastTickRemainder += researchRemainder;
			if ((int)researchLastTickRemainder > 0)
			{
				research += (int)researchLastTickRemainder;
				researchLastTickRemainder -= (int)researchLastTickRemainder;
			}
			research += (int)idleResearchIncome;
		}

		public static void ManualMoneyGain()
		{
			money += moneyPerClick;
		}

		public static bool DecreaseMoney(long amount)
		{
			long newBalacnce = money - amount;
			if (newBalacnce >= 0)
			{
				money = newBalacnce;
				Console.WriteLine("New balance: " + newBalacnce);
				return true;
			}
			else
				return false;
		}

		public static Dictionary<ResourceGainer.Type, ResourceGainer> resourceGainers = new Dictionary<ResourceGainer.Type, ResourceGainer>()
		{
			{ ResourceGainer.Type.Squire, new ResourceGainer(ResourceGainer.Type.Squire) },
			{ ResourceGainer.Type.Farmer, new ResourceGainer(ResourceGainer.Type.Farmer) },
			{ ResourceGainer.Type.Knight, new ResourceGainer(ResourceGainer.Type.Knight) },
			{ ResourceGainer.Type.Feudal_Lord, new ResourceGainer(ResourceGainer.Type.Feudal_Lord) },
			{ ResourceGainer.Type.Church, new ResourceGainer(ResourceGainer.Type.Church) },
			{ ResourceGainer.Type.Gold_Mine, new ResourceGainer(ResourceGainer.Type.Gold_Mine) },
			{ ResourceGainer.Type.Diamond_Mine, new ResourceGainer(ResourceGainer.Type.Diamond_Mine) }
		};

		public static Dictionary<Upgrade.Type, Upgrade> upgrades = new Dictionary<Upgrade.Type, Upgrade>()
		{
			{ Upgrade.Type.Bronze_Rod, new Upgrade(Upgrade.Type.Bronze_Rod) },
			{ Upgrade.Type.Silver_Rod, new Upgrade(Upgrade.Type.Silver_Rod) },
			{ Upgrade.Type.Gold_Rod, new Upgrade(Upgrade.Type.Gold_Rod) },
			{ Upgrade.Type.Platinum_Rod, new Upgrade(Upgrade.Type.Platinum_Rod) },
			{ Upgrade.Type.Diamond_Rod, new Upgrade(Upgrade.Type.Diamond_Rod) }
		};

		public static void UpdateIncomeAmount()
		{
			idleMoneyIncome = 0;
			idleResearchIncome = 0;
			foreach (KeyValuePair<ResourceGainer.Type, ResourceGainer> pair in resourceGainers)
			{

				idleMoneyIncome += pair.Value.moneyIncome * pair.Value.amount;
				idleResearchIncome += pair.Value.researchIncome * pair.Value.amount;
				
			}
		}

        internal static void Reset()
        {
            money = 0;
        }
    }
}
