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
		public static float idleMoneyIncome { get; set; } = 0.1f;

		public static float idleResearchIncome { get; set; } = 0;

		public static void UpdateIdleIncome()
		{
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

		public static void UpdateMoney()
		{
			money += moneyPerClick;
		}
	}
}
